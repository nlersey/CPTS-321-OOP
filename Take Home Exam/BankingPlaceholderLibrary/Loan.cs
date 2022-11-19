// Programmer: Nicholas Lersey 11633967
// <copyright file="Loan.cs" company="PlaceholderCompany">
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
    /// Cclass of Loan to be used by BankAccount.
    /// </summary>
    internal class Loan
    {
        /// <summary>
        /// List of loan payments for this loan.
        /// </summary>
        public List<LoanPayment> LoanPayments = new List<LoanPayment>();
        private double currentAmount;
        private double totalpaidtoCapital;
        private double totalpaidtoInterest;
        private double interestRate;
        private double totalamount;
        private int loanID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Loan"/> class.
        /// </summary>
        /// <param name="loanID">string id for identifing loan.</param>
        /// <param name="currentamount">inital/current amount left on loan.</param>
        /// <param name="totalpaidtoCapital">total payments towards captial.</param>
        /// <param name="paidtointerest">total payments towards interest.</param>
        /// <param name="interestrate">interest rate of loan.</param>
        public Loan(int loanID, double currentamount, double totalpaidtoCapital, double paidtointerest, double interestrate)
        {
            this.loanID = loanID;
            this.currentAmount = currentamount;
            this.totalamount = currentamount;
            this.totalpaidtoCapital = totalpaidtoCapital;
            this.totalpaidtoInterest = paidtointerest;
            this.interestRate = interestrate;
        }

        /// <summary>
        /// Gets loanid.
        /// </summary>
        public double Loanid
        {
            get
            {
                return this.loanID;
            }
        }

        /// <summary>
        /// Gets currentamount.
        /// </summary>
        public double Currentamount
        {
            get
            {
                return this.currentAmount;
            }
        }

        /// <summary>
        /// Gets totalamount.
        /// </summary>
        public double Totalamount
        {
            get
            {
                return this.totalamount;
            }
        }

        /// <summary>
        /// Gets interestrate.
        /// </summary>
        public double InterestRate
        {
            get
            {
                return this.interestRate;
            }
        }

        /// <summary>
        /// Make payment towards a loan.
        /// </summary>
        /// <param name="payment">loan payment.</param>
        public void MakePayment(double payment)
        {
            this.currentAmount -= payment;
            this.LoanPayments.Add(new LoanPayment(payment, this.interestRate));
        }

        /// <summary>
        /// Displays status of all loan payments.
        /// </summary>
        public void DisplayLoanPayments()
        {
           if (this.LoanPayments.Count > 0)
            {
                int i = 0;
                while (i < this.LoanPayments.Count)
                {
                    Console.WriteLine("LoanPayment: TowardsCapital: " + this.LoanPayments[i].TowardsCapital + " Towards Interest: " + this.LoanPayments[i].TowardsInterest + " Payment: " + this.LoanPayments[i].PaymentAmount);
                    i++;
                }
            }
        }
    }
}
