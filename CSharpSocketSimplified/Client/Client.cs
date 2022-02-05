using System;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Client
{
    class Client
    {
        public static void SendMessage(NetworkStream stream, string msg)
        {
            byte[] msgBin = Encoding.UTF8.GetBytes(msg);
            stream.Write(msgBin, 0, msgBin.Length);
        }

        public static string RecvMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            stream.Read(buffer, 0, buffer.Length);

            // Count the Length of the Byte Array received
            int recv = 0;
            foreach (byte b in buffer)
            {
                if (b != 0)
                {
                    recv++;
                }
            }

            return Encoding.UTF8.GetString(buffer, 0, recv);
        }

        public static void Main(string[] arg)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 1302);  // is equal to s.connect()
                NetworkStream stream = client.GetStream();

                SendMessage(stream, "My name is Sir Jackie.");
                Console.WriteLine(RecvMessage(stream));
                
                stream.Close();
                client.Close();

            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to connect...");
            }

            Console.ReadKey();
        }
    }
}
