// Programmer: Nicholas Lersey 11633967
// <copyright file="SpreadsheetLoader.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace CPTS321
{
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Spreadsheet loading class.
    /// </summary>
    internal class SpreadsheetLoader
    {
        /// <summary>
        /// Loads spreadsheet from xml.
        /// </summary>
        /// <param name="stream">file stream.</param>
        /// <param name="spreadsheet">spreadsheet to be loaded.</param>
        public static void LoadXML(Stream stream, Spreadsheet spreadsheet)
        {
            StreamReader reader = new StreamReader(stream);
            XmlDocument document = new XmlDocument();
            try
            {
                ClearSpread(spreadsheet);
                document.LoadXml(reader.ReadToEnd());
                var scells = document.SelectNodes("//cell");
                foreach (XmlNode cellNode in scells)
                {
                    var cell = GetCell(spreadsheet, GetCellName(cellNode));
                    cell.BackgroundColor = GetCellColor(cellNode);
                    cell.Text = GetCellText(cellNode);
                }
            }
            catch
            {
                return;
            }
        }

        private static void ClearSpread(Spreadsheet spreadsheet)
        {
            for (int i = 0; i < spreadsheet.RowCount; ++i)
            {
                for (int j = 0; j < spreadsheet.Columncount; ++j)
                {
                    ClearCell(spreadsheet.GetCell(i + 1, j + 1));
                }
            }
        }

        private static void ClearCell(Cell cell)
        {
            cell.BackgroundColor = 0xFFFFFFFF;
            cell.Text = string.Empty;
        }

        private static Cell GetCell(Spreadsheet spreadsheet, string name)
        {
            int row, column;
            try
            {
                column = (int)char.Parse(name.Substring(0,1)) - (int)'A';
                row = int.Parse(name.Substring(1));
            }
            catch
            {
                throw new FormatException($"{name} has invalid format");
            }

            return spreadsheet.GetCell(row, column + 1);
        }

        private static string GetCellName(XmlNode cell)
        {
            foreach (XmlAttribute attr in cell.Attributes)
            {
                if (attr.Name == "name")
                {
                    return attr.Value;
                }
            }

            return null;
        }

        private static uint GetCellColor(XmlNode cell)
        {
            foreach (XmlNode node in cell.ChildNodes)
            {
                if (node.Name == "bgcolor")
                {
                    try
                    {
                        string s = node.Value;
                        return uint.Parse(node.InnerText, System.Globalization.NumberStyles.HexNumber);
                    }
                    catch
                    {
                        return 0xFFFFFFFF;
                    }
                }
            }

            return 0xFFFFFFFF;
        }

        private static string GetCellText(XmlNode cell)
        {
            foreach (XmlNode node in cell.ChildNodes)
            {
                if (node.Name == "text")
                {
                    return node.InnerText;
                }
            }

            return string.Empty;
        }

    }
}
