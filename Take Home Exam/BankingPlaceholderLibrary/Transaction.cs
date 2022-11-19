// Programmer: Nicholas Lersey 11633967
// <copyright file="Transaction.cs" company="PlaceholderCompany">
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
    /// Abstract class for Transactions.
    /// </summary>
    internal class Transaction
    {
        private double amount;
        private string payee;
        private DateTime date;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// Constructor for Transactions.
        /// </summary>
        /// <param name="amount">amount for transaction that occured.</param>
        /// <param name="payee">Entity for who was invovled in the transaction.</param>
        /// <param name="date">Date of when transaction occured.</param>
        public Transaction(double amount, string payee, DateTime date)
        {
            this.amount = amount;
            this.payee = payee;
            this.date = date;
        }

        /// <summary>
        /// Gets amount.
        /// </summary>
        public double Amount
        {
            get
            {
                return this.amount;
            }
        }

        /// <summary>
        /// Gets payee.
        /// </summary>
        public string Payee
        {
            get
            {
                return this.payee;
            }
        }

        /// <summary>
        /// Gets date.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return this.date;
            }
        }
    }
}
