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
        /// <summary>
        /// Requests a command to be executed
        /// </summary>
        /// <returns>The command that is inputted as a string</returns>
        string RequestCommand();

        /// <summary>
        /// Requests the user to input his/her username
        /// </summary>
        /// <returns>The username that is inputted as a string</returns>
        string RequestUserName();
    }
}
