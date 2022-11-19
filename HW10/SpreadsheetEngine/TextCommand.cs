// Programmer: Nicholas Lersey 11633967
// <copyright file="TextCommand.cs" company="PlaceholderCompany">
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
    /// Class for changing text command.
    /// </summary>
    public class TextCommand : ICommand
    {
        private string oldText;
        private string newText;
        private Cell cell;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCommand"/> class.
        /// </summary>
        /// <param name="cell">cell being changed.</param>
        /// <param name="newText">new text for cell.</param>
        public TextCommand(Cell cell, string newText)
        {
            this.newText = newText;
            this.oldText = cell.Text;
            this.cell = cell;
        }

        /// <summary>
        /// Gets Title of command.
        /// </summary>
        public string Title => "Change Text";

        /// <summary>
        /// Executes text change.
        /// </summary>
        public void Execute()
        {
            this.cell.Text = this.newText;
        }

        /// <summary>
        /// Undoes Text change.
        /// </summary>
        public void Undo()
        {
            this.cell.Text = this.oldText;
        }
    }
}
