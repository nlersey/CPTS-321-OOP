//Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
    /// <summary>
    /// VariableNode class.
    /// </summary>
    internal class VariableNode : ExpressionTreeNode
    {
        private readonly string variableName;
        private readonly double variableValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name">string from expressiontree with name of variable.</param>
        /// <param name="value">double value from expression tree with value of variable.</param>
        public VariableNode(string name, double value = 0)
        {
            this.variableName = name;
            this.variableValue = value;
        }

        /// <summary>
        /// Overriden Evaluate function.
        /// </summary>
        /// <returns>value for varaiblenode.</returns>
        public override double Evaluate()
        {
            return this.variableValue;
        }
    }
}
