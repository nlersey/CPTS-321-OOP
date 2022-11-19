//Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
    /// <summary>
    /// Constant Node.
    /// </summary>
    internal class ConstantNode : ExpressionTreeNode
    {
        /// <summary>
        /// Constant variable value.
        /// </summary>
        private readonly double variableValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// Intalizator for a new class of constant node.
        /// </summary>
        /// <param name="value">constant value.</param>
        public ConstantNode(double value)
        {
            this.variableValue = value;
        }

        /// <summary>
        /// Gets function for constant node's value.
        /// </summary>
        public double ConstantGetSet
        {
            get { return this.variableValue; }
        }

        /// <summary>
        /// Overridden Evaluate function.
        /// </summary>
        /// <returns>value of constant node.</returns>
        public override double Evaluate()
        {
            return this.variableValue;
        }
    }
}
