using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DeviceIpFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private CancellationTokenSource _ts;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            var port = PortTextBox.Text;
            var timeout = TimeoutTextBox.Text;

            string ip1 = Ip1TextBox.Text;
            string ip2 = Ip2TextBox.Text;
            string ip3 = Ip3TextBox.Text;

            IpsListTextBox.Text = "";

            _ts = new CancellationTokenSource();
            CancellationToken ct = _ts.Token;

            Task task = Task.Run(() => RunSearch(ip1, ip2, ip3, port, timeout, ct), ct)
                // ReSharper disable once MethodSupportsCancellation
                .ContinueWith(
                    t => Dispatcher.Invoke(
                        DispatcherPriority.Send,
                        new Action(() => StartButton.IsEnabled = true)
                    )
                );

        }

        private void RunSearch(string ip1, string ip2, string ip3, string port, string timeout, CancellationToken ct)
        {
            for (int i = 1; i < 255; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    return;
                }

                var hostname = ip1 + "." + ip2 + "." + ip3 + "." + i;
                IPAddress ip;
                int portInt;
                int timeoutInt;

                try
                {
                    ip = IPAddress.Parse(hostname);
                    portInt = int.Parse(port);
                    timeoutInt = int.Parse(timeout);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An error has occured");
                    return;
                }

                Dispatcher.Invoke(DispatcherPriority.Send, new Action(() =>
                {
                    IpsListTextBox.Text += "Connecting to " + hostname + ":" + port;
                }));

                if (Connect(ip, portInt, timeoutInt))
                {
                    Dispatcher.Invoke(DispatcherPriority.Send, new Action(() =>
                    {
                        IpsListTextBox.Text += " Success\n";
                    }));
                }
                else
                {
                    Dispatcher.Invoke(DispatcherPriority.Send, new Action(() =>
                    {
                        IpsListTextBox.Text += " Failure\n";
                    }));
                }

            }
        }

        private bool Connect(IPAddress host, int port, int timeout)
        {
            var client = new TcpClient();
            try
            {
                Task connectionTask = client.ConnectAsync(host, port);
                if (!connectionTask.Wait(timeout))
                {
                    client.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                client.Close();
                return false;
            }
            client.Close();
            return true;

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (_ts != null)
            {
                _ts.Cancel();
            }
        }
    }
}
