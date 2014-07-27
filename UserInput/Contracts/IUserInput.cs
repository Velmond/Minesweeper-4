// ********************************
// <copyright file="IUserInput.cs" company="Minesweeper4">
// Copyright (c) 2014 Telerik Academy. All rights reserved.
// </copyright>
//
// ********************************
namespace Minesweeper.UserInput.Contracts
{
    /// <summary>
    /// Defines how the input data is requested by the user.
    /// </summary>
    public interface IUserInput
    {
        string RequestCommand();

        string RequestUserName();
    }
}
