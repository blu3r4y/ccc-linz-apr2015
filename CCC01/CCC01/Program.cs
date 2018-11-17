using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace CCC01
{
    class Program
    {
        private static TcpConnection conn;
        private static int wait = 1000;

        static void Main(string[] args)
        {
            conn = new TcpConnection();

            conn.disconnect();
            conn.connect();

            Level.ROD_NUM = int.Parse(command("GET_NUMBER", true)) - 1;

            Level.ROD_NUM = 120;
            Level.reinitRods();

            for (int x = 0; x < 130; x++)
            {
                Level.reinitRods();

                conn.writeLine(Level.lowerEverySecond());
                waitOkayAndSleep();

                conn.writeLine(Level.liftTheLoweredToM5());
                waitOkayAndSleep();

                conn.writeLine(Level.lowerInbetween());
                waitOkayAndSleep();

                conn.writeLine(Level.moveLiftedTo5());
                waitOkayAndSleep();

                conn.writeLine(Level.liftTheLoweredTo0());
                waitOkayAndSleep();

                conn.writeLine(Level.lowerLiftedInPos5());
                waitOkayAndSleep();

                conn.writeLine(Level.liftLowered());
                waitOkayAndSleep();

                int pos = int.Parse(command("GET_POSITION", true));

                if (pos > 1000)
                {
                    Console.WriteLine(command("EXIT", true));
                    break;
                }
            }
        }

        private static string command(string cmd, bool wait)
        {
            if (cmd != null) conn.writeLine(cmd);

            string buffer = "";

            do
            {
                // read
                buffer += conn.read();

                // write to console
                if (buffer.Contains("\n"))
                {
                    return buffer.Replace("\n", "");
                }
            }
            while (wait);

            return null;
        }

        private static void waitOkayAndSleep()
        {
            Thread.Sleep(wait);
            Console.WriteLine(command(null, true));
        }
    }
}
