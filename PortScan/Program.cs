using System.IO;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

class Program
{
    public static void Main()
    {
        Console.Write("Ingresa la dirección IP que deseas escanear: ");
        string ipAddress = Console.ReadLine();

        Console.Write("Ingresa el puerto inicial: ");
        int startPort = int.Parse(Console.ReadLine());

        Console.Write("Ingresa el puerto final: ");
        int endPort = int.Parse(Console.ReadLine());

        Console.WriteLine("Escaneando puertos abiertos en " + ipAddress + "...");

        List<Thread> threads = new List<Thread>();

        for (int port = startPort; port <= endPort; port++)
        {
            Thread thread = new Thread(() => ScanPort(ipAddress, port));
            thread.Start();
            threads.Add(thread);
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        Console.WriteLine("Escaneo de puertos completado.");
        Console.ReadLine();
    }

    public static void ScanPort(string ipAddress, int port)
    {
        using (TcpClient tcpClient = new TcpClient())
        {
            try
            {
                tcpClient.Connect(ipAddress, port);
                Console.WriteLine("[+] Puerto: " +  port + " abierto");
            }
            catch (SocketException)
            {
                //Console.WriteLine($"Puerto {port} cerrado");
            }
        }
    }
}