using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NpWelcomeApp
{
    static class Examples
    {
        public static void IPEndPointExample()
        {
            IPAddress address1 = new IPAddress(new byte[] { 127, 0, 0, 1 });
            IPAddress address2 = new IPAddress(0x0100007F);

            IPAddress address3 = IPAddress.Parse("127.0.0.1");

            IPAddress localHost = IPAddress.Loopback;  // 127.0.0.1
            IPAddress anyHost = IPAddress.Any;         // 0.0.0.0
            IPAddress broadCast = IPAddress.Broadcast; // 255.255.255.255

            Console.WriteLine(address1.AddressFamily.ToString());

            IPEndPoint endPoint = new IPEndPoint(address1, 5000);
            IPEndPoint endPoint2 = IPEndPoint.Parse("127.0.0.1:5000");

            IPEndPoint.TryParse("127.0.0.1:5000", out IPEndPoint? endPoint3);
        }

        public static void UriExample()
        {
            Uri uri = new Uri("https://tula.mysite.ru:5000/about/works/employee?name=bobby&age=26#part1");

            Console.WriteLine(uri);
            Console.WriteLine($"AbsoluteUri {uri.AbsoluteUri}");
            Console.WriteLine($"Scheme {uri.Scheme}");
            Console.WriteLine($"Authority {uri.Authority}");
            Console.WriteLine($"Host {uri.Host}");
            Console.WriteLine($"Port {uri.Port}");
            Console.WriteLine($"PathAndQuery {uri.PathAndQuery}");
            Console.WriteLine($"LocalPath {uri.LocalPath}");
            Console.WriteLine($"Query {uri.Query}");
            Console.WriteLine($"Fragment {uri.Fragment}");
            Console.WriteLine("Segments:");
            foreach (var segment in uri.Segments)
                Console.WriteLine($"\t{segment}");


            UriBuilder builder = new();
            builder.Scheme = "https";
            builder.Host = "tula.mysite.com";
            builder.Port = 3000;
            builder.Path = "company/gallery/works";
            builder.Query = "brand=omni&month=5";
            builder.Fragment = "page3";

            Uri uriNew = builder.Uri;

            Console.WriteLine(uriNew);
        }

        public static void DnsExamples()
        {
            string domen = "yandex.ru";
            IPHostEntry domenEntry = Dns.GetHostEntry(domen);

            Console.WriteLine($"IP adresses for {domenEntry.HostName}:");
            foreach (IPAddress ip in domenEntry.AddressList)
                Console.WriteLine($"\t{ip}");
            Console.WriteLine();

            IPAddress[] ips = Dns.GetHostAddresses(domen);

            Console.WriteLine($"IP adresses for {domen}:");
            foreach (IPAddress ip in ips)
                Console.WriteLine($"\t{ip}");
            Console.WriteLine();

            var localName = Dns.GetHostName();
            var localIps = Dns.GetHostAddresses(localName, System.Net.Sockets.AddressFamily.InterNetwork);

            Console.WriteLine($"IP adresses for {localName}:");
            foreach (IPAddress ip in localIps)
                Console.WriteLine($"\t{ip}");
            Console.WriteLine();
        }

        public static void ConnectionsPropsExamples()
        {
            var adapters = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var adapter in adapters)
            {
                Console.WriteLine(new String('*', 50));
                Console.WriteLine($"Id adapter: {adapter.Id}");
                Console.WriteLine($"Name adapter: {adapter.Name}");
                Console.WriteLine($"Description adapter: {adapter.Description}");
                Console.WriteLine($"Type adapter: {adapter.NetworkInterfaceType}");
                Console.WriteLine($"Physical address adapter: {adapter.GetPhysicalAddress()}");
                Console.WriteLine($"Status adapter: {adapter.OperationalStatus}");
                Console.WriteLine($"Speed adapter: {adapter.Speed}");
                Console.WriteLine();
            }

            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnections = ipProperties.GetActiveTcpConnections();

            foreach (var tcpConnection in tcpConnections)
            {
                Console.WriteLine(new String('*', 50));
                Console.WriteLine($"Local address: {tcpConnection.LocalEndPoint.Address}:{tcpConnection.LocalEndPoint.Port}");
                Console.WriteLine($"Local address: {tcpConnection.RemoteEndPoint.Address}:{tcpConnection.RemoteEndPoint.Port}");
                Console.WriteLine($"State: {tcpConnection.State}");
                Console.WriteLine();
            }
        }

        public static void SocketsExample()
        {
            Socket socketTcp = new Socket(AddressFamily.InterNetwork,
                           SocketType.Stream,
                           ProtocolType.Tcp);

            Socket socketUdp = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Dgram,
                                          ProtocolType.Udp);

            IPEndPoint remoteEndPoint = IPEndPoint.Parse("77.88.44.55:443");
            socketTcp.Connect(remoteEndPoint);

            Console.WriteLine($"Address Famaly: {socketTcp.AddressFamily}");
            Console.WriteLine($"Socket Type: {socketTcp.SocketType}");
            Console.WriteLine($"Protocol Type: {socketTcp.ProtocolType}");

            Console.WriteLine($"Local EndPoint: {socketTcp.LocalEndPoint?.ToString()}");
            Console.WriteLine($"Remote EndPoint: {socketTcp.RemoteEndPoint?.ToString()}");


            Console.WriteLine($"Available: {socketTcp.Available}");
            Console.WriteLine($"Connected: {socketTcp.Connected}");

            socketTcp.Close();
            socketUdp.Close();
        }
    }
}
