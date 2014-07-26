// ********************************
// <copyright file="GameFieldSave.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.Saving
{
    using Contracts;

    /// <summary>
    /// Class that contains the memento that contains the saved game field
    /// </summary>
    public class GameFieldSave : IGameFieldSave
    {
        public GameFieldMemento SavedField { get; set; }
    }
}
