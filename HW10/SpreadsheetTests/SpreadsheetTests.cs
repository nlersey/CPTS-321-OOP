// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ComponentModel;
using CPTS321;

namespace CPTS321.Tests
{
    [TestFixture]
    public class SpreadsheetTests
    {
        /// <summary>
        /// Tests constructor for Cell. 
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            TestingCell cell = new TestingCell(5, 5);
            Assert.AreEqual(5, cell.ColumnIndex);
            Assert.AreEqual(5, cell.RowIndex);
        }

        /// <summary>
        /// Tests color of cell.
        /// </summary>
        [Test]
        public void TestColor()
        {
            TestingCell cell = new TestingCell(1, 1);
            List<string> recievedEvents = new List<string>();
            cell.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                recievedEvents.Add(e.PropertyName);
            };
            Assert.AreEqual(0xFFFFFFFF, cell.BackgroundColor);
            cell.BackgroundColor = 0xAAAAAAAA;
            Assert.AreEqual(0xAAAAAAAA, cell.BackgroundColor);
            Assert.AreEqual(1, recievedEvents.Count);
            Assert.AreEqual("BackgroundColor", recievedEvents[0]);
        }

        /// <summary>
        /// Tests changing text of a cell.
        /// </summary>
        [Test]
        public void TestText()
        {
            List<string> recievedEvents = new List<string>();
            TestingCell c = new TestingCell(4, 1);
            c.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                recievedEvents.Add(e.PropertyName);
            };
            c.Text = "Hello";
            Assert.AreEqual("Hello", c.Text);
            Assert.AreEqual("Text", recievedEvents[0]);
            recievedEvents.Clear();
            c.Text = "Hello";
            Assert.AreEqual(0, recievedEvents.Count);
        }

        /// <summary>
        /// Tests CellIdentity
        /// </summary>
        [Test]
        public void TestCellName()
        {
            TestingCell t = new TestingCell(2, 2);
            Assert.AreEqual("B2", t.CellID);
            var t2 = new TestingCell(4, 4);
            Assert.AreEqual("D4", t2.CellID);
        }

        /// <summary>
        /// Tests undo stack feature.
        /// </summary>
        [Test]
        public void TestTextExecuteUndo()
        {
            TestingCell cell = new TestingCell(1, 1);
            Assert.AreEqual(string.Empty, cell.Text);
            TextCommand com = new TextCommand(cell, "Hello");
            com.Execute();
            Assert.AreEqual("Hello", cell.Text);
            com.Undo();
            Assert.AreEqual(string.Empty, cell.Text);
        }

        /// <summary>
        /// Tests changing color of a cell.
        /// </summary>
        [Test]
        public void TestChangeColor()
        {
            //test uniform color
            TestingCell[] cells = new TestingCell[3];
            for (int i = 0; i < 3; i++)
            {
                cells[i] = new TestingCell(1, i + 1);
            }
            ChangeColorCommand com = new ChangeColorCommand(cells, 0x33333333);
            com.Execute();
            foreach (Cell c in cells)
            {
                Assert.AreEqual(0x33333333, c.BackgroundColor);
            }

            com.Undo();
            cells[0].BackgroundColor = 0xAAAAAAAA;
            com = new ChangeColorCommand(cells, 0xEEEEEEEE);
            com.Execute();
            foreach (Cell c in cells)
            {
                Assert.AreEqual(0xEEEEEEEE, c.BackgroundColor);
            }
            com.Undo();
            Assert.AreEqual(0xAAAAAAAA, cells[0].BackgroundColor);
            Assert.AreEqual(0xFFFFFFFF, cells[1].BackgroundColor);
            Assert.AreEqual(0xFFFFFFFF, cells[2].BackgroundColor);
        }

        /// <summary>
        /// Tests xml loader.
        /// </summary>
        [Test]
        public void TestLoad()
        {
            string xml = "<spreadsheet><cell name=\"B1\"><bgcolor>FF8000FF</bgcolor><text>=A1+6</text></cell></spreadsheet>";
            MemoryStream filestream = new MemoryStream(Encoding.ASCII.GetBytes(xml));

            Spreadsheet spreadsheet = new Spreadsheet(20, 20);
            spreadsheet.LoadFile(filestream);
            Assert.NotNull(spreadsheet.GetCell(2, 2));
            Assert.AreEqual("B1", spreadsheet.GetCell(1, 2).CellID);
            Assert.AreEqual(0xFF8000FF, spreadsheet.GetCell(1, 2).BackgroundColor);
            Assert.AreEqual("=A1+6", spreadsheet.GetCell(1, 2).Text);
        }

        /// <summary>
        /// Tests xml saver.
        /// </summary>
        [Test]
        public void TestSave()
        {
            Spreadsheet sheet = new Spreadsheet(10, 10);
            MemoryStream stream = new MemoryStream(new byte[1000]);

            sheet.GetCell(1, 1).BackgroundColor = 0xFF00FF00;

            sheet.GetCell(2, 2).Text = "I'MSOBURNTOUT";

            sheet.SaveFile(stream);

            var sheet2 = new Spreadsheet(10, 10);
            stream.Position = 0;
            sheet2.LoadFile(stream);
            Assert.AreEqual(sheet.GetCell(1, 1).BackgroundColor, sheet2.GetCell(1, 1).BackgroundColor);
            Assert.AreEqual(sheet.GetCell(2, 2).Text, sheet.GetCell(2, 2).Text);


        }

        /// <summary>
        /// tests cell's refernce.
        /// </summary>
        [Test]
        public void TestReference()
        {
            Spreadsheet sheet = new Spreadsheet(10, 10);
            Assert.NotNull(sheet.GetCell(1, 1));
            Assert.NotNull(sheet.GetCell(1, 2));
            sheet.GetCell(1, 1).Text = "4";
            sheet.GetCell(1, 2).Text = "=A1";
            Assert.AreEqual(sheet.GetCell(1,1).Value, sheet.GetCell(1,2).Value);
        }

        /// <summary>
        /// Tests cell's propertychange when refernence changes.
        /// </summary>
        [Test]
        public void TestReferencePropertyChange()
        {
            Spreadsheet sheet = new Spreadsheet(10, 10);
            Assert.NotNull(sheet.GetCell(1, 1));
            Assert.NotNull(sheet.GetCell(1, 2));
            sheet.GetCell(1, 1).Text = "41";
            sheet.GetCell(1, 2).Text = "=A1";
            Assert.AreEqual(sheet.GetCell(1, 1).Value, sheet.GetCell(1, 2).Value);
            sheet.GetCell(1, 1).Text = "14";
            Assert.AreEqual(sheet.GetCell(1, 1).Value, sheet.GetCell(1, 2).Value);

        }

        /// <summary>
        /// Tests circular refernce. 
        /// </summary>
        [Test]
        public void TestCircularRefernce()
        {
            Spreadsheet sheet = new Spreadsheet(10, 10);
            Assert.NotNull(sheet.GetCell(1, 1));
            Assert.NotNull(sheet.GetCell(1, 2));
            sheet.GetCell(1, 1).Text = "=B1";
            sheet.GetCell(1, 2).Text = "=A1";
            Assert.AreEqual(sheet.GetCell(1, 2).Value, "!(circular reference)");
        }

        /// <summary>
        /// tests if cell can be dereferneced after being made a cirularrefernce.
        /// </summary>
        [Test]
        public void TestDeCircularRefernce()
        {
            Spreadsheet sheet = new Spreadsheet(10, 10);
            Assert.NotNull(sheet.GetCell(1, 1));
            Assert.NotNull(sheet.GetCell(1, 2));
            sheet.GetCell(1, 1).Text = "=B1";
            sheet.GetCell(1, 2).Text = "=A1";
            Assert.AreEqual(sheet.GetCell(1, 2).Value, "!(circular reference)");
            sheet.GetCell(1, 2).Text = "44";
            Assert.AreNotEqual(sheet.GetCell(1, 2).Value, "!(circular reference)");
        }
    }
}
