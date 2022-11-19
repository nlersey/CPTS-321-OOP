// Programmer: Nicholas Lersey 11633967
// <copyright file="Cell.cs" company="PlaceholderCompany">
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
    /// Cell class.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// cell text.
        /// </summary>
        protected string celltext;

        /// <summary>
        /// cell value.
        /// </summary>
        protected string cellvalue;

        private uint cellBGColor;
        private int row;
        private int column;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="rowIndex">Row index.</param>
        /// <param name="columnIndex">Column index.</param>
        public Cell(int rowIndex, int columnIndex)
        {
            this.row = rowIndex;
            this.column = columnIndex;
            this.cellBGColor = 0xFFFFFFFF;
            this.cellvalue = string.Empty;
            this.celltext = string.Empty;
        }

        /// <summary>
        /// Event handlar for property.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets RowIndex.
        /// </summary>
        public int RowIndex
        {
            get
            {
                return this.row;
            }
        }

        /// <summary>
        /// Gets ColumnIndex.
        /// </summary>
        public int ColumnIndex
        {
            get
            {
                return this.column;
            }
        }

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
                    this.PropertyChangedFunction();
                }
            }
        }

        /// <summary>
        /// Gets cellvalue.
        /// </summary>
        public string Value
        {
            get
            {
                return this.cellvalue;
            }
        }

        /// <summary>
        /// Gets CellID used by EventRefernce.
        /// </summary>
        public string CellID
        {
            get
            {
                string cellID = ((char)((char)this.column + 'A' - (char)1)).ToString();
                cellID += this.RowIndex.ToString();
                return cellID;
            }
        }

        /// <summary>
        /// Gets or Sets cell BackgroundColor.
        /// </summary>
        public uint BackgroundColor
        {
            get
            {
                return this.cellBGColor;
            }

            set
            {
                if (value != this.cellBGColor)
                {
                    this.cellBGColor = value;
                    this.PropertyChangedFunction();
                }
            }
        }

        /// <summary>
        /// Property changed event invoker.
        /// </summary>
        /// <param name="name">Name of property triggered.</param>
        protected void PropertyChangedFunction([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}