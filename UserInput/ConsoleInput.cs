// ********************************
// <copyright file="ConsoleInput.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.UserInput
{
    using System;
    using Contracts;

    /// <summary>
    /// Implements IUserInput for a console application.
    /// </summary>
    public class ConsoleInput : IUserInput
    {
        public string RequestCommand()
        {
            return Console.ReadLine();
        }

        public string RequestUserName()
        {
            string name = String.Empty;

            do
            {
                name = Console.ReadLine();
            }
            while(name == String.Empty);

            return name;
        }
    }
}
