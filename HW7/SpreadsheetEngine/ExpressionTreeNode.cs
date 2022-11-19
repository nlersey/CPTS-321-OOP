//Nicholas Lersey 11633967

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
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
