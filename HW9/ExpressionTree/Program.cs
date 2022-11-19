// Programmer: Nicholas Lersey 11633967
// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;

namespace CPTS321
{
    static class Program
    {
        /// <summary>
        /// The Main driver for expressiontree hw.
        /// </summary>
        static void Main(string[] args)
        {
            string input = string.Empty;
            ExpressionTree tree = new ExpressionTree(string.Empty);
            while (true)
            {
                Console.WriteLine("Menu (current expression=\"" + tree.InfixExpression + "\")");
                Console.WriteLine("1. Enter new expression");
                Console.WriteLine("2. Set a variable value");
                Console.WriteLine("3. Evaluate tree");
                Console.WriteLine("4. Quit");
                input = Console.ReadLine();
                int switchint = Int32.Parse(input);
                switch (switchint)
                {
                    case 1:
                        Console.WriteLine("Enter a new expression: ");
                        tree = new ExpressionTree(Console.ReadLine());
                        break;
                    case 2:
                        Console.WriteLine("Enter a new variable name: ");
                        string variablename = Console.ReadLine();
                        Console.WriteLine("Enter a new variable value: ");
                        double variablevalue = Convert.ToDouble(Console.ReadLine());
                        tree.SetVariable(variablename, variablevalue);
                        break;

                    case 3:
                        Console.WriteLine(tree.Evaluate());
                        break;
                    case 4:
                        return;
                }

            }


        }
    }
}
