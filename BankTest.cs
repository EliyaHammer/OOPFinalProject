using System;
using System.Collections.Generic;
using FinalProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFinalProject
{
    [TestClass]
    public class BankTest
    {
        //CONSTRUCTOR TEST:
        [TestMethod]
        public void Constructor()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Assert.AreEqual("Leumi", Leumi.Name);
            Assert.AreEqual("Rabin Square 17, Tel Aviv", Leumi.Address);
            Assert.AreEqual(0, Leumi.CustomerCounter);
        }

        //GET CUSTOMER BY ID TESTS:
        [TestMethod]
        public void GetCustomerByID()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);

            Assert.AreEqual(Eliya, (Leumi.GetCustomerByID(333)));
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void GetCustomerByIDDoesnotExistException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);

            Leumi.GetCustomerByID(222);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void GetCustomerByIDIllegalIDException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.GetCustomerByID(-1);
        }

        //ADD NEW CUSTOMER TESTS:
        [TestMethod]
        public void AddNewCustomer()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);

            Assert.AreEqual(Eliya, Leumi.GetCustomerByNumber(1));
            Assert.AreEqual(1, Leumi.CustomerCounter);
            Assert.AreEqual(Eliya, Leumi.GetCustomerByID(333));
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerAlreadyExistsException))]
        public void AddNewCustomerAlreadyExsistsException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.AddNewCustomer(Eliya);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewCustomerNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = null;
            Leumi.AddNewCustomer(Eliya);
        }

        //ADD NEW ACCOUNT TESTS:
        [TestMethod]
        public void OpenNewAccount()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Assert.IsTrue(Leumi.GetAccountsByCustomer(Eliya).Count == 1);
            Leumi.OpenNewAccount(Eliya);
            Assert.IsTrue(Leumi.GetAccountsByCustomer(Eliya).Count == 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OpenNewAccountNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = null;
            Leumi.OpenNewAccount(Eliya);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void OpenNewAccountCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.OpenNewAccount(Eliya);
        }


        //GET ACCOUNT BY CUSTOMER TESTS:
        [TestMethod]
        public void GetAccountsByCustomer()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Assert.IsInstanceOfType(Leumi.GetAccountsByCustomer(Eliya), typeof(List<Account>));
            Assert.IsTrue(Leumi.GetAccountsByCustomer(Eliya).Count == 1);
            Assert.IsTrue(Leumi.GetAccountsByCustomer(Eliya)[0].AccountOwner == Eliya);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAccountsByCustomerNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = null;
            Leumi.GetAccountsByCustomer(Eliya);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void GetAccountsByCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.GetAccountsByCustomer(Eliya);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void GetAccountsByCustomerNoAccountException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.GetAccountsByCustomer(Eliya);
        }

        //GET CUSTOMER BY NUMBER TESTS:
        [TestMethod]
        public void GetCustomerByNumber()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);

            Assert.AreEqual(Eliya, Leumi.GetCustomerByNumber(1));
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void GetCustomerByNumberInvalidNumberException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Leumi.GetCustomerByNumber(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void GetCustomerByNumberNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Leumi.GetCustomerByNumber(3);
        }

        //GET ACCOUNT BY NUMBER TESTS:
        [TestMethod]
        public void GetAccountByNumber()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);

            Assert.AreEqual(Leumi.GetAccountsByCustomer(Eliya)[0], Leumi.GetAccountByNumber(1));
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void GetAccountByNumberIllegalNumberException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Leumi.GetAccountByNumber(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void GetAccountByNumberNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Leumi.GetAccountByNumber(3);
        }

        //DEPOSIT TESTS:
        [TestMethod]
        public void Deposit()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.OpenNewAccount(Eliya);

            Leumi.Deposit(Eliya, 1, 200);
            Assert.AreEqual(200, Leumi.GetAccountsByCustomer(Eliya)[0].Balance);
            Assert.AreEqual(0, Leumi.GetAccountsByCustomer(Eliya)[1].Balance);
            Assert.AreEqual(200, Leumi.GetTotalBalanceCustomer(Eliya));

            Leumi.Deposit(Eliya, 1, 100);
            Assert.AreEqual(300, Leumi.GetAccountsByCustomer(Eliya)[0].Balance);
            Assert.AreEqual(0, Leumi.GetAccountsByCustomer(Eliya)[1].Balance);
            Assert.AreEqual(300, Leumi.GetTotalBalanceCustomer(Eliya));
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIncomeException))]
        public void DepositIllegalAmountException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void DepositAccountNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.Deposit(Eliya, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIncomeException))]
        public void DepositCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DepositCustomerNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Eliya = null;
            Leumi.Deposit(Eliya, 1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void DepositIllegalAccountNumberException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, -1, 100);
        }

        //GET TOTAL BALANCE FOR CUSTOMER TESTS:
        [TestMethod]
        public void GetTotalBalanceCustomer()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, 100);
            Assert.AreEqual(100, Leumi.GetTotalBalanceCustomer(Eliya));
            Leumi.Deposit(Eliya, 2, 100);
            Assert.AreEqual(200, Leumi.GetTotalBalanceCustomer(Eliya));
            Leumi.Deposit(Eliya, 2, 100);
            Assert.AreEqual(300, Leumi.GetTotalBalanceCustomer(Eliya));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTotalBalanceNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Eliya = null;
            Leumi.Deposit(Eliya, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void GetTotalBalanceCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.Deposit(Eliya, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void GetTotalBalanceAccountNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.Deposit(Eliya, 1, 1);
        }

        //GET ACCOUT BALANCE TESTS:
        [TestMethod]
        public void GetAccountBalance()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, 100);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 2, 100);
            Assert.AreEqual(100, Leumi.GetAccountBalance(Eliya, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetAccountBalanceNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = null;
            Leumi.GetAccountBalance(Eliya, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void GetAccountBalanceIllegalIDException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);

            Leumi.GetAccountBalance(Eliya, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void GetAccountBalanceCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);

            Leumi.GetAccountBalance(Eliya, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void GetAccountBalanceAccountNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);

            Leumi.GetAccountBalance(Eliya, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void GetAccountBalanceAccountNotFoundExceptionTwo()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.GetAccountBalance(Eliya, 2);
        }

        //WITHDRAW TESTS:
        [TestMethod]
        public void Withdraw()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, 100);
            Leumi.Withdraw(Eliya, 1, 50);
            Assert.AreEqual(50, Leumi.GetAccountBalance(Eliya, 1));
            Assert.AreEqual(0, Leumi.GetAccountBalance(Eliya, 2));
        }

        [TestMethod]
        public void WithdrawTwo()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, 50);
            Leumi.Withdraw(Eliya, 1, 100);
            Assert.AreEqual(-50, Leumi.GetAccountBalance(Eliya, 1));
            Assert.AreEqual(0, Leumi.GetAccountBalance(Eliya, 2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void WithdrawNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = null;
            Leumi.Withdraw(Eliya, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void WithdrawIllegalIDException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.Withdraw(Eliya, -1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIncomeException))]
        public void WithdrawIllegalAmountException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Withdraw(Eliya, 1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void WithdrawCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.Withdraw(Eliya, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void WithdrawNoAccountsException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.Withdraw(Eliya, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void WithdrawWrongAccountException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Withdraw(Eliya, 2, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIncomeException))]
        public void WithdrawNoMoneyException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, 50);
            Leumi.Withdraw(Eliya, 1, 250);
        }

        //CLOSE ACCOUNT TESTS:
        [TestMethod]
        public void CloseAccount()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.CloseAccount(Eliya, 1);

            Assert.IsTrue(Leumi.GetAccountsByCustomer(Eliya).Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void CloseAccountTwo()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.CloseAccount(Eliya, 1);
            Leumi.GetAccountByNumber(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CloseAccountNullException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = null;
            Leumi.CloseAccount(Eliya, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void CloseAccountIllegalIDException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.CloseAccount(Eliya, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void CloseAccountCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.CloseAccount(Eliya, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void CloseAccountNoAccountsException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.CloseAccount(Eliya, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void CloseAccountWrongAccountException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.CloseAccount(Eliya, 2);
        }

        //JOIN ACCOUNTS TESTS:
        [TestMethod]
        public void JoinAccounts()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.JoinAccounts(Eliya, 1, 2);
            Assert.IsTrue(Leumi.GetAccountsByCustomer(Eliya)[0].AccountNumber != 1 && Leumi.GetAccountsByCustomer(Eliya)[0].AccountNumber != 2);
            Assert.IsTrue(Leumi.GetAccountsByCustomer(Eliya).Count == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void JoinAccountsNullException ()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = null;
            Leumi.JoinAccounts(Eliya, 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void JoinAccountsIllegalIDException ()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.JoinAccounts(Eliya, -1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIDException))]
        public void JoinAccountsIllegalIDExceptionTwo()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.JoinAccounts(Eliya, 1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void JoinAccountsCustomerNotFoundException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.JoinAccounts(Eliya, 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void JoinAccountsNoAccountsException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.JoinAccounts(Eliya, 1, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(AccountNotFoundException))]
        public void JoinAccountsWrongAccountException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.JoinAccounts(Eliya, 1, 2);
        }

        //CHARGE ANNUAL COMMISSION TESTS:
        [TestMethod]
        public void ChargeAnnualCommission ()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, 100);
            Leumi.Deposit(Eliya, 2, 100);
            Leumi.ChargeAnnualCommission(2.0f);
            Assert.IsTrue(Leumi.GetAccountBalance(Eliya, 1) == 98 && Leumi.GetAccountBalance(Eliya, 2) == 98);
            Leumi.Withdraw(Eliya, 1, 98);
            Leumi.ChargeAnnualCommission(2.0f);
            Assert.AreEqual(-2, Leumi.GetAccountBalance(Eliya, 1));
            Leumi.Withdraw(Eliya, 1, 98);
            Leumi.ChargeAnnualCommission(2.0f);
            Assert.AreEqual(-104, Leumi.GetAccountBalance(Eliya, 1));
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalIncomeException))]
        public void ChargeAnnualCommissionIllegalNumberException()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Leumi.ChargeAnnualCommission(0.0f);
        }

        //SAVE ALL DATA TESTS:
        [TestMethod]
        public void SaveAllData ()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Customer Eliya = new Customer(333, "Eliya", 0545454);
            Leumi.AddNewCustomer(Eliya);
            Leumi.OpenNewAccount(Eliya);
            Leumi.Deposit(Eliya, 1, 100);
            Leumi.SaveAllData();
            Assert.AreEqual(0, Leumi.Profits_Save);
            Assert.AreEqual(0, Eliya.NumberOfTotalCustomers_Save);
            Assert.AreEqual(0, Leumi.GetAccountByNumber(1).MaxMinusAllowed_Save);
        }

        [TestMethod]
        public void SaveAllDataEmptyLists()
        {
            Bank Leumi = new Bank("Leumi", "Rabin Square 17, Tel Aviv");
            Leumi.SaveAllData();
        }






    }
}
