# DeviceIpFinder
This application will help you find an IP address for a device on your local network.

The application requires first 3 sections of an IP address, port number, and connection timeout value in milliseconds. The defaults are IP: 192.168.0.X, Port 22, Timeout 500. When 'Start' button is clicked, the application will attempt to connect to every IP address in the range between 192.168.0.1 - 192.168.0.254 (inclusive), and will report on the outcome in the scrollable textbox.

For example: You connect Raspberry Pi to your network, and want to connect to it without having to configure static IP address allocation. To connect to Raspberry Pi you will need to know an IP address assigned to the device. Run the DeviceIpFinder to find the IP of the Raspberry Pi.

# Questions and Answers
Q: How to find Raspberry Pi IP address?

A: Answer has three steps:
1. Once you connected Raspberry Pi to your network run the DeviceIpFinder and specify the first three octets of your network IP address like this: 192.168.0 or 192.168.1
3. Specify the configured SSH port (or any other port you requre) like this: 22
2. Click the DeviceIpFinder's "Start" button
DeviceIpFinder will display the address of your Raspberry PI along with "Success" status
