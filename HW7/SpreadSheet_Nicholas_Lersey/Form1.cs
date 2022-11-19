//Programmer: Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPTS321
{
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

        private void SpreadsheetCellPropertyChanged(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            this.dataGridView1[cell.ColumnIndex, cell.RowIndex].Value = this.spreadSheetObject.SpreadsheetArray[cell.RowIndex, cell.ColumnIndex].Value;
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
        /// event when cell is first edited.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event.</param>
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.dataGridView1[e.ColumnIndex, e.RowIndex].Value = this.spreadSheetObject.SpreadsheetArray[e.RowIndex, e.ColumnIndex].Text;
        }

        /// <summary>
        /// event triggered when user has finished editing cell.
        /// </summary>
        /// <param name="sender">sender cell.</param>
        /// <param name="e">event.</param>
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.spreadSheetObject.SpreadsheetArray[e.RowIndex, e.ColumnIndex].Text = this.dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(); 
            this.dataGridView1[e.ColumnIndex, e.RowIndex].Value = this.spreadSheetObject.SpreadsheetArray[e.RowIndex, e.ColumnIndex].Value;
        }

    }
}
