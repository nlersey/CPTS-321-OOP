// Programmer: Nicholas Lersey 11633967
// <copyright file="LoanPayment.cs" company="PlaceholderCompany">
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
    /// Class for creating a payment towards a loan.
    /// </summary>
    internal class LoanPayment
    {
        private double towardsCapital;
        private double towardsInterest;
        private double paymentamount;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoanPayment"/> class.
        /// </summary>
        /// <param name="payment">Payment towards loan.</param>
        /// <param name="interest">Interest rate of loan.</param>
        public LoanPayment(double payment, double interest)
        {
            this.towardsInterest = payment * interest;
            this.towardsCapital = payment - this.towardsInterest;
            this.paymentamount = payment;
        }

        /// <summary>
        /// Gets towardscapital.
        /// </summary>
        public double TowardsCapital
        {
            get
            {
                return this.towardsCapital;
            }
        }

        /// <summary>
        /// Gets TowardsInterest.
        /// </summary>
        public double TowardsInterest
        {
            get
            {
                return this.towardsInterest;
            }
        }

        /// <summary>
        /// Gets Payment.
        /// </summary>
        public double PaymentAmount
        {
            get
            {
                return this.paymentamount;
            }
        }
    }
}
