// ********************************
// <copyright file="IGameFieldSave.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.Field.Contracts
{
    public interface IGameFieldSave
    {
        /// <summary>
        /// Gets or sets the memento that contains a saved game field
        /// </summary>
        GameFieldMemento SavedField { get; set; }
    }
}
