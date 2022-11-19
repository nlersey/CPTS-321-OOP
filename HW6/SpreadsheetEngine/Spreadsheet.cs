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
   public class Spreadsheet
    {
        public Cell[,] TwoDSpreadSheetArray;
        public event PropertyChangedEventHandler PropertyChanged;

        //constuctor
        public Spreadsheet(int rows, int columns)
        {
            this.columnnumber = columns;
            this.rownumber = rows;
            TwoDSpreadSheetArray = new Cell[rows, columns];

            for (int rowi=0;rowi<rows;rowi++)
            {
                for (int coli=0;coli<columns;columns++)
                {
                    this.TwoDSpreadSheetArray[rowi, coli].PropertyChanged += CellPropertyChanged;
                }
            }

        }

        protected int columnnumber;
        protected int rownumber;

        public Cell GetCell(int row, int column)
        {
            //check to make sure doesn't exceed boundaries of spreadsheet
            if(row<=rownumber && row>0 && column>0 && column <=columnnumber)
            {
                return TwoDSpreadSheetArray[row, column];
            }
            else
            {
                return null;
            }
        }

        public int Columncount { get { return columnnumber; } }
        public int RowCount { get { return rownumber; } }

        private void CellPropertyChanged(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender; //cast sender
            
            //check if text doesnt start with =
            if(cell.Text[0]!='=')
            {
                cell.Value = cell.Text;
            }
            //case where a forumla is expected after the =
            else
            {
                //Support pulling the value from another cell. So if you see the text in the cell starting with ‘=’ then assume the remaining part is the name of the cell we need to copy a value from.

            }

        }



    }
}
