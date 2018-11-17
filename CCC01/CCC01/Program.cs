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
        private static int wait = 10;

        static void Main(string[] args)
        {
            conn = new TcpConnection();

            conn.disconnect();
            conn.connect();

            Level.ROD_NUM = int.Parse(command("GET_NUMBER", true));
            Level.reinitRods();

            bool moveForward = true;
            bool fastForward = true;
            bool fastBackward = true;

            forward(2, true); 

            while (true)
            {
                if (moveForward)
                {
                    forward(2, false); 
                }
                else
                {
                    backward(2, false);
                }
                

                int pos = int.Parse(command("GET_POSITION", true));
                Console.WriteLine("::pos::" + pos);

                if (Math.Abs(pos - 940) <= 10 && moveForward)
                {
                    fastForward = false;
                }

                if (Math.Abs(pos - 520) <= 10 && !moveForward)
                {
                    fastBackward = false;
                }
                else if (Math.Abs(pos - 955) <= 3)
                {
                    moveForward = false;
                   
                }
                else if (Math.Abs(pos - 495) <= 3 && !moveForward)
                {
                    Console.WriteLine(command("EXIT", true));
                    break;
                }
            }
        }

        private static void forward(int v = 2, bool slow = false)
        {
            //Level.reinitRods();

            conn.writeLine(Level.lowerEverySecond(v));
            waitOkayAndSleep();

            conn.writeLine(Level.liftTheLowered(-5, v));
            waitOkayAndSleep();

            conn.writeLine(Level.lowerInbetween(v));
            waitOkayAndSleep();

            conn.writeLine(Level.moveLiftedTo(5, v));
            waitOkayAndSleep();

            conn.writeLine(Level.liftTheLoweredTo0(v));
            waitOkayAndSleep();

            if (!slow)
            {
                conn.writeLine(Level.lowerLiftedInPos5(v));
                waitOkayAndSleep();
            }

            conn.writeLine(Level.liftLowered(v));
            waitOkayAndSleep();
        }

        private static void backward(int v = 2, bool slow = false)
        {
            //Level.reinitRods();

            conn.writeLine(Level.lowerEverySecond(v));
            waitOkayAndSleep();

            conn.writeLine(Level.liftTheLowered(5, v));
            waitOkayAndSleep();

            conn.writeLine(Level.lowerInbetween(v));
            waitOkayAndSleep();
            
            conn.writeLine(Level.moveLiftedTo(-5, v));
            waitOkayAndSleep();
            
            conn.writeLine(Level.liftTheLoweredTo0(v));
            waitOkayAndSleep();

            if (!slow)
            {
                conn.writeLine(Level.lowerLiftedInPos5(v));
                waitOkayAndSleep();
            }

            conn.writeLine(Level.liftLowered(v));
            waitOkayAndSleep();
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

            string output = command(null, true);

            if (output.ToUpper().Contains("ERROR"))
            {
                
            }

            Console.WriteLine(output);
        }
    }
}
