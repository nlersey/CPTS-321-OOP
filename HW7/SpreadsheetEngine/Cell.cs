//Programmer: Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

namespace CPTS321
{
    /// <summary>
    /// Cell class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// ExpressionTree Engine used to drive code.
        /// </summary>
        public ExpressionTree ExpressionTreeObject;

        private string celltext;
        private string cellvalue;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">Row index.</param>
        /// <param name="columnIndex">Column index.</param>
        public Cell(int rowIndex, int columnIndex) 
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
        }

        /// <summary>
        /// Event handlar for property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets row index.
        /// </summary>
        public int RowIndex { get; }

        /// <summary>
        /// Gets ColumnIndex.
        /// </summary>
        public int ColumnIndex { get; }

        /// <summary>
        /// Gets or Sets celltext.
        /// </summary>
        public string Text
        {
            get
            {
                return this.celltext;
            }

            set
            {
                if (value != this.celltext)
                {
                    this.celltext = value;
                    this.PropertyChangedFunction("Text");
                }
            }
        }

        /// <summary>
        /// Gets or Sets cellvalue.
        /// </summary>
        public string Value
        {
            get
            {
                return this.cellvalue;
            }

            set
            {
                if (value != this.cellvalue)
                {
                    this.cellvalue = value;
                    this.PropertyChangedFunction("Value");
                }
            }
        }

        /// <summary>
        /// Gets CellID used by EventRefernce.
        /// </summary>
        public string CellID
        {
            get
            {
                string cellID = ((char)((char)this.ColumnIndex + 'A' - (char)1)).ToString();
                cellID += this.RowIndex.ToString();
                return cellID;
            }
        }

        /// <summary>
        /// Refernced cells changed event.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">parameter.</param>
        public void EventRefernce(object sender, PropertyChangedEventArgs e)
        {
            this.cellvalue = this.ExpressionTreeObject.Evaluate().ToString();
            this.ExpressionTreeSetVar((sender as Cell).CellID, double.Parse((sender as Cell).cellvalue));
            if (sender as Cell != this)
            {
                this.PropertyChangedFunction("Cell");
            }
        }

        /// <summary>
        /// Creates New expression tree from string expression.
        /// </summary>
        /// <param name="expression">string expression.</param>
        public void NewExpressionTree(string expression)
        {
            this.ExpressionTreeObject = new ExpressionTree(expression);
        }

        /// <summary>
        /// EPasses Variable to expressiontree.
        /// </summary>
        /// <param name="variable">variable name.</param>
        /// <param name="value">passed in string value.</param>
        internal void ExpressionTreeSetVar(string variable, double value)
        {
            this.ExpressionTreeObject.SetVariable(variable, value);
        }

        /// <summary>
        /// Propertychanged event.
        /// </summary>
        /// <param name="text">type text.</param>
        protected void PropertyChangedFunction(string text)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(text));
        }
    }

    /// <summary>
    /// TCell initilization of Cell.
    /// </summary>
    public class TCell : Cell
    {
        public TCell(int row, int column)
            : base(row, column)
        {
        }
    }
}