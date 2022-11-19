//Programmer: Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace CPTS321
{
    /// <summary>
    /// Spreadsheet class.
    /// </summary>
   public class Spreadsheet
    {

        public Cell[,] SpreadsheetArray;

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
            this.SpreadsheetArray = new TCell[rows, columns];
            for (int rowi = 0; rowi < rows; rowi++)
            {
                for (int coli = 0; coli < columns; coli++)
                {
                    this.SpreadsheetArray[rowi, coli] = new TCell(rowi, coli);
                    this.SpreadsheetArray[rowi, coli].PropertyChanged += this.PropertyChangedCell;
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
            get { return this.columnnumber; }
        }

        /// <summary>
        /// Gets rownumber.
        /// </summary>
        public int RowCount
        {
            get { return this.rownumber; }
        }


        /// <summary>
        /// Finds cell in spreadsheet by row and column.
        /// </summary>
        /// <param name="row">row number of cell.</param>
        /// <param name="column">column number of cell.</param>
        /// <returns>null or the cell.</returns>
        public Cell GetCell(int row, int column)
        {
            if (row <= this.rownumber && row > 0 && column > 0 && column <= this.columnnumber)
            {
                return this.SpreadsheetArray[row - 1, column - 1];
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
            int row;
            int column;
            row = int.Parse(name.Substring(1));
            column = (int)char.Parse(name.Substring(0, 1)) - (int)'A';
            return this.GetCell(row, column + 1);
        }

        /// <summary>
        /// Cell Changed event used to trigger evluation.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">parameter.</param>
        private void PropertyChangedCell(object sender, EventArgs e)
        {
            if (sender is TCell)
            {
                TCell expressionCell = (TCell)sender;

                if (expressionCell.Text != null)
                {
                    if (expressionCell.Text[0] != '=')
                    {
                        expressionCell.Value = expressionCell.Text;
                    }
                    else
                    {
                        try
                        {
                            expressionCell.ExpressionTreeObject = new ExpressionTree(expressionCell.Text.Substring(1));
                            var variables = expressionCell.ExpressionTreeObject.GetVariableNamesandValues();
                            foreach (var stringval in variables)
                            {
                                expressionCell.ExpressionTreeObject.SetVariable(stringval, double.Parse(this.GetCell(stringval).Value));
                                this.GetCell(stringval).PropertyChanged += expressionCell.EventRefernce;
                            }

                            expressionCell.Value = expressionCell.ExpressionTreeObject.Evaluate().ToString();
                        }
                        catch
                        {
                            expressionCell.Value = expressionCell.Text;
                        }
                    }
                }
                else
                {
                    expressionCell.Value = string.Empty;
                }
            }
        }
    }
}
