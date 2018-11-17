using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace CCC01
{
    public class TcpConnection
    {
        private TcpClient tcpClient;
        private NetworkStream stream;

        public void connect()
        {
            /* start jar */

            /*string path = @"C:\Media\Dokumente\Dropbox\CCC\CCC02\CCC01\bin\Debug\simulator.jar";
            var processInfo = new ProcessStartInfo(@"C:\Program Files\Java\jre1.8.0_40\bin\java.exe", "-jar \"" + path + "\" --level=input-level4.txt --tcp=7000");*/

            string path = @"M:\Dokumente\Dropbox\CCC\CCC01\CCC01\bin\Debug\simulator.jar";
            var processInfo = new ProcessStartInfo(@"C:\Program Files\Java\jre1.8.0_45\bin\java.exe", "-jar \"" + path + "\" --level=input-level4.txt --tcp=7000");

            Process proc;

            if ((Process.Start(processInfo)) == null)
            {
                throw new InvalidOperationException("??");
            }

            /* start tcp client */

            tcpClient = new TcpClient("localhost", 7000);
            tcpClient.NoDelay = true;

            stream = tcpClient.GetStream();
        }

        public void disconnect()
        {
            /* kill client */

            if (tcpClient != null)
            {
                stream.Close();
                tcpClient.Close();
            }

            /* kill jar */

            foreach (Process proc in Process.GetProcessesByName("java"))
            {
                proc.Kill();
            }
        }

        public string read()
        {
            if (stream.DataAvailable)
            {
                byte[] buff = new byte[1024];
                int read = stream.Read(buff, 0, buff.Length);

                string responseData = Encoding.ASCII.GetString(buff, 0, read);

                return responseData;
            }
            else
            {
                return null;
            }
        }

        public void writeLine(string msg)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(msg + "\r\n");

            if (msg == "MOVE")
            {
                
            }
            else
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            
            if (!msg.Contains("MOVE"))
            {
                Console.WriteLine(":: write " + msg);
            }
        }
    }
}
