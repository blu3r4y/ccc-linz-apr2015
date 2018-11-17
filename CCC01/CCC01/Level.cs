using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCC01
{
    public static class Level
    {
        public static int?[] rods;
        public const int ROD_NUM = 100;

        static Level()
        {
            reinitRods();
        }

        public static void reinitRods()
        {
            rods = new int?[ROD_NUM + 1];
        }

        public static string buildMoveCommand()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("MOVE");

            for (int i = 1; i < rods.Length; i++)
            {
                if (rods[i] != null)
                {
                    sb.Append(" " + i + " " + rods[i]);
                }
            }

            return sb.ToString();
        }

        public static string lowerEverySecond()
        {
            reinitRods();

            for (int i = 1; i < ROD_NUM + 1; i = i + 2)
            {
                rods[i] = 0;
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string liftTheLoweredToM5()
        {
            //reinitRods();

            for (int i = 2; i < ROD_NUM + 1; i = i + 2)
            {
                rods[i] = -5;
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string lowerInbetween()
        {
            //reinitRods();

            for (int i = 1; i < ROD_NUM + 1; i = i + 2)
            {
                rods[i] = null;
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string moveLiftedTo5()
        {
            //reinitRods();

            for (int i = 2; i < ROD_NUM + 1; i = i + 2)
            {
                rods[i] = 5;
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string liftTheLoweredTo0()
        {
            //reinitRods();

            for (int i = 1; i < ROD_NUM + 1; i = i + 2)
            {
                rods[i] = 0;
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string lowerLiftedInPos5()
        {
            //reinitRods();

            for (int i = 2; i < ROD_NUM + 1; i = i + 2)
            {
                rods[i] = null;
            }

            string str = buildMoveCommand();
            return str;
        }

        public static string liftLowered()
        {
            //reinitRods();

            for (int i = 2; i < ROD_NUM + 1; i = i + 2)
            {
                rods[i] = 0;
            }

            string str = buildMoveCommand();
            return str;
        }
    }
}
