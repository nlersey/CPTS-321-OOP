// Programmer: Nicholas Lersey 11633967
// <copyright file="SpreadsheetSaver.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace CPTS321
{
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Saves XML file for spreadsheet.
    /// </summary>
    internal class SpreadsheetSaver
    {
        /// <summary>
        /// Saves XML.
        /// </summary>
        /// <param name="stream">file stream</param>
        /// <param name="spreadsheet">spreadsheet object.</param>
        internal static void SaveXML(Stream stream, Spreadsheet spreadsheet)
        {
            XmlDocument document = new XmlDocument();
            var current = document.CreateElement("spreadsheet");
            for (int i = 1; i <= spreadsheet.RowCount; ++i)
            {
                for (int j = 1; j <= spreadsheet.Columncount; ++j)
                {
                    var tempCell = spreadsheet.GetCell(i, j);
                    if (!CellIsDefault(tempCell))
                    {
                        current.AppendChild(XmlSerialize(document, tempCell));
                    }
                }
            }

            document.AppendChild(current);
            document.Save(stream);
        }

        /// <summary>
        /// Serializes cell being passed in.
        /// </summary>
        /// <param name="document">xml document.</param>
        /// <param name="cell">cell being seralized.</param>
        /// <returns>seralized cell.</returns>
        private static XmlNode XmlSerialize(XmlDocument document, Cell cell)
        {
            var cellWriter = document.CreateElement("cell");
            cellWriter.Attributes.Append(document.CreateAttribute("name"));
            cellWriter.Attributes[0].Value = cell.CellID;
            var textWriter = document.CreateElement("text");
            textWriter.InnerText = cell.Text;
            cellWriter.AppendChild(textWriter);
            var colorWriter = document.CreateElement("bgcolor");
            colorWriter.InnerText = Convert.ToString(cell.BackgroundColor, 16).ToUpper();
            cellWriter.AppendChild(colorWriter);
            return cellWriter;
        }

        private static bool CellIsDefault(Cell cell)
        {
            return cell.BackgroundColor == 0xFFFFFFFF && cell.Text == string.Empty;
        }
    }
}
