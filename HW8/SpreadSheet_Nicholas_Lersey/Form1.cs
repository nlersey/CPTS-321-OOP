// Programmer: Nicholas Lersey 11633967
// <copyright file="Form1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Form1 class.
    /// </summary>
    public partial class Form1 : Form
    {
        private Spreadsheet spreadSheetObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();
            this.spreadSheetObject = new Spreadsheet(50, 26);
            this.spreadSheetObject.PropertyChanged += this.SpreadsheetCellPropertyChanged;
        }

        /// <summary>
        /// Form1Loader.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event args.</param>
        private void Form1_Load(object sender, EventArgs e)
        {
            for (char columnLetter = 'A'; columnLetter <= 'Z'; columnLetter++)
            {
                this.dataGridView1.Columns.Add(columnLetter.ToString(), columnLetter.ToString());
            }

            for (int i = 1; i <= 50; i++)
            {
               this.dataGridView1.Rows.Add();
               this.dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();
            }

            this.spreadSheetObject.PropertyChanged += this.SpreadsheetCellPropertyChanged;
        }

        /// <summary>
        /// Spreadsheet text or color changed handler.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event arguements.</param>
        private void SpreadsheetCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            int row = (sender as Cell).RowIndex;
            int column = (sender as Cell).ColumnIndex;

            if (e.PropertyName == "Text")
            {
                this.dataGridView1[column - 1, row - 1].Value = (object)this.spreadSheetObject.GetCell(row, column).Value;
                string s = (string)this.dataGridView1[column - 1, row - 1].Value;
            }

            if (e.PropertyName == "BackgroundColor")
            {
                Color color = this.dataGridView1[column - 1, row - 1].Style.BackColor;
                DataGridViewCellStyle style = new DataGridViewCellStyle(this.dataGridView1[column - 1, row - 1].Style);
                style.BackColor = Color.FromArgb((int)(sender as Cell).BackGroundColor);
                this.dataGridView1[column - 1, row - 1].Style.ApplyStyle(style);
            }
        }

        private void Demo_Click(object sender, EventArgs e)
        {
            var rnd = new Random();
            int row, col;
            for (int i = 0; i < 50; i++)
            {
                row = rnd.Next(49);
                col = rnd.Next(25);
                this.dataGridView1.Rows[row].Cells[col].Value = "Hello world!";
                this.dataGridView1.Rows[i].Cells[1].Value = "This is cell B#" + (i + 1).ToString();
                this.dataGridView1.Rows[i].Cells[0].Value = "=B" + (i + 1).ToString();
            }
        }

        /// <summary>
        /// Event when cell is first edited.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event argument.</param>
        private void Datagrid1_BeginCellEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.SetGridText(e.RowIndex, e.ColumnIndex, this.GetCell(e.RowIndex, e.ColumnIndex).Value);
        }

        /// <summary>
        /// Event triggered when user has finished editing cell.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event arguement.</param>
        private void Datagridview1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.spreadSheetObject.History.ExecuteCommand(new TextCommand(this.GetCell(e.RowIndex, e.ColumnIndex), (string)this.dataGridView1[e.ColumnIndex, e.RowIndex].Value));
            this.SetGridText(e.RowIndex, e.ColumnIndex, this.GetCell(e.RowIndex, e.ColumnIndex).Value);
        }

        /// <summary>
        /// Sets cell text.
        /// </summary>
        /// <param name="row">cell row.</param>
        /// <param name="column">cell column.</param>
        /// <param name="value">cell value.</param>
        private void SetGridText(int row, int column, string value)
        {
            this.dataGridView1[column, row].Value = (object)value;
        }

        /// <summary>
        /// Change color tool strip menu click for bringing up color dialog.
        /// </summary>
        /// <param name="sender">sendr cell.</param>
        /// <param name="e">event arguements.</param>
        private void ChangeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.ChangeColor(dialog.Color);
            }
        }

        /// <summary>
        /// edit tool strip menu for preventing/enabling undo/redo.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event arguements.</param>
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.redoToolStripMenuItem.Text = "Redo " + this.spreadSheetObject.History.RedoPeek;
            this.undoToolStripMenuItem.Text = "Undo " + this.spreadSheetObject.History.UndoPeek;

            if (this.spreadSheetObject.History.RedoPeek == string.Empty)
            {
                this.redoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.redoToolStripMenuItem.Enabled = true;
            }

            if (this.spreadSheetObject.History.UndoPeek == string.Empty)
            {
                this.undoToolStripMenuItem.Enabled = false;
            }
            else
            {
                this.undoToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// Triggers change color for cell.
        /// </summary>
        /// <param name="color">color to be changed.</param>
        private void ChangeColor(Color color)
        {
            DataGridViewSelectedCellCollection cells = this.dataGridView1.SelectedCells;
            List<Cell> selectedCells = new List<Cell>();
            for (int i = 0; i < cells.Count; ++i)
            {
                selectedCells.Add(this.spreadSheetObject.GetCell(cells[i].RowIndex + 1, cells[i].ColumnIndex + 1));
            }

            this.spreadSheetObject.History.ExecuteCommand(new ChangeColorCommand(selectedCells.ToArray(), (uint)color.ToArgb()));
        }

        /// <summary>
        /// Data grid cell formatter for color.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event arguements.</param>
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.spreadSheetObject != null)
            {
                e.CellStyle.BackColor = Color.FromArgb((int)this.GetCell(e.RowIndex, e.ColumnIndex).BackGroundColor);
            }
        }

        /// <summary>
        /// Gets cell in spreadsheet by row, column.
        /// </summary>
        /// <param name="row">row of cell.</param>
        /// <param name="column">column of cell.</param>
        /// <returns>cell.</returns>
        private Cell GetCell(int row, int column)
        {
            try
            {
                return this.spreadSheetObject.GetCell(row + 1, column + 1);
            }
            catch
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Triggers undo from stack.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event arguements.</param>
        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadSheetObject.History.Undo();
        }

        /// <summary>
        /// Triggers redo from stack.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event arguements.</param>
        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadSheetObject.History.Redo();
        }
    }
}
