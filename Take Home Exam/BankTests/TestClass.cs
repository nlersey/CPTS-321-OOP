// NUnit 3 tests
// See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using BankingPlaceholderLibrary;
using System.IO;

namespace BankTests
{
    [TestFixture]
    public class TestClass
    {
        /// <summary>
        /// Test case for Creating new checking account.
        /// </summary>
        [Test]
        public void CreateNewChecking()
        {
            BankAccount bankAccount = new BankAccount("TestUser");
            bankAccount.CreateNewCheckingAccount(1);
            Assert.AreEqual(1, bankAccount.CheckingAccounts.Count);
        }

        /// <summary>
        /// Test case for creating new Savings account.
        /// </summary>
        [Test]
        public void CreateNewSaving()
        {
            BankAccount bankAccount = new BankAccount("TestUser");
            bankAccount.CreateNewSavingsAccount(1,1,1);
            Assert.AreEqual(1, bankAccount.SavingsAccounts.Count);
        }

        /// <summary>
        /// Test case for checking suffiecent funds method.
        /// </summary>
        [Test]
        public void TestSufficentFunds()
        {
            BankAccount bankAccount = new BankAccount("TestUser");
            bankAccount.CreateNewSavingsAccount(15000,1,1);
            Assert.AreEqual(1, bankAccount.SavingsAccounts.Count);
            bankAccount.CreateNewCheckingAccount(15000);
            Assert.AreEqual(1, bankAccount.CheckingAccounts.Count);
            Assert.False(bankAccount.CheckingAccounts["C-TestUser2"].SufficentFunds(15001));
            Assert.True(bankAccount.CheckingAccounts["C-TestUser2"].SufficentFunds(1));
            Assert.False(bankAccount.SavingsAccounts["S-TestUser1"].SufficentFunds(15001));
            Assert.True(bankAccount.SavingsAccounts["S-TestUser1"].SufficentFunds(1));

        }

        /// <summary>
        /// Test case for withdrawing funds method.
        /// </summary>
        [Test]
        public void TestWithdraw()
        {
            BankAccount bankAccount = new BankAccount("TestUser");
            bankAccount.CreateNewSavingsAccount(15000,1,1);
            Assert.AreEqual(1, bankAccount.SavingsAccounts.Count);
            bankAccount.CreateNewCheckingAccount(15000);
            Assert.AreEqual(1, bankAccount.CheckingAccounts.Count);
            Assert.True(bankAccount.CheckingAccounts["C-TestUser2"].SufficentFunds(15000));
            Assert.True(bankAccount.SavingsAccounts["S-TestUser1"].SufficentFunds(15000));
            bankAccount.CheckingAccounts["C-TestUser2"].Withdraw(15000);
            bankAccount.SavingsAccounts["S-TestUser1"].Withdraw(15000);
            Assert.AreEqual(0, bankAccount.CheckingAccounts["C-TestUser2"].Funds);
            Assert.AreEqual(0, bankAccount.SavingsAccounts["S-TestUser1"].Funds);

        }

        /// <summary>
        /// Test case for depositing funds.
        /// </summary>
        [Test]
        public void TestDeposit()
        {
            BankAccount bankAccount = new BankAccount("TestUser");
            bankAccount.CreateNewSavingsAccount(15000,1,1);
            Assert.AreEqual(1, bankAccount.SavingsAccounts.Count);
            bankAccount.CreateNewCheckingAccount(15000);
            Assert.AreEqual(1, bankAccount.CheckingAccounts.Count);
            bankAccount.CheckingAccounts["C-TestUser2"].Deposit(1);
            bankAccount.SavingsAccounts["S-TestUser1"].Deposit(1);
            Assert.AreEqual(15001, bankAccount.CheckingAccounts["C-TestUser2"].Funds);
            Assert.AreEqual(15001, bankAccount.SavingsAccounts["S-TestUser1"].Funds);

        }

        /// <summary>
        /// Test case for creating a new loan.
        /// </summary>
        [Test]
        public void TestCreateNewloan()
        {
            BankAccount bankAccount = new BankAccount("TestUser");
            bankAccount.CreateNewLoan(1000, 0, 0, 0.05);
            Assert.IsTrue(bankAccount.Loans.Count > 0);
        }

        /// <summary>
        /// Test case for loan payments.
        /// </summary>
        [Test]
        public void TestLoanPayment()
        {
            BankAccount bankAccount = new BankAccount("TestUser");
            bankAccount.CreateNewLoan(1000, 0, 0, 0.05);
            bankAccount.PaymenttoLoan(100, 1);
            Assert.AreEqual(900, bankAccount.Loans[1].Currentamount);
        }

    }
}
