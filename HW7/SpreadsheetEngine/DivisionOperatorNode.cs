//Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
    /// <summary>
    /// Node for Divison Operator.
    /// </summary>
    internal class DivisionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionOperatorNode"/> class.
        /// Returns operator '/'.
        /// </summary>
        public DivisionOperatorNode()
             : base('/')
        {
        }

        /// <summary>
        /// Gets overriden Precedence function.
        /// </summary>
        /// <returns>ushort of / operator precdence.</returns>
        public override ushort Precedence { get; } = 2;

        /// <summary>
        /// Overidden Evaluate function.
        /// </summary>
        /// <returns>Evaluted value of left and right node.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() / this.Right.Evaluate();

        }

    }
}
