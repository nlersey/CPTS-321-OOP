// Programmer: Nicholas Lersey 11633967
// <copyright file="Account.cs" company="PlaceholderCompany">
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
    /// Abstract class for two various bank accounttypes.
    /// </summary>
    internal abstract class Account
    {
        /// <summary>
        /// List of Transactions for this Account.
        /// </summary>
        public List<Transaction> Transactions = new List<Transaction>();
        private double funds;
        private string accountID;

        /// <summary>
        /// Gets or Sets Funds.
        /// </summary>
        public double Funds
        {
            get
            {
                return this.funds;
            }

            set
            {
                if (value != this.funds)
                {
                    this.funds = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets accountID.
        /// </summary>
        public string AccountID
        {
            get
            {
                return this.accountID;
            }

            set
            {
                if (value != this.accountID)
                {
                    this.accountID = value;
                }
            }
        }

        /// <summary>
        /// Checks if account has enough funds to withdraw desired amount.
        /// </summary>
        /// <param name="fundstobewithdrawn">proposed amount of funds to be withdraw.</param>
        /// <returns>bool based on fund quantity.</returns>
        public bool SufficentFunds(double fundstobewithdrawn)
        {
            if (this.funds < fundstobewithdrawn)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Subtracts desired amount of funds from account.
        /// </summary>
        /// <param name="fundstobewithdrawn">amount of funds to be subtracted from account.</param>
        public void Withdraw(double fundstobewithdrawn)
        {
            this.Funds -= fundstobewithdrawn;
        }

        /// <summary>
        /// Adds desired amount of funds to account.
        /// </summary>
        /// <param name="fundstobeadded">amount of funds to be added to account.</param>
        public void Deposit(double fundstobeadded)
        {
            this.Funds += fundstobeadded;
        }

        /// <summary>
        /// Adds new  deposit transaction to list of past transactions.
        /// </summary>
        /// <param name="transaction">new transaction.</param>
        public void NewTransactionDeposit(Transaction transaction)
        {
            this.Deposit(transaction.Amount);
            this.Transactions.Add(transaction);
        }

        /// <summary>
        /// Adds new  withdraw transaction to list of past transactions.
        /// </summary>
        /// <param name="transaction">new transaction.</param>
        /// <returns>bool if operation was successful or not.</returns>
        public bool NewTransactionWithdraw(Transaction transaction)
        {
            if (this.SufficentFunds(transaction.Amount))
            {
                this.Withdraw(transaction.Amount);
                this.Transactions.Add(transaction);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method to display all transactions.
        /// </summary>
        public void DisplayTransactions()
        {
            if (this.Transactions.Count > 0)
            {
                int i = 0;
                while (i < this.Transactions.Count)
                {
                    Console.WriteLine(this.Transactions[i].Amount + " Date " + this.Transactions[i].Date + " " + this.Transactions[i].Payee);
                    i++;
                }
            }
        }
    }
}
