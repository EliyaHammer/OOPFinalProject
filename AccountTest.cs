using System;
using FinalProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFinalProject
{
    [TestClass]
    public class AccountTest
    {
        //CTOR TESTS:
        [TestMethod]
        public void constructor()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Assert.AreEqual(1, accountTest.AccountNumber);
        }

        [TestMethod]
        [ExpectedException (typeof(IllegalIncomeException))]
        public void ConstructorNegativeIncomeException()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(-10, accountOwner);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorNullOwner()
        {
            Customer accountOwner = null;
            Account accountTest = new Account(100, accountOwner);
        }

        //OPERATOR == TEST:
        [TestMethod]
        public void OperatorEqualizer()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = new Account(3000, accountOwner);
            Assert.IsTrue(accountTest == accountTest);
            Assert.IsFalse(accountTest == accountTestTwo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OperatorEqualizerNullexception()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = null;
            bool equal = (accountTest == accountTestTwo);
        }

        //OPERATOR != TEST
        [TestMethod]
        public void OperatorNotEqualizer()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = new Account(3000, accountOwner);
            Assert.IsFalse(accountTest != accountTest);
            Assert.IsTrue(accountTest != accountTestTwo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OperatorNotEqualizerNullexception()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = null;
            bool equal = (accountTest != accountTestTwo);
        }

        //EQUALS TESTS:
        [TestMethod]
        public void Equals()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = new Account(3000, accountOwner);
            Assert.IsTrue(accountTest.Equals(accountTest));
            Assert.IsFalse(accountTest.Equals(accountTestTwo));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EqualsNullexception()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = null;
            bool equal = accountTest.Equals(accountTestTwo);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeAccessException))]
        public void EqualsWrongTypeException()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Customer customer = new Customer(1212, "eliya", 05454);
            bool equal = accountTest.Equals(customer);
        }

        //HASHCODE TESTS:
        [TestMethod]
        public void GetHashCode()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Assert.AreEqual(1, accountTest.GetHashCode());
        }

        //OPERATOR + TESTS:
        [TestMethod]
        public void PlusOperator()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = new Account(200, accountOwner);
            Account resultAccount = accountTest + accountTestTwo;
            Assert.AreEqual(accountTest.AccountOwner, resultAccount.AccountOwner);
            Assert.AreEqual(accountTestTwo.AccountOwner, resultAccount.AccountOwner);
            Assert.AreEqual((accountTest.Balance + accountTestTwo.Balance), resultAccount.Balance);
            Assert.IsTrue(accountTest.AccountNumber != resultAccount.AccountNumber && accountTestTwo.AccountNumber != resultAccount.AccountNumber);
        }


        [TestMethod]
        [ExpectedException(typeof(SameAccountException))]
        public void PlusOperatorSameAccountException()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account resultAccount = accountTest + accountTest;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlusOperatorNullException ()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = null;
            Account resultAccount = accountTest + accountTestTwo;
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerAccessabilityException))]
        public void PlusOperatorCustomerAccessablityException()
        {
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Customer accountOwnerTwo = new Customer(232355, "dan", 0545455);
            Account accountTest = new Account(3000, accountOwner);
            Account accountTestTwo = new Account(200, accountOwnerTwo);
            Account resultAccount = accountTest + accountTestTwo;
        }

        //INITIALIZE SAVING TESTS:
       [TestMethod]
       public void InitializeSaving ()
        {
            Bank Leumi = new Bank("Leumi", "tel aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Leumi.AddNewCustomer(accountOwner);
            Leumi.OpenNewAccount(accountOwner);
            Leumi.GetAccountByNumber(1).InitializeSaving(Leumi);
            Assert.AreEqual(1, Leumi.GetAccountByNumber(1).TotalNumberOfAccounts_Save);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void InitializeSavingNotFoundException ()
        {
            Bank Leumi = new Bank("Leumi", "tel aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(100, accountOwner);

            accountTest.InitializeSaving(Leumi);
        }

        //CLOSE SAVING TESTS:
        [TestMethod]
        public void CloseSaving()
        {
            Bank Leumi = new Bank("Leumi", "tel aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Leumi.AddNewCustomer(accountOwner);
            Leumi.OpenNewAccount(accountOwner);
            Leumi.GetAccountByNumber(1).InitializeSaving(Leumi);
            Leumi.GetAccountByNumber(1).CloseSaving(Leumi);
            Assert.AreEqual(0, Leumi.GetAccountByNumber(1).TotalNumberOfAccounts_Save);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void CloseSavingNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "tel aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Account accountTest = new Account(100, accountOwner);

            accountTest.CloseSaving(Leumi);
        }

    }
}
