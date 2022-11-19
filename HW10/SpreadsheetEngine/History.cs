// Programmer: Nicholas Lersey 11633967
// <copyright file="History.cs" company="PlaceholderCompany">
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
    /// Class for command history.
    /// </summary>
    public class History
    {
        private Stack<ICommand> redoStack;
        private Stack<ICommand> undoStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="History"/> class.
        /// </summary>
        public History()
        {
            this.redoStack = new Stack<ICommand>();
            this.undoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Gets the title of command on top of the stack.
        /// </summary>
        public string RedoPeek
        {
            get
            {
                if (this.redoStack.Count != 0)
                {
                    return this.redoStack.Peek().Title;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the title of command on top of the stack.
        /// </summary>
        public string UndoPeek
        {
            get
            {
                if (this.undoStack.Count != 0)
                {
                    return this.undoStack.Peek().Title;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Redoes previous command.
        /// </summary>
        public void Redo()
        {
            if (this.redoStack.Count != 0)
            {
                ICommand command = this.redoStack.Pop();
                command.Execute();
                this.undoStack.Push(command);
            }
        }

        /// <summary>
        /// Undoes previous command.
        /// </summary>
        public void Undo()
        {
            if (this.undoStack.Count != 0)
            {
                ICommand command = this.undoStack.Pop();
                command.Undo();
                this.redoStack.Push(command);
            }
        }

        /// <summary>
        /// Executes a command, adds it to undo stack, clears redo stack.
        /// </summary>
        /// <param name="command">Command to be executed.</param>
        public void ExecuteCommand(ICommand command)
        {
            this.redoStack.Clear();
            command.Execute();
            this.undoStack.Push(command);
        }

        /// <summary>
        /// Clears stacks when load is called.
        /// </summary>
        public void Clear()
        {
            this.undoStack.Clear();
            this.undoStack.Clear();
        }
    }
}
