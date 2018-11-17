using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCC01
{
    public static class Level
    {
        public static int?[,] rodsDevX;
        public static int?[,] rodsDevY;

        public static int ROD_NUM_X = 100;
        public static int ROD_NUM_Y = 100;

        public static bool flip = true;

        static Level()
        {
            reinitRods();
        }

        public static void reinitRods()
        {
            rodsDevX = new int?[ROD_NUM_X + 1, ROD_NUM_Y + 1];
            rodsDevY = new int?[ROD_NUM_X + 1, ROD_NUM_Y + 1];
        }

        public static string buildMoveCommand()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("MOVE");

            for (int x = 1; x < ROD_NUM_X; x++)
            {
                for (int y = 1; y < ROD_NUM_Y; y++)
                {
                    if (rodsDevX[x, y] != null && rodsDevY[x, y] != null)
                    {
                        if (flip)
                        {
                            sb.Append(" " + x + " " + y + " " + rodsDevX[x, y] + " " + rodsDevY[x, y]);
                        }
                        else
                        {
                            sb.Append(" " + x + " " + y + " " + rodsDevY[x, y] + " " + rodsDevX[x, y]);
                        }
                    }
                }
            }

            return sb.ToString();
        }

        public static string lowerEverySecond(int v = 2)
        {
            //reinitRods();

            for (int x = 1; x < ROD_NUM_X + 1; x = x + 1)
            {
                for (int y = 1; y < ROD_NUM_Y + 1; y = y + v)
                {
                    rodsDevX[x, y] = 0;
                    rodsDevY[x, y] = 0;
                }
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string liftTheLowered(int to, int v = 2)
        {
            //reinitRods();

            for (int x = 2; x < ROD_NUM_X + 1; x = x + 1)
            {
                for (int y = 2; y < ROD_NUM_Y + 1; y = y + v)
                {
                    rodsDevX[x, y] = to;
                    rodsDevY[x, y] = 0;
                }
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string lowerInbetween(int v = 2)
        {
            //reinitRods();

            for (int x = 1; x < ROD_NUM_X + 1; x = x + 1)
            {
                for (int y = 1; y < ROD_NUM_Y + 1; y = y + v)
                {
                    rodsDevX[x, y] = null;
                    rodsDevY[x, y] = 0;
                }
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string moveLiftedTo(int to, int v = 2)
        {
            //reinitRods();

            for (int x = 2; x < ROD_NUM_X + 1; x = x + 1)
            {
                for (int y = 2; y < ROD_NUM_Y + 1; y = y + v)
                {
                    rodsDevX[x, y] = to;
                    rodsDevY[x, y] = 0;
                }
            }
            
            string str = buildMoveCommand();
            return str;
        }

        public static string liftTheLoweredTo0(int v = 2)
        {
            //reinitRods();

            for (int x = 1; x < ROD_NUM_X + 1; x = x + 1)
            {
                for (int y = 1; y < ROD_NUM_Y + 1; y = y + v)
                {
                    rodsDevX[x, y] = 0;
                    rodsDevY[x, y] = 0;
                }
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string lowerLiftedInPos5(int v = 2)
        {
            //reinitRods();

            for (int x = 2; x < ROD_NUM_X + 1; x = x + 1)
            {
                for (int y = 2; y < ROD_NUM_Y + 1; y = y + v)
                {
                    rodsDevX[x, y] = null;
                    rodsDevY[x, y] = 0;
                }
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string liftLowered(int v = 2)
        {
            //reinitRods();

            for (int x = 2; x < ROD_NUM_X + 1; x = x + 1)
            {
                for (int y = 2; y < ROD_NUM_Y + 1; y = y + v)
                {
                    rodsDevX[x, y] = 0;
                    rodsDevY[x, y] = 0;
                }
            }

            string str = buildMoveCommand();
            return str;
        }
    }
}
