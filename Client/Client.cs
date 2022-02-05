using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Client
{
    class Client
    {
        public static void Main(string[] arg)
        {
            connection:
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 1302);  // is equal to s.connect()

                string msg = "My name is Neo";
                byte[] msgBin = Encoding.UTF8.GetBytes(msg);

                NetworkStream stream = client.GetStream();
                stream.Write(msgBin, 0, msgBin.Length);
                Console.WriteLine("Sending data to Server...");

                StreamReader sr = new StreamReader(stream);
                string response = sr.ReadLine();
                Console.WriteLine(response);

                stream.Close();
                client.Close();
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to connect...");
                goto connection;
            }
        }
    }
}
