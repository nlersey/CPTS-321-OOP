// Programmer: Nicholas Lersey 11633967
// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Spreadsheet class.
    /// </summary>
    public class Spreadsheet
    {
        /// <summary>
        /// spreadsheetArray object.
        /// </summary>
        private SCell[,] spreadsheetArray;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">number of rows for spreadsheet.</param>
        /// <param name="columns">number of columns for spreadsheet.</param>
        public Spreadsheet(int rows, int columns)
        {
            this.InitiateCells(rows, columns);
            this.History = new History();
        }

        /// <summary>
        /// Event propagator.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Gets columnnumber.
        /// </summary>
        public int Columncount
        {
            get
            {
                return this.spreadsheetArray.GetLength(1);
            }
        }

        /// <summary>
        /// Gets rownumber.
        /// </summary>
        public int RowCount
        {
            get
            {
                return this.spreadsheetArray.GetLength(0);
            }
        }

        /// <summary>
        /// Gets Command History.
        /// </summary>
        public History History
        {
            get;
            private set;
        }

        /// <summary>
        /// Finds cell in spreadsheet by row and column.
        /// </summary>
        /// <param name="row">row number of cell.</param>
        /// <param name="column">column number of cell.</param>
        /// <returns>null or the cell.</returns>
        public Cell GetCell(int row, int column)
        {
            if (row > 0 && row <= this.RowCount && column > 0 && column <= this.RowCount)
            {
                return this.spreadsheetArray[row - 1, column - 1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Finds cell by name.
        /// </summary>
        /// <param name="name">name of cell.</param>
        /// <returns>null or the cell.</returns>
        public Cell GetCell(string name)
        {
            int column, row;
            try
            {
                column = (int)char.Parse(name.Substring(0, 1)) - (int)'A';
                row = int.Parse(name.Substring(1));
            }
            catch
            {
                throw new FormatException($"{name} has invalid format");
            }

            return this.GetCell(row, column + 1);
        }

        /// <summary>
        /// Property changed Handler.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">Event arguements.</param>
        public void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                this.Evaluate(sender as SCell);
            }

            this.PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(e.PropertyName));
        }

        /// <summary>
        /// Passes stream from form1 to spreadsheet saving class.
        /// </summary>
        /// <param name="s">file stream.</param>
        public void SaveFile(Stream s)
        {
            SpreadsheetSaver.SaveXML(s, this);
        }

        /// <summary>
        /// Passes stream from form1 to spreadsheet saving class.
        /// </summary>
        /// <param name="s">file stream.</param>
        public void LoadFile(Stream s)
        {
            SpreadsheetLoader.LoadXML(s, this);
        }

        /// <summary>
        /// Evaluate function call in the event of text propertychange.
        /// </summary>
        /// <param name="expressionCell">Cell triggered by event and to be evaluated.</param>
        private void Evaluate(SCell expressionCell)
        {
            if (expressionCell.Text == string.Empty)
            {
                expressionCell.MValue = string.Empty;
                return;
            }

            if (expressionCell.Text != null)
            {
                if (expressionCell.Text[0] == '=')
                {
                    try
                    {
                        expressionCell.ExpressionTreeObject = new ExpressionTree(expressionCell.Text.Substring(1));
                        var variables = expressionCell.ExpressionTreeObject.GetVariableNamesandValues();
                        expressionCell.PopulateDictonary(expressionCell.Text.Substring(1));

                        if (!this.IsSelfRefernce(expressionCell) && !this.IsCircularRefernce(expressionCell))
                        {
                            foreach (var stringval in variables)
                            {
                                try
                                {
                                    expressionCell.ExpressionTreeObject.SetVariable(stringval, double.Parse(this.GetCell(stringval).Value));
                                }
                                catch
                                {
                                    expressionCell.ExpressionTreeObject.SetVariable(stringval, 0.0);
                                }

                                expressionCell.CreateRefernce(this.GetCell(stringval));
                            }

                            expressionCell.MValue = expressionCell.ExpressionTreeObject.Evaluate().ToString();
                        }
                    }
                    catch
                    {
                        expressionCell.MValue = expressionCell.Text;
                    }
                }
                else
                {
                    if (expressionCell.CellVariables.Count > 0)
                    {
                        foreach (var cellname in expressionCell.CellVariables.Keys.ToList())
                        {
                            expressionCell.DeRefernce(this.GetCell(cellname));
                        }
                    }

                    expressionCell.MValue = expressionCell.Text;
                }
            }
        }

        /// <summary>
        /// Checks if cell is a self refernce.
        /// </summary>
        /// <param name="expressioncell">cell to be checked for refernce.</param>
        /// <returns>bool based on if or isn't a self refernce.</returns>
        private bool IsSelfRefernce(SCell expressioncell)
        {
            var variables = expressioncell.ExpressionTreeObject.GetVariableNamesandValues();
            foreach (var stringval in variables)
            {
                if (stringval == expressioncell.CellID)
                {
                    expressioncell.MValue = "!(self reference)";
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if cell is a circular refernce.
        /// </summary>
        /// <param name="expressioncell">cell to be checked for refernce.</param>
        /// <returns>bool based on if circular refernce or not.</returns>
        private bool IsCircularRefernce(SCell expressioncell)
        {
            var variables = expressioncell.ExpressionTreeObject.GetVariableNamesandValues();
            foreach (var stringval in variables)
            {
                if (this.IsCircularRefernceHelper(stringval, expressioncell.CellID))
                {
                    expressioncell.MValue = "!(circular reference)";
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Recursive helper function to checks each cell's variable dictonary to see if any refernced cells are orginal cell.
        /// </summary>
        /// <param name="celltobechecked">current cell on stack to compare to orginal cell.</param>
        /// <param name="orginalcell">orginal cell passed from IsCircularRefernce.</param>
        /// <returns>bool based on if or isnt a circular refernce.</returns>
        private bool IsCircularRefernceHelper(string celltobechecked, string orginalcell)
        {
            SCell expressioncell = (SCell)this.GetCell(celltobechecked);
            foreach (var cellname in expressioncell.CellVariables.Keys.ToList())
            {
                if (cellname == orginalcell)
                {
                    return true;
                }

                return this.IsCircularRefernceHelper(cellname, orginalcell);
            }

            return false;
        }

        /// <summary>
        /// Creates instances of each cell for the spreadsheet.
        /// </summary>
        /// <param name="row">rows of spreadsheet.</param>
        /// <param name="column">columns of spreadsheet.</param>
        private void InitiateCells(int row, int column)
        {
            this.spreadsheetArray = new SCell[row, column];
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < column; ++j)
                {
                    this.spreadsheetArray[i, j] = new SCell(i + 1, j + 1);
                    this.spreadsheetArray[i, j].PropertyChanged += this.PropertyChangedHandler;
                }
            }
        }
    }
}
