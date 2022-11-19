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
    public class SCell : Cell
    {
        /// <summary>
        /// Dictonary for spreadsheet cells to grab variables for cell refernces.
        /// </summary>
        public Dictionary<string, double> CellVariables = new Dictionary<string, double>();

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
        /// Takes in expression string from evaulate in spreadsheet, creates a treeobject for that expression so that CellVarialbes can be populated.
        /// </summary>
        /// <param name="expression">passed in expression to be turned into an expression treee so CellVariables can be populated.</param>
        public void PopulateDictonary(string expression)
        {
            this.ExpressionTreeObject = new ExpressionTree(expression);
            this.CellVariables = this.ExpressionTreeObject.Cellvariables;
            this.ExpressionTreeObject.Treecell = this;
        }

        /// <summary>
        /// creates refernce to cell and updates value.
        /// </summary>
        /// <param name="cell">sender cell.</param>
        internal void CreateRefernce(Cell cell)
        {
            cell.PropertyChanged += this.RefernceChange;
            if (this.CellVariables.ContainsKey(cell.CellID))
            {
                if (double.TryParse(cell.Value, out double num))
                {
                    this.CellVariables[cell.CellID] = num;
                }
                else
                {
                    this.CellVariables[cell.CellID] = 0.0;
                }
            }
        }

        /// <summary>
        /// Dereferences the cell from orginal refernced cells.
        /// </summary>
        /// <param name="cell">cell to be derefernced.</param>
        internal void DeRefernce(Cell cell)
        {
            cell.PropertyChanged -= this.RefernceChange;
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
