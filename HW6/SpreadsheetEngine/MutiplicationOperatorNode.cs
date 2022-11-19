//Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
    /// <summary>
    /// Mutlipcation Node class.
    /// </summary>
    internal class MutlipcationNodeOperator : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MutlipcationNodeOperator"/> class.
        /// Returns operator '*'.
        /// </summary>
        public MutlipcationNodeOperator()
             : base('*')
        {
        }

        /// <summary>
        /// Gets overriden Precedence function.
        /// </summary>
        /// <returns>ushort of * operator precdence.</returns>
        public override ushort Precedence { get; } = 2;

        /// <summary>
        /// Overrideen Evaluate function.
        /// </summary>
        /// <returns>left and right node's evaluations.</returns>
        public override double Evaluate()
        {
            return this.Left.Evaluate() * this.Right.Evaluate();
        }
    }
}