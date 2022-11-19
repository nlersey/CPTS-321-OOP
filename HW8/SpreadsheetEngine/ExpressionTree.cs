// Programmer: Nicholas Lersey 11633967
// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
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
    /// Main driver program.
    /// Expression tree class.
    /// </summary>
    public class ExpressionTree
    {
        private ExpressionTreeNode root;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">user inputed infix expression.</param>
        public ExpressionTree(string expression)
        {
            this.InfixExpression = expression;
            this.Variables = new Dictionary<string, double>();
            this.root = this.BuildTree(this.InfixExpression);
        }

        /// <summary>
        /// Gets or sets Infixexpression.
        /// </summary>
        public string InfixExpression { get; set; }

        /// <summary>
        /// Gets or sets postfixexpression.
        /// </summary>
        public string PostfixExpression { get; set; }

        /// <summary>
        /// Gets or sets variable's values.
        /// Dictionary of variables used by tree.
        /// </summary>
        public Dictionary<string, double> Variables { get; set; }

        /// <summary>
        /// Sets value and name for varaibles for Dictionary.
        /// </summary>
        /// <param name="variableName">string for variablename.</param>
        /// <param name="variableValue">double for variablevalue.</param>
        public void SetVariable(string variableName, double variableValue)
        {
           this.Variables[variableName] = variableValue;
        }

        /// <summary>
        /// intalizaiton of evaluate function.
        /// </summary>
        /// <returns>evaluation of root node which triggers all tree node evaluations.</returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }

        /// <summary>
        /// Calls variable helper to retrive all stored variables to be sent back to spreadsheet.
        /// </summary>
        /// <returns>Variable List.</returns>
        public List<string> GetVariableNamesandValues()
        {
            List<string> variableList = new List<string>();
            this.GetVariableNamesHelper(variableList, this.root);
            return variableList;
        }

        /// <summary>
        /// shunting algorthim variation to convert infix expression to postfix expression.
        /// </summary>
        /// <param name="infixexpression">user inputed infix expression.</param>
        /// <returns>List.<string> of postfixexpression.</string></returns>
        private List<string> PostfixExpressionConverter(string infixexpression)
        {
            List<string> postfixexpression = new List<string>();
            Stack<char> postfixstack = new Stack<char>();
            for (int i = 0; i < infixexpression.Length; i++)
            {
                char c = infixexpression[i];

                // case of c being )
                if (c == ')')
                {
                    char stacktop = postfixstack.Pop();
                    while (postfixstack.Count > 0 && stacktop != '(')
                    {
                        postfixexpression.Add(stacktop.ToString());
                        stacktop = postfixstack.Pop();
                    }
                }

                // case of c being (
                else if (c == '(')
                {
                    postfixstack.Push(c);
                }

                // case of c being operator
                else if (this.IsOperator(c))
                {
                    OperatorNode cNode;
                    ushort stacktopprec;
                    do
                    {
                        stacktopprec = 0;
                        cNode = OperatorNodeFactory.NewOperatorNode(c);
                        if (postfixstack.Count > 0)
                        {
                            if (this.IsParenthesis(postfixstack.Peek()))
                            {
                                stacktopprec = 0;
                            }
                            else
                            {
                                OperatorNode postfixstacknode = OperatorNodeFactory.NewOperatorNode(postfixstack.Peek());
                                stacktopprec = postfixstacknode.Precedence;
                            }

                            if (postfixstack.Count > 0 && cNode.Precedence <= stacktopprec)
                            {
                                postfixexpression.Add(postfixstack.Pop().ToString());
                            }
                        }
                    }
                    while (postfixstack.Count > 0 && cNode.Precedence <= stacktopprec);
                    postfixstack.Push(c);
                }

                // case of c being operand
                else
                {
                    string operand = string.Empty;
                    while (!this.IsOperator(c) && !this.IsParenthesis(c) && i < infixexpression.Length)
                    {
                        operand += c;
                        i++;
                        if (i < infixexpression.Length)
                        {
                            c = infixexpression[i];
                        }
                    }

                    postfixexpression.Add(operand);
                    i--;
                }
            }

            // pop all remaing elements from stack
            while (postfixstack.Count > 0)
            {
                postfixexpression.Add(postfixstack.Pop().ToString());
            }

            return postfixexpression;
        }

        /// <summary>
        /// Buildtree function which takes infixexpression, passes to shunting alg function, then builds expression tree.
        /// </summary>
        /// <param name="infixstring">user inputed infix expression.</param>
        /// <returns> null if infix is empty or root node.</returns>
        private ExpressionTreeNode BuildTree(string infixstring)
        {
            // to avoid any pop empty stack errors
            if (infixstring.Length != 0)
            {
                Stack<ExpressionTreeNode> expressionTreeStack = new Stack<ExpressionTreeNode>();
                List<string> poststring = this.PostfixExpressionConverter(infixstring);

                foreach (var symbol in poststring)
                {
                    double number = 0.0;
                    if (double.TryParse(symbol, out number))
                    {
                        expressionTreeStack.Push(new ConstantNode(number));
                    }
                    else if (this.IsOperator(symbol[0]) || this.IsParenthesis(symbol[0]))
                    {
                        OperatorNode newOPNode = OperatorNodeFactory.NewOperatorNode(symbol[0]);
                        newOPNode.Left = expressionTreeStack.Pop();
                        newOPNode.Right = expressionTreeStack.Pop();
                        expressionTreeStack.Push(newOPNode);
                    }
                    else
                    {
                        this.Variables.Add(symbol, 0);
                        expressionTreeStack.Push(new VariableNode(this.Variables, symbol));
                    }
                }

                return expressionTreeStack.Pop();
            }

            return null;
        }

        /// <summary>
        /// simple bool function to check if char c is a valid operator we're looking for in expressiontree.
        /// </summary>
        /// <param name="c">char c from postfix expression which could be operator or not.</param>
        /// <returns>true if c is valid operator, false if not.</returns>
        private bool IsOperator(char c)
        {
            if (c == '+' || c == '-' || c == '/' || c == '*' || c == '^')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Simple bool function to check of char c is a parenthesis.
        /// </summary>
        /// <param name="c">char c from postfix expression which could be parenthesis or not.</param>
        /// <returns>true if c is a parenthesis, false if not.</returns>
        private bool IsParenthesis(char c)
        {
            if (c == '(' || c == ')')
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Helperfunction which retrives all variables.
        /// </summary>
        /// <param name="varlist">passed in list to store variables.</param>
        /// <param name="node">passed in root node which is recursively used to retrive all other tree nodes.</param>
        private void GetVariableNamesHelper(List<string> varlist, ExpressionTreeNode node)
        {
            if (node is VariableNode)
            {
                varlist.Add((node as VariableNode).GetsvariableName);
            }

            if (node is OperatorNode)
            {
                this.GetVariableNamesHelper(varlist, (node as OperatorNode).Left);
                this.GetVariableNamesHelper(varlist, (node as OperatorNode).Right);
            }
        }
    }
}
