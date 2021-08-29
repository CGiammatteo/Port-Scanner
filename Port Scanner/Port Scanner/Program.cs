using System;
using System.Net;
using System.Net.Sockets;

namespace Port_Scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TCP Port Scanner";

            Console.WriteLine("Input An IP To Scan: ");
            string ip = Console.ReadLine();
            Console.WriteLine("Choose An Option: ");

            string[] options = { "Scan Ports 1 By 1", "Scan Common TCP Ports", "Scan Every Port (0 - 65535)" };

            for(int i = 0; i < options.Length; i++)
            {
                Console.WriteLine("[" + i + "] " + options[i]);
            }

            int answer = Convert.ToInt32(Console.ReadLine());

            switch (answer)
            {
                case 0:
                    while (true)
                    {
                        Console.WriteLine("Port: ");
                        int port = Convert.ToInt32(Console.ReadLine());
                        ScanPort(ip, port);
                    }
                case 1:
                    int[] commonPorts = { 80, 443, 53, 25, 20, 21, 502, 102, 44818 };

                    Console.WriteLine("Starting...");

                    foreach (int port in commonPorts)
                    {
                        ScanPort(ip, port);
                    }
                    break;
                case 2:
                    Console.WriteLine("Starting...");

                    for (int i = 0; i < 65535; i++)
                    {
                        ScanPort(ip, i);
                    }
                    break;
                default:
                    Colors.Error("Incorrect Input! Press Any Key To Close...");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
            }

            Colors.Success("Task Completed! Press Any Key To Close...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void ScanPort(string ip, int port)
        {
            using (TcpClient scanner = new TcpClient())
            {
                try
                {
                    scanner.Connect(IPAddress.Parse(ip), port);
                    Colors.Success("Port " + port + " | OPEN");
                }
                catch (Exception ex)
                {
                    Colors.Error("Port " + port + " | CLOSED");
                }
            }
        }
    }
}
