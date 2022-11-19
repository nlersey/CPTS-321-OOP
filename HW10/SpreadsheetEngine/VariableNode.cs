// Programmer: Nicholas Lersey 11633967
// <copyright file="VariableNode.cs" company="PlaceholderCompany">
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
    /// VariableNode class.
    /// </summary>
    internal class VariableNode : ExpressionTreeNode
    {
        private readonly Dictionary<string, double> variableDictionary;
        private readonly string variableName;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="variables">variables dictionary refernce.</param>
        /// <param name="name">name of Variable.</param>
        public VariableNode(ref Dictionary<string, double> variables, string name)
        {
            this.variableDictionary = variables;
            this.variableName = name;
        }

        /// <summary>
        /// Gets variableName.
        /// </summary>
        public string GetsvariableName
        {
            get { return this.variableName; }
        }

        /// <summary>
        /// Overrideen Evaluate function.
        /// </summary>
        /// <returns>Returns key from Variable Dictonary.</returns>
        public override double Evaluate()
        {
            if (this.variableDictionary.ContainsKey(this.variableName))
            {
                return this.variableDictionary[this.variableName];
            }
            else
            {
                return 0.0;
            }
        }
    }
}
