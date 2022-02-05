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

        public static string RecvMessage(NetworkStream stream, int maxLen)
        {
            byte[] buffer = new byte[maxLen];
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

        public static void SafeSendMessage(NetworkStream stream, string msg)
        {
            SendMessage(stream, msg.Length.ToString().PadRight(10));
            SendMessage(stream, msg);
        }

        public static string SafeRecvMessage(NetworkStream stream)
        {
            int msgLen = int.Parse(RecvMessage(stream, 10).Trim());
            return RecvMessage(stream, msgLen);
        }

        public static void Main(string[] arg)
        {
            TcpClient client = new TcpClient("127.0.0.1", 1302);  // is equal to s.connect()
            NetworkStream stream = client.GetStream();

            SafeSendMessage(stream, "My name is Sir Jackie.");
            Console.WriteLine(SafeRecvMessage(stream));
                
            stream.Close();
            client.Close();

            Console.ReadKey();
        }
    }
}
