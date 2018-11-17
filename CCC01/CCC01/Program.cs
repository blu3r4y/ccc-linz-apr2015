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

            // number

            string get_number = command("GET_NUMBER", true);
            string[] get_number_split = get_number.Split(' ');

            Level.ROD_NUM_X = int.Parse(get_number_split[0]);
            Level.ROD_NUM_Y = int.Parse(get_number_split[1]);

            // shape

            string get_shape = command("GET_SHAPE", true);
            string[] get_shape_split = get_shape.Split(' ');

            int shape1x = int.Parse(get_shape_split[0]);
            int shape1y = int.Parse(get_shape_split[1]);

            int shape2x = int.Parse(get_shape_split[2]);
            int shape2y = int.Parse(get_shape_split[3]);

            int shape3x = int.Parse(get_shape_split[4]);
            int shape3y = int.Parse(get_shape_split[5]);

            int shape4x = int.Parse(get_shape_split[6]);
            int shape4y = int.Parse(get_shape_split[7]);

            Level.reinitRods();

            bool right = true;

            while (true)
            {
                    forward();
                

                // pos

                string get_position = command("GET_POSITION", true);
                string[] get_position_split = get_position.Split(' ');

                int posx = int.Parse(get_position_split[0]);
                int posy = int.Parse(get_position_split[1]);

                if (posx == 400 && posy == 40)
                {
                    Level.flip = false;
                }
                else if (posx == 400 && posy == 200)
                {
                   Console.WriteLine(command("EXIT", true));
                break;
                }

                Console.WriteLine("::pos::" + posx + ":" + posy);

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
            Console.WriteLine(output);
        }
    }
}
