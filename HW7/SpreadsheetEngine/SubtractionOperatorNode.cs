//nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
    /// <summary>
    /// Class of Subtraction operator.
    /// </summary>
    internal class SubtractionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionOperatorNode"/> class.
        /// Returns operator '-'.
        /// </summary>
        public SubtractionOperatorNode()
            : base('-')
        {
        }

        /// <summary>
        /// Gets overriden Precedence function.
        /// </summary>
        /// <returns>ushort of - operator precdence.</returns>
        public override ushort Precedence { get; } = 1;

        /// <summary>
        /// Overrideen Evaluate function.
        /// </summary>
        /// <returns>left and right node's evaluations.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() - this.Right.Evaluate();
        }
    }
}
