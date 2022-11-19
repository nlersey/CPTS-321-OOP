// Programmer: Nicholas Lersey 11633967
// <copyright file="ICommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ICommand interface.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets Name of command.
        /// </summary>
        string Title
        { get; }

        /// <summary>
        /// Execute Command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undo Command.
        /// </summary>
        void Undo();
    }
}
