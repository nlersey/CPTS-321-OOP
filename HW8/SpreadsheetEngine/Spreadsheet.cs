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
        private Cell[,] spreadsheetArray;

        private int columnnumber;
        private int rownumber;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rows">number of rows for spreadsheet.</param>
        /// <param name="columns">number of columns for spreadsheet.</param>
        public Spreadsheet(int rows, int columns)
        {
            this.columnnumber = columns;
            this.rownumber = rows;
            this.History = new History();
            this.spreadsheetArray = new SCell[rows, columns];

            for (int rowi = 0; rowi < rows; ++rowi)
            {
                for (int coli = 0; coli < columns; ++coli)
                {
                    this.spreadsheetArray[rowi, coli] = new SCell(rowi + 1, coli + 1);
                    this.spreadsheetArray[rowi, coli].PropertyChanged += this.PropertyChangedHandler;
                }
            }
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
            if (row <= this.RowCount && row > 0 && column > 0 && column <= this.RowCount)
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

        private void Evaluate(SCell expressionCell)
        {
            if (expressionCell.Text == string.Empty)
            {
                expressionCell.MValue = string.Empty;
                return;
            }

            if (expressionCell.Text != null)
            {
                if (expressionCell.Text[0] != '=')
                {
                    expressionCell.MValue = expressionCell.Text;
                }

                if (expressionCell.Text[0] == '=')
                {
                    try
                    {
                        expressionCell.ExpressionTreeObject = new ExpressionTree(expressionCell.Text.Substring(1));
                        var variables = expressionCell.ExpressionTreeObject.GetVariableNamesandValues();
                        foreach (var stringval in variables)
                        {
                            expressionCell.ExpressionTreeObject.SetVariable(stringval, double.Parse(this.GetCell(stringval).Value));
                            this.GetCell(stringval).PropertyChanged += expressionCell.RefernceChange;
                        }

                        expressionCell.MValue = expressionCell.ExpressionTreeObject.Evaluate().ToString();
                    }
                    catch
                    {
                        expressionCell.MValue = expressionCell.Text;
                    }
                }
                else
                {
                    expressionCell.MValue = expressionCell.Text;
                }
            }
        }
    }
}
