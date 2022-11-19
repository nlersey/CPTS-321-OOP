// Programmer: Nicholas Lersey 11633967
// <copyright file="BankAccount.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace BankingPlaceholderLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Bank Account class for a client's account with a bank.
    /// </summary>
    internal class BankAccount
    {
        /// <summary>
        /// Dictonary holder for all Checking Accounts tied to this account.
        /// </summary>
        public Dictionary<string, CheckingAccount> CheckingAccounts = new Dictionary<string, CheckingAccount>();

        /// <summary>
        /// Dictonary holder for all Savings Account tied to this account.
        /// </summary>
        public Dictionary<string, SavingsAccount> SavingsAccounts = new Dictionary<string, SavingsAccount>();

        /// <summary>
        /// Global int for naming accounts.
        /// </summary>
        public int Numberofaccounts;

        /// <summary>
        /// Global int for naming loans.
        /// </summary>
        public int Numberofloans;

        /// <summary>
        /// Dictonary holder for all Loans for this account.
        /// </summary>
        public Dictionary<int, Loan> Loans = new Dictionary<int, Loan>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// </summary>
        /// <param name="user">user for this Bank account.</param>
        public BankAccount(string user)
        {
            this.Username = user;
            this.Numberofaccounts = 0;
        }

        /// <summary>
        /// Gets or sets username for this Bankaccount.
        /// </summary>
        private string Username { get; set; }

        /// <summary>
        /// Creates brand new savings account for this BankAccount with default values we're using for this prototype.
        /// </summary>
        /// <param name="balance">inital balance.</param>
        /// <param name="interestrate">interestrate.</param>
        /// <param name="interestgained">interestgained.</param>
        public void CreateNewSavingsAccount(double balance, double interestrate, double interestgained)
        {
            this.Numberofaccounts += 1;
            string newid = "S-" + this.Username + this.Numberofaccounts;
            this.SavingsAccounts[newid] = new SavingsAccount(newid, balance, interestrate, interestgained);
        }

        /// <summary>
        /// Creates brand new checking account for this BankAccount with default values we're using for this prototype.
        /// </summary>
        /// <param name="accountbalance">intial balance.</param>
        public void CreateNewCheckingAccount(double accountbalance)
        {
            this.Numberofaccounts += 1;
            string newid = "C-" + this.Username + this.Numberofaccounts;
            this.CheckingAccounts[newid] = new CheckingAccount(newid, accountbalance);
        }

        /// <summary>
        /// Processes New transaction for deposting funds from Bank to a checking or savings account.
        /// </summary>
        /// <param name="accountid">given accountid.</param>
        /// <param name="funds">given funds to deposit.</param>
        /// <param name="payee">given payee.</param>
        /// <returns>bool based on if deposit was successful or not.</returns>
        public bool NewTransactionDeposit(string accountid, double funds, string payee)
        {
            if (this.AccountExists(accountid) != false)
            {
                if (accountid[0] == 'C')
                {
                    this.CheckingAccounts[accountid].NewTransactionDeposit(new Transaction(funds, payee, DateTime.Now));
                    return true;
                }
                else if (accountid[0] == 'S')
                {
                    this.SavingsAccounts[accountid].NewTransactionDeposit(new Transaction(funds, payee, DateTime.Now));
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Error: No Account by that ID");
            }

            return false;
        }

        /// <summary>
        /// Processes New transaction for withdrawing funds from a checking or savings account.
        /// </summary>
        /// <param name="accountid">given accountid.</param>
        /// <param name="funds">given funds to deposit.</param>
        /// <param name="payee">given payee.</param>
        /// <returns>bool based on if withdraw was successful or not.</returns>
        public bool NewTransactionWithDraw(string accountid, double funds, string payee)
        {
            if (this.AccountExists(accountid) != false)
            {
                if (accountid[0] == 'C')
                {
                    if (this.CheckingAccounts[accountid].SufficentFunds(funds) == true)
                    {
                        this.CheckingAccounts[accountid].NewTransactionWithdraw(new Transaction(funds, payee, DateTime.Now));
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Error: InsufficentFunds");
                    }
                }
                else if (accountid[0] == 'S')
                {
                    if (this.SavingsAccounts[accountid].SufficentFunds(funds) == true)
                    {
                        this.SavingsAccounts[accountid].NewTransactionWithdraw(new Transaction(funds, payee, DateTime.Now));
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Error: InsufficentFunds");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: No Account by that ID");
            }

            return false;
        }

        /// <summary>
        /// Utitlity method which checks to see if an account exists inside BankAccount.
        /// </summary>
        /// <param name="accountid">accountid to check if exists.</param>
        /// <returns>bool based on if accountid exists or not.</returns>
        public bool AccountExists(string accountid)
        {
            if (accountid[0] == 'C')
            {
               if (this.CheckingAccounts.ContainsKey(accountid) == true)
                {
                    return true;
                }
            }

            if (accountid[0] == 'S')
            {
                if (this.SavingsAccounts.ContainsKey(accountid) == true)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Shows status of all properties of checking account and savings account.
        /// </summary>
        public void Showstatus()
        {
            if (this.CheckingAccounts.Count > 0)
            {
                foreach (KeyValuePair<string, CheckingAccount> entry in this.CheckingAccounts)
                {
                    Console.WriteLine("Accountid: " + this.CheckingAccounts[entry.Key].AccountID);
                    Console.WriteLine("Funds: " + this.CheckingAccounts[entry.Key].Funds);
                    if (this.CheckingAccounts[entry.Key].Transactions.Count > 0)
                    {
                        Console.WriteLine("Transactions: ");
                        this.CheckingAccounts[entry.Key].DisplayTransactions();
                    }
                    else
                    {
                        Console.WriteLine("Transactions: No transactions have occured yet for this account.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No Checking Accounts on File.");
            }

            if (this.SavingsAccounts.Count > 0)
            {
                foreach (KeyValuePair<string, SavingsAccount> entry in this.SavingsAccounts)
                {
                    Console.WriteLine("Accountid: " + this.SavingsAccounts[entry.Key].AccountID);
                    Console.WriteLine("Funds: " + this.SavingsAccounts[entry.Key].Funds);
                    Console.WriteLine("InterestRate: " + this.SavingsAccounts[entry.Key].InterestRate);
                    Console.WriteLine("InterestGained: " + this.SavingsAccounts[entry.Key].InterestGained);
                    if (this.SavingsAccounts[entry.Key].Transactions.Count > 0)
                    {
                        Console.WriteLine("Transactions: ");
                        this.SavingsAccounts[entry.Key].DisplayTransactions();
                    }
                    else
                    {
                        Console.WriteLine("Transactions: No transactions have occured yet for this account.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No Savings Accounts on File.");
            }
        }

        /// <summary>
        /// Creates new loan and adds it to list.
        /// </summary>
        /// <param name="currentamount">loan amount.</param>
        /// <param name="totalpaidtoCapital">amount paid to capital.</param>
        /// <param name="paidtointerest">amount paid to interest.</param>
        /// <param name="interestrate">interest rate.</param>
        public void CreateNewLoan(double currentamount, double totalpaidtoCapital, double paidtointerest, double interestrate)
        {
            this.Numberofloans += 1;
            this.Loans[this.Numberofloans] = new Loan(this.Numberofloans, currentamount, totalpaidtoCapital, paidtointerest, interestrate);
        }

        /// <summary>
        /// Makes payment towards a loan.
        /// </summary>
        /// <param name="payment">payment to loan.</param>
        /// <param name="loanid">loan thats being paid off.</param>
        /// <returns>bool based on if payment was succesful or not.</returns>
        public bool PaymenttoLoan(double payment, int loanid)
        {
            if (this.Loans.ContainsKey(loanid) == true)
            {
                this.Loans[loanid].MakePayment(payment);
                return true;
            }
            else
            {
                Console.WriteLine("Error: No Loan by that ID");
            }

            return false;
        }

        /// <summary>
        /// Displays all loans for this account and their corresponding info.
        /// </summary>
        public void DisplayAllLoans()
        {
            if (this.Loans.Count > 0)
            {
                foreach (KeyValuePair<int, Loan> entry in this.Loans)
                {
                    Console.WriteLine("LoanID: " + this.Loans[entry.Key].Loanid + " total amount: " + this.Loans[entry.Key].Totalamount + " current amount: " +
                        this.Loans[entry.Key].Currentamount + " interestrate: " + this.Loans[entry.Key].InterestRate);
                    if (this.Loans[entry.Key].LoanPayments.Count > 0)
                    {
                        this.Loans[entry.Key].DisplayLoanPayments();
                    }
                }
            }
        }
    }
}
