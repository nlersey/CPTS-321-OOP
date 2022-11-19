using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321.Tests
{
    internal class TestingCell : Cell
    {
        /// <summary>
        /// method to create Cell for testcases since cell is protected.
        /// </summary>
        /// <param name="row">row of cell</param>
        /// <param name="column">column of cell.</param>
        public TestingCell(int row, int column) : base(row, column)
        { }
    }
}
