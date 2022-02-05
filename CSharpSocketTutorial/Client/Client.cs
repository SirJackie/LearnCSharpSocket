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
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 1302);  // is equal to s.connect()
                NetworkStream stream = client.GetStream();

                //
                // Write a Message
                //

                string msg = "My name is Neo";
                byte[] msgBin = Encoding.UTF8.GetBytes(msg);
                stream.Write(msgBin, 0, msgBin.Length);

                //
                // Read a Message
                //

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

                string request = Encoding.UTF8.GetString(buffer, 0, recv);
                Console.WriteLine(request);

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
