// Programmer: Nicholas Lersey 11633967
// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace CPTS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// OperatorNodeFactory class.
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// Switch function used to select instance of particular operator node.
        /// </summary>
        /// <param name="c">passed in operator char from expressiontree.</param>
        /// <returns>New instance of a particular operator node.</returns>
        public static OperatorNode NewOperatorNode(char c)
        {
            switch (c)
            {
                case '/':
                    return new DivisionOperatorNode();
                case '*':
                    return new MutiplicationOperatorNode();
                case '+':
                    return new AdditionOperatorNode();
                case '-':
                    return new SubtractionOperatorNode();
            }

            return null;
        }
    }
}
