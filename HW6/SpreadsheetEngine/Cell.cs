//Programmer: Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;




//namespace SpreadsheetEngine
namespace CPTS321
{
    public abstract class Cell : INotifyPropertyChanged
    {

        //constructor
        public Cell(int RowIndex, int ColumnIndex)
        {
            this.RowIndex = RowIndex;
            this.ColumnIndex = ColumnIndex;
        }

        //property RowIndex
        //Read only
        public int RowIndex {get;}

        //property ColumnIndex
        //Read only
        public int ColumnIndex {get;}

        //text property that repersents actual text typed into the the cell
        //member varaible must be marked as protected
        //getter can return the memeber varaible
        //setter does:
        //if text is being set to the exact same text then just ignore it and dont invoke the property change event below
        //if text is being changed to something new, then update the protected memeber varaible and fire the PropertyChanged event

        //protected varaible for cell text 
        protected string celltext;
        protected string cellvalue;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get { return this.celltext; }
            set
            {
                //no change to text, exit with return
                if(value == this.celltext)
                {
                    return;
                }
                else
                {
                    this.celltext = value;
                    OnPropertyChanged("Text");
                }
            }
        }
     
        public string Value
        {
            get { return this.cellvalue; }
            set ///come back to this step
            {
                
            }
        }

        protected void OnPropertyChanged(string text)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(text));
        }

    }
}
