// Programmer: Nicholas Lersey 11633967
// <copyright file="ExpressionTreeNode.cs" company="PlaceholderCompany">
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
    /// Abstract ExpressionTreeNode class.
    /// </summary>
    public abstract class ExpressionTreeNode
    {
        /// <summary>
        /// Abstract Evaluate function.
        /// </summary>
        /// <returns>value of Evaluate function in child classes.</returns>
        public abstract double Evaluate();
    }
}
