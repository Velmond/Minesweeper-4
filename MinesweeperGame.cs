namespace Minesweeper
{
    using System;

    public class MinesweeperGame
    {
        public static void Main()
        {
            var engine = Engine.Instance;

            engine.Start();
            
            Console.WriteLine("Made by Pavlin Panev 2010 - all rights reserved!");
        }
    }
}
