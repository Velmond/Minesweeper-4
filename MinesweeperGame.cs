namespace Minesweeper
{
    using System;

    public class MinesweeperGame
    {
        static void Main()
        {
            var engine = Engine.Instance;
            engine.Start();
            
            // Console.Clear();
            Console.WriteLine("Made by Pavlin Panev 2010 - all rights reserved!");

            // Console.WriteLine("Press any key to exit.");
            // Console.Read();
        }
    }
}
