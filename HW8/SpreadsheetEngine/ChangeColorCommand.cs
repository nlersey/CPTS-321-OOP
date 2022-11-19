// Programmer: Nicholas Lersey 11633967
// <copyright file="ChangeColorCommand.cs" company="PlaceholderCompany">
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
    /// ColorCommand based on ICommand to change cell color.
    /// </summary>
    public class ChangeColorCommand : ICommand
    {
        private Dictionary<Cell, uint> orginalcolors;
        private Cell[] cells;
        private uint color;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeColorCommand"/> class.
        /// </summary>
        /// <param name="cells">Cells refernce list.</param>
        /// <param name="color">New color for change.</param>
        public ChangeColorCommand(Cell[] cells, uint color)
        {
            this.orginalcolors = new Dictionary<Cell, uint>();
            foreach (Cell cell in cells)
            {
                this.orginalcolors[cell] = cell.BackGroundColor;
            }

            this.cells = cells;
            this.color = color;
        }

        /// <summary>
        /// Gets Title Command for Changed Cell color.
        /// </summary>
        public string Title => "Changed Cell Color";

        /// <summary>
        /// Execute command for color change.
        /// </summary>
        public void Execute()
        {
            foreach (Cell cell in this.cells)
            {
                cell.BackGroundColor = this.color;
            }
        }

        /// <summary>
        /// Undo command for undoing a color change to cell.
        /// </summary>
        public void Undo()
        {
            foreach (Cell cell in this.cells)
            {
                cell.BackGroundColor = this.orginalcolors[cell];
            }
        }
    }
}
