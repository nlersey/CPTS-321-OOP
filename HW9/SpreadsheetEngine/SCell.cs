// Programmer: Nicholas Lersey 11633967
// <copyright file="SCell.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for altering spreadsheet abtract class.
    /// </summary>
    internal class SCell : Cell
    {
        /// <summary>
        /// Expression tree for evaluation.
        /// </summary>
        internal ExpressionTree ExpressionTreeObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="SCell"/> class.
        /// </summary>
        /// <param name="row">row for cell.</param>
        /// <param name="column">column for cell.</param>
        internal SCell(int row, int column)
            : base(row, column)
        {
        }

        /// <summary>
        /// Gets or sets the value of the cell.
        /// </summary>
        internal string MValue
        {
            get
            {
                return this.cellvalue;
            }

            set
            {
                if (this.cellvalue == value)
                {
                    return;
                }

                this.cellvalue = value;
            }
        }

        /// <summary>
        /// Caller for when referenced cells are changed.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event arguements.</param>
        internal void RefernceChange(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                this.ExpressionTreeSetVar((sender as SCell).CellID, float.Parse((sender as SCell).cellvalue));
                this.cellvalue = this.ExpressionTreeObject.Evaluate().ToString();
                if (sender as SCell != this)
                {
                    this.PropertyChangedFunction();
                }
            }
            catch
            {
                this.cellvalue = this.celltext;
            }
        }

        /// <summary>
        /// Sets varible in the tree.
        /// </summary>
        /// <param name="variable">variable string name.</param>
        /// <param name="value">value of the variable.</param>
        internal void ExpressionTreeSetVar(string variable, float value)
        {
            this.ExpressionTreeObject.SetVariable(variable, value);
        }
    }
}
