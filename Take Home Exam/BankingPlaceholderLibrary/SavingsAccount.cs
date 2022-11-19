// Programmer: Nicholas Lersey 11633967
// <copyright file="SavingsAccount.cs" company="PlaceholderCompany">
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
    /// Instiated version of abstract class Account for SavingsAccount.
    /// </summary>
    internal class SavingsAccount : Account
    {
        private double interestrate;
        private double totalinterestgained;

        /// <summary>
        /// Initializes a new instance of the <see cref="SavingsAccount"/> class.
        /// Constructor for Savings Account.
        /// </summary>
        /// <param name="accountid">account id for the savings account.</param>
        /// <param name="balance">inital balance for savings account.</param>
        /// <param name="interestrate">inital interest rate for savings account.</param>
        /// <param name="interestgained">inital total interest for savingsaccount.</param>
        public SavingsAccount(string accountid, double balance, double interestrate, double interestgained)
        {
            this.AccountID = accountid;
            this.Funds = balance;
            this.Transactions = new List<Transaction>();
            this.interestrate = interestrate;
            this.totalinterestgained = interestgained;
        }

        /// <summary>
        /// Gets or sets InterestRate.
        /// </summary>
        public double InterestRate
        {
            get
            {
                return this.interestrate;
            }

            set
            {
                if (value != this.interestrate)
                {
                    this.interestrate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets totalinterestgained.
        /// </summary>
        public double InterestGained
        {
            get
            {
                return this.totalinterestgained;
            }

            set
            {
                if (value != this.totalinterestgained)
                {
                    this.totalinterestgained = value;
                }
            }
        }
    }
}
