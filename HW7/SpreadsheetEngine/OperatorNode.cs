//Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
    /// <summary>
    /// Abstract class of Operator node.
    /// </summary>
    internal abstract class OperatorNode : ExpressionTreeNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        /// <param name="operatorchar">passed in char from operatornodeswitch factory operator.</param>
        public OperatorNode(char operatorchar)
        {
            this.Operatorvalue = operatorchar;
            this.Left = null;
            this.Right = null;
        }

        /// <summary>
        /// Gets or sets Left node.
        /// </summary>
        public ExpressionTreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets Right node.
        /// </summary>
        public ExpressionTreeNode Right { get; set; }

        /// <summary>
        /// Gets or sets value of Operator node.
        /// </summary>
        public char Operatorvalue { get; set; }

        /// <summary>
        /// Gets abstract Precedence function.
        /// </summary>
        /// <returns> abstract precdence.</returns>
        public abstract ushort Precedence { get; }

        /// <summary>
        /// Abstract evaluate function.
        /// </summary>
        /// <returns>value of abstract evaluation function.</returns>
        public abstract override double Evaluate();
    }
}
