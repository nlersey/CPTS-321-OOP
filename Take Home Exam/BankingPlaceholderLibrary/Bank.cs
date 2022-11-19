// Programmer: Nicholas Lersey 11633967
// <copyright file="Bank.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace BankingPlaceholderLibrary
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// MainDriver of backend being Bank class which is to act like Banks main DB in such a way.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// List which holds all current actively used BankAccounts, for example the one being used by current user.
        /// </summary>
        internal Dictionary<string, BankAccount> BankAccountsDictValue = new Dictionary<string, BankAccount>();

        /// <summary>
        /// Dictonary populated at start with all various usernames/passwords.
        /// </summary>
        internal Dictionary<string, string> UserDictValue = new Dictionary<string, string>();

        private string thiscurrentuser;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bank"/> class.
        /// Bank Constructor which grabs from TextFile list of preexisting test usernames/passwords and populates dict with them.
        /// Also grabs from another TextFile list of current bank accounts and their balances.
        /// </summary>
        public Bank()
        {
            this.PopulateuserDict();
            this.PopulateaccountsDict();
        }

        /// <summary>
        /// Gets userDict.
        /// </summary>
        public Dictionary<string, string> UserDict
        {
            get
            {
                return this.UserDictValue;
            }
        }

        /// <summary>
        /// Gets bankAccountDict.
        /// </summary>
        internal Dictionary<string, BankAccount> BankAccountsDict
        {
            get
            {
                return this.BankAccountsDictValue;
            }
        }

        /// <summary>
        /// Authenicates a user for login.
        /// </summary>
        /// <param name="username">user input username.</param>
        /// <param name="password">user input password.</param>
        /// <returns>bool based on success or not.</returns>
        public bool Authenicate(string username, string password)
        {
            if (this.UserDictValue.ContainsKey(username))
            {
                if (this.UserDictValue[username] == password)
                {
                    this.thiscurrentuser = username;
                    return true;
                }
            }

            Console.WriteLine("Incorrect Username/Password");
            return false;
        }

       /// <summary>
       /// Creates new CheckingAccount for this user with default test balance.
       /// </summary>
        public void CreateNewChecking()
        {
            this.BankAccountsDictValue[this.thiscurrentuser].CreateNewCheckingAccount(15000);
        }

        /// <summary>
        /// Creates new SavingsAccount for this user with default test values.
        /// </summary>
        public void CreateNewSavings()
        {
            this.BankAccountsDictValue[this.thiscurrentuser].CreateNewSavingsAccount(15000, 0.05, 0);
        }

        /// <summary>
        /// Creates new loan for this account.
        /// </summary>
        /// <param name="loanamount">loan amount to take out.</param>
        /// <param name="interestrate">interest rate for loan.</param>
        public void CreateNewLoan(double loanamount, double interestrate)
        {
            this.BankAccountsDictValue[this.thiscurrentuser].CreateNewLoan(loanamount, 0, 0, interestrate);
        }

        /// <summary>
        /// Method for displaying all loans status for an account.
        /// </summary>
        public void DisplayAllLoansStatus()
        {
            this.BankAccountsDictValue[this.thiscurrentuser].DisplayAllLoans();
        }

        /// <summary>
        /// method for displaying all checking and savings accounts for a user.
        /// </summary>
        public void DisplayCheckingSavingsStatus()
        {
            this.BankAccountsDictValue[this.thiscurrentuser].Showstatus();
        }

        /// <summary>
        /// Method for taking user input money and paying towards loan.
        /// </summary>
        /// <param name="payment">user payment.</param>
        /// <param name="loanid">loan to be paid.</param>
        /// <returns>bool based on if operation was successful.</returns>
        public bool PaymentToLoan(double payment, int loanid)
        {
           if (this.BankAccountsDictValue[this.thiscurrentuser].PaymenttoLoan(payment, loanid))
            {
                return true;
            }

           return false;
        }

        /// <summary>
        /// Method for deposint funds to ones own account.
        /// </summary>
        /// <param name="accountid">account to deposit funds to. </param>
        /// <param name="funds">funds to deposit.</param>
        /// <returns>bool.</returns>
        public bool SelfDeposit(string accountid, double funds)
        {
            if (this.Deposit(accountid, funds, "SelfDeposit", this.thiscurrentuser))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method for withdrawing funds out of persons own account like an atm.
        /// </summary>
        /// <param name="accountid">account to withdraw funds from.</param>
        /// <param name="funds">funds to withdraw.</param>
        /// <returns>bool.</returns>
        public bool SelfWithdraw(string accountid, double funds)
        {
            if (this.Withdraw(accountid, funds, "SelfWithdraw", this.thiscurrentuser))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Main method for processing transferring of funds from one account to another.
        /// </summary>
        /// <param name="usersCheckingSavingAccountID">users accountID.</param>
        /// <param name="payeeBankAccountID">Payee's bank ID.</param>
        /// <param name="payeeCheckingSavingsAccountID">Payee's Checking/Saving AccountID.</param>
        /// <param name="funds">funds.</param>
        /// <returns>bool.</returns>
        public bool TransferFunds(string usersCheckingSavingAccountID, string payeeBankAccountID, string payeeCheckingSavingsAccountID, double funds)
        {
            if (this.BankAccountsDictValue.ContainsKey(payeeBankAccountID))
            {
                if (this.thiscurrentuser == payeeBankAccountID)
                {
                    if (this.Withdraw(usersCheckingSavingAccountID, funds, this.thiscurrentuser, this.thiscurrentuser))
                    {
                        if (this.Deposit(payeeCheckingSavingsAccountID, funds, this.thiscurrentuser, this.thiscurrentuser))
                        {
                            return true;
                        }
                    }
                }
                else if (this.Withdraw(usersCheckingSavingAccountID, funds, payeeBankAccountID, this.thiscurrentuser))
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Error: No BankAccount by that ID");
            }

            return false;
        }

        /// <summary>
        /// Withdrawing method which withdraws funds from a particular account.
        /// </summary>
        /// <param name="checkingSavingAccountID">account to withdraw funds from.</param>
        /// <param name="funds">funds to withdraw.</param>
        /// <param name="payee">payee.</param>
        /// <param name="bankAccountID">given bankaccount.</param>
        /// <returns>bool.</returns>
        private bool Withdraw(string checkingSavingAccountID, double funds, string payee, string bankAccountID)
        {
            if (this.BankAccountsDictValue[bankAccountID].NewTransactionWithDraw(checkingSavingAccountID, funds, payee))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Depostsing funds to a particular bank account.
        /// </summary>
        /// <param name="checkingSavingsAccountID">checking/savings accountid.</param>
        /// <param name="funds">funds to deposit.</param>
        /// <param name="payee">payee.</param>
        /// <param name="bankAccountID">bank username.</param>
        /// <returns>bool.</returns>
        private bool Deposit(string checkingSavingsAccountID, double funds, string payee, string bankAccountID)
        {
            if (this.BankAccountsDictValue[bankAccountID].NewTransactionDeposit(checkingSavingsAccountID, funds, payee))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reads from CSV file and populates userDict with it's contents.
        /// </summary>
        private void PopulateuserDict()
        {
            string filePath = @"Users.csv";
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    this.UserDictValue[values[0]] = values[1];
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
        }

        /// <summary>
        /// Populates a dictonary with usernames/passwords read from a csv file.
        /// </summary>
        private void PopulateaccountsDict()
        {
            string filePath = @"BankAccounts.csv";
            StreamReader reader = null;
            if (File.Exists(filePath))
            {
                reader = new StreamReader(File.OpenRead(filePath));
                var line = reader.ReadLine(); // garbage line

                while (!reader.EndOfStream)
                {
                    // username,CheckingAccountBalance,SavingsAccountBalance,SavingsAccountinterestrate,SavingsAccountinterestgained
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    string username = values[0];
                    double checkingbalance = double.Parse(values[1]);
                    double savingsbalance = double.Parse(values[2]);
                    double savingsaccountinterestrate = double.Parse(values[3]);
                    double savingsaccountinterestgained = double.Parse(values[4]);
                    this.BankAccountsDictValue[username] = new BankAccount(username);
                    this.BankAccountsDictValue[username].CreateNewCheckingAccount(checkingbalance);
                    this.BankAccountsDictValue[username].CreateNewSavingsAccount(savingsbalance, savingsaccountinterestrate, savingsaccountinterestgained);
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
        }
    }
}
