using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Account
    {
        //PROPERTIES AND VARIABLES
        private static int TOTAL_NUMBER_OF_ACCOUNTS;
        public int TotalNumberOfAccounts_Save { get; private set; }
        readonly int _accountNumber;
        public int AccountNumber
        {
            get { return _accountNumber; }
        }
        readonly Customer _accountOwner;
        public Customer AccountOwner
        {
            get { return _accountOwner; }
        }
        private int MaxMinusAllowed { get; set; }
        public int MaxMinusAllowed_Save { get; private set; }
        public float Balance { get; private set; }

        //CTOR
        private Account () { }

        public Account(int monthlyIncome, Customer accountOwner)
        {
            if (accountOwner is null)
            {
                throw new ArgumentNullException("Account owner can't be null.");
            }
            else
            {
                this._accountOwner = accountOwner;
            }

            if (monthlyIncome < 0)
            {
                throw new IllegalIncomeException("Monthly income cannot be less than zero.");
            }
            else
            {
                this.MaxMinusAllowed = (monthlyIncome * 3);
                this.Balance = 0;
            }

            TOTAL_NUMBER_OF_ACCOUNTS++;

            if (TOTAL_NUMBER_OF_ACCOUNTS == 0)
            {
                throw new ArgumentNullException("Account number is zero, cannot be assigned.");
            }
            else
            {
                this._accountNumber = TOTAL_NUMBER_OF_ACCOUNTS;
            }
        }


        //METHODS
        public static bool operator ==(Account a, Account b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("One of the accounts is null. Cannot compare");
            else
                return a.AccountNumber == b.AccountNumber;
        }

        public static bool operator !=(Account a, Account b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("One of the accounts is null. Cannot compare");
            else
                return !(a == b);
        }

        public override bool Equals(object a)
        {
            if (a is null)
                throw new ArgumentNullException("Object inserted is null. Cannot compare");
            if ((a is Account) == false)
                throw new TypeAccessException("The object inserted is not from Account type.");

             return this.AccountNumber == (a as Account).AccountNumber;
        }

        public override int GetHashCode()
        {
            return this.AccountNumber;
        } 

        public static Account operator + (Account a, Account b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("One of the accounts is null.");
            if (a == b)
                throw new SameAccountException("Accounts given are both the same account.");
            if (a.AccountOwner != b.AccountOwner)
                throw new CustomerAccessabilityException("The accounts given have different owners.");
            else
            {
                Account resultAccount = new Account((a.MaxMinusAllowed / 3) + (b.MaxMinusAllowed / 3), a.AccountOwner);
                resultAccount.Balance = (a.Balance + b.Balance);
                return resultAccount;
            }
        }

        public void AddMoney(float amount)
        {
            if (amount <= 0)
                throw new IllegalIncomeException("Amount to deposit is less or equal to zero.");
            if (this.MaxMinusAllowed == 0)
                this.MaxMinusAllowed = (int)((amount * 3) * -1);

            this.Balance += amount;
        }

        public void WithdrawMoney (float amount)
        {
            if (amount <= 0)
                throw new IllegalIncomeException("Amount to deposit is less or equal to zero.");
            if ((this.Balance - amount) < this.MaxMinusAllowed)
                throw new IllegalIncomeException("You don't have enough minus allowed to withdraw this amount.");
            this.Balance -= amount;
        }

        // check initialized, check wrong id, check exception
        public void InitializeSaving (Bank caller)
        {
            if ((caller.GetAccountByNumber(this.AccountNumber) is null) == false)
            {
                this.TotalNumberOfAccounts_Save = TOTAL_NUMBER_OF_ACCOUNTS;
                this.MaxMinusAllowed_Save = this.MaxMinusAllowed;
            }
        }

        // check initialized, check wrong id, check exception
        public void CloseSaving (Bank caller)
        {
            if ((caller.GetAccountByNumber(this.AccountNumber) is null) == false)
            {
                this.TotalNumberOfAccounts_Save = 0;
                this.MaxMinusAllowed_Save = 0;
            }
        }

    }
} 
