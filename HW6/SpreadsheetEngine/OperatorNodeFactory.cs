using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS321
{
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
        public static OperatorNode newOperatorNode(char c)
        {
            switch (c)
            {
                case '/':
                    return new DivisionOperatorNode();
                case '*':
                    return new MutlipcationNodeOperator();
                case '+':
                    return new AdditionOperatorNode();
                case '-':
                    return new SubtractionOperatorNode();
            }

            return null;
        }
    }
}
