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
                TcpClient client = new TcpClient("127.0.0.1", 1302);
                string messageToSend = "My name is Neo";

                int byteCount = Encoding.ASCII.GetByteCount(messageToSend + 1);
                byte[] sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes(messageToSend);

                NetworkStream stream = client.GetStream();
                stream.Write(sendData, 0, sendData.Length);
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
