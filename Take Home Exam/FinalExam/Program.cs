// Programmer: Nicholas Lersey 11633967
// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankingPlaceholderLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Main driver.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args">Main args.</param>
        public static void Main(string[] args)
        {
            Bank bank = new Bank();
            Console.WriteLine("Welcome to Bank of 321. \nPlease login.");
            while (true)
            {
                Console.WriteLine("Enter Username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string password = Console.ReadLine();
                if (bank.Authenicate(username, password))
                {
                    Console.WriteLine("Login Successful. Welcome " + username);
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("Main Menu:\n Enter Corresponding numbers for Menu options.");
                Console.WriteLine("1. Create new checking account with default test value");
                Console.WriteLine("2. Create new savings account with default test values");
                Console.WriteLine("3. Create new Loan.");
                Console.WriteLine("4. Display Checking/Savings account status.");
                Console.WriteLine("5. Display Loan Status.");
                Console.WriteLine("6. Make Deposit. ");
                Console.WriteLine("7. Withdraw Funds.");
                Console.WriteLine("8. Make payment on loan.");
                Console.WriteLine("9. Transfer Funds.");

                string menuoption = Console.ReadLine();
                switch (menuoption)
                {
                    case "1":
                        bank.CreateNewChecking();
                        break;
                    case "2":
                        bank.CreateNewSavings();
                        break;
                    case "3":
                        Console.WriteLine("Enter loan amount to be taken out.");
                        double loan = double.Parse(Console.ReadLine());
                        Console.WriteLine("Enter interest rate in decimal format.");
                        double interest = double.Parse(Console.ReadLine());
                        bank.CreateNewLoan(loan, interest);
                        break;
                    case "4":
                        bank.DisplayCheckingSavingsStatus();
                        break;
                    case "5":
                        bank.DisplayAllLoansStatus();
                        break;
                    case "6":
                        bank.DisplayCheckingSavingsStatus();
                        Console.WriteLine("Enter accountid of account transfering funds to: ");
                        string accountid = Console.ReadLine();
                        Console.WriteLine("Enter number of funds to deposit: ");
                        double funds = double.Parse(Console.ReadLine());
                        bank.SelfDeposit(accountid, funds);
                        break;
                    case "7":
                        bank.DisplayCheckingSavingsStatus();
                        Console.WriteLine("Enter accountid of account transfering funds from: ");
                        string account = Console.ReadLine();
                        Console.WriteLine("Enter number of funds to withdraw: ");
                        double fund = double.Parse(Console.ReadLine());
                        bank.SelfWithdraw(account, fund);
                        break;
                    case "8":
                        bank.DisplayAllLoansStatus();
                        Console.WriteLine("Enter loanid of loan to make payment to: ");
                        int loanid = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter payment number: ");
                        double payment = double.Parse(Console.ReadLine());
                        bank.PaymentToLoan(payment, loanid);
                        break;
                    case "9":
                        bank.DisplayCheckingSavingsStatus();
                        Console.WriteLine("Enter accountID of your account you're transferring funds from: ");
                        string userbankID = Console.ReadLine();
                        Console.WriteLine("Enter BankAccountID of person transfering funds to: ");
                        Console.WriteLine("Note for TA demo: Only two BankAccountIDs are either \"user1\" or \"user2\"");
                        string payeebankID = Console.ReadLine();
                        Console.WriteLine("Enter Checking or Savings Account ID of person transfering funds to: ");
                        Console.WriteLine("Note for TA demo: automatically loaded demo IDs are \"C-user11\", \"S-user12\", \"C-user21\",\"S-user22\"");
                        string payeecheckingsavingID = Console.ReadLine();
                        Console.WriteLine("Enter number of funds to transfer: ");
                        double amount = double.Parse(Console.ReadLine());
                        bank.TransferFunds(userbankID, payeebankID, payeecheckingsavingID, amount);
                        break;
                }
            }
        }
    }
}
