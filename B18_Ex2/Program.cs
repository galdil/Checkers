using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace B18_Ex2
{
    class Program
    {
        public static void Main()
        {
            Interface newgames = new Interface();
            newgames.Start();
            //Game checkers = new Game("afik", "gal", 8, 2);
            //checkers.PrintBoard(checkers.m_Board, 8, "afik");
            Console.ReadLine();
        }

    }
}
