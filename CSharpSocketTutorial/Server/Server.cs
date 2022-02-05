using System;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        public static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 1302);  // is equal to s.bind()
            listener.Start();
            while(true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client accepted.");
                NetworkStream stream = client.GetStream();
                try
                {
                    //
                    // Read a Message
                    //

                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);

                    // Count the Length of the Byte Array received
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if(b != 0)
                        {
                            recv++;
                        }
                    }

                    string request = Encoding.UTF8.GetString(buffer, 0, recv);
                    Console.WriteLine(request);


                    //
                    // Write a Message
                    //

                    string msg = "OK, I know your name is Leo.";
                    byte[] msgBin = Encoding.UTF8.GetBytes(msg);
                    stream.Write(msgBin, 0, msgBin.Length);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong.");
                }
            }
        }
    }
}
