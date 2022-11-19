// Programmer: Nicholas Lersey 11633967
// <copyright file="CheckingAccount.cs" company="PlaceholderCompany">
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
    /// Instiated version of abstract class Account for CheckingAccount.
    /// </summary>
    internal class CheckingAccount : Account
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckingAccount"/> class.
        /// Constructor for Checking account.
        /// </summary>
        /// <param name="accountid">checking account id.</param>
        /// <param name="balance">inital balance of checking account.</param>
        public CheckingAccount(string accountid, double balance)
        {
            this.AccountID = accountid;
            this.Funds = balance;
            this.Transactions = new List<Transaction>();
        }
    }
}
