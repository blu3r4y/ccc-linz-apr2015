using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CCC01
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpConnection conn = new TcpConnection();

            conn.disconnect();
            conn.connect();

            string buffer = "";

            int wait = 2000;

            while (true)
            {
                // read
                buffer += conn.read();

                // write to console
                if (buffer.Contains("\n"))
                {
                    Console.WriteLine(buffer);
                    buffer = "";
                }

                // write
                conn.writeLine(Level.lowerEverySecond());
                Thread.Sleep(wait);
                
                conn.writeLine(Level.liftTheLoweredToM5());

                Thread.Sleep(wait);
                
                conn.writeLine(Level.lowerInbetween());

                Thread.Sleep(wait);

                conn.writeLine(Level.moveLiftedTo5());
                Thread.Sleep(wait);

                conn.writeLine(Level.liftTheLoweredTo0());
                Thread.Sleep(wait);

                conn.writeLine(Level.lowerLiftedInPos5());
                Thread.Sleep(wait);

                conn.writeLine(Level.liftLowered());
                Thread.Sleep(wait);

                Thread.Sleep(wait);

                conn.writeLine("EXIT");

                Thread.Sleep(wait);

                // read
                buffer += conn.read();

                // write to console
                if (buffer.Contains("\n"))
                {
                    Console.WriteLine(buffer);
                    buffer = "";
                }


                Thread.Sleep(Timeout.Infinite);
            }
        }
    }
}
