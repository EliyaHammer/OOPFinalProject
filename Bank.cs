using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinalProject
{
    public class Bank : IBank
    {
        //PROPERTIES
        readonly string _name;
        public string Name
        {
            get { return _name; }
        }
        readonly string _address;
        public string Address
        {
            get { return _address; }
        }
        public int CustomerCounter { get; private set; }

        
        private List<Account> _accounts;
        public List<Account> Accounts_Save { get; private set; }
        private List<Customer> _customers;
        public List<Customer> Customers_Save { get; private set; }
        private Dictionary<int, Customer> _customerByID;
        public Dictionary<int, Customer> CustomerById_Save { get; private set; }
        private Dictionary<int, Customer> _customerByCustomerNumber;
        public Dictionary<int, Customer> CustomerByNumber_Save { get; private set; }
        private Dictionary<int, Account> _accountByNumber;
        public Dictionary<int, Account> AccountByNumber_Save { get; private set; }
        private Dictionary<Customer, List<Account>> _accountsListForCustomer;
        public Dictionary<Customer,List<Account>> AccountsCustomersList_Save { get; private set; }
        private float _totalMoneyInBank;
        public float TotalMoneyInBank_Save { get; private set; }
        private float _profits;
        public float Profits_Save { get; private set; }

        //CTOR
        public Bank (string name, string address)
        {
            this._name = name;
            this._address = address;
            this.CustomerCounter = 0;

            _accounts = new List<Account>();
            _customers = new List<Customer>();
            _customerByID = new Dictionary<int, Customer>();
            _customerByCustomerNumber = new Dictionary<int, Customer>();
            _accountByNumber = new Dictionary<int, Account>();
            _accountsListForCustomer = new Dictionary<Customer, List<Account>>();
        }

        private Bank () { }

        //METHODS
        public Customer GetCustomerByID (int customerId)
        {
            if (_customerByID is null)
                throw new ArgumentNullException("Customers ID dictionary is null");
            else
            {
                if (customerId <= 0)
                    throw new IllegalIDException("ID can't be less or equal to zero.");

                if (_customerByID.ContainsKey(customerId) == false)
                    throw new CustomerNotFoundException("There is no customer with this ID.");
                else
                    return _customerByID[customerId];
            }
        }

        public Customer GetCustomerByNumber (int customerNumber)
        {
            if (_customerByCustomerNumber is null)
                throw new ArgumentNullException("Customer by number list is null.");
            else
            {
                if (customerNumber <= 0)
                    throw new IllegalIDException("Customer number is less or equal to zero.");
                if (_customerByCustomerNumber.ContainsKey(customerNumber) == false)
                    throw new CustomerNotFoundException("Customer with this number was not found.");
                else
                {
                    return _customerByCustomerNumber[customerNumber];
                }
            }
            
        }

        public Account GetAccountByNumber (int accountNumber)
        {
            if (_accountByNumber is null)
                throw new ArgumentNullException("Accounts number list is null.");
            else
            {
                if (accountNumber <= 0)
                    throw new IllegalIDException("Number provided is less or equal to zero.");
                if (_accountByNumber.ContainsKey(accountNumber) == false)
                    throw new AccountNotFoundException("No account with this number found.");
                else
                {
                    return _accountByNumber[accountNumber];
                }
            }
        } 
        
        public List<Account> GetAccountsByCustomer (Customer customerOwner)
        {
            if (_accountsListForCustomer is null || _customers is null)
                throw new ArgumentNullException("Customers Accounts list is null.");
            else
            { 
            if (customerOwner is null)
                throw new ArgumentNullException("Owner given is null.");
            if (_customers.Contains(customerOwner) == false)
                throw new CustomerNotFoundException("The customer given is not registered on this bank.");
            if (_accountsListForCustomer[customerOwner].Count == 0)
                    throw new AccountNotFoundException("No accounts assigned to this customer.");
                else
                    return _accountsListForCustomer[customerOwner];
            }
            
        } 

        public void AddNewCustomer (Customer customerToAdd)
        {
            if (_customerByCustomerNumber is null || _customerByID is null || _customers is null || _accountsListForCustomer is null)
                throw new ArgumentNullException("One of the customers lists is null");
            else
            {
                if (customerToAdd is null)
                throw new ArgumentNullException("Customer given is null.");
                if (_customers.Contains(customerToAdd))
                throw new CustomerAlreadyExistsException("This customer already exists.");

                this._customerByCustomerNumber.Add(customerToAdd.CustomerNumber, customerToAdd);
                this._customerByID.Add(customerToAdd.CustomerID, customerToAdd);
                this.CustomerCounter++;
                this._customers.Add(customerToAdd);
                _accountsListForCustomer.Add(customerToAdd, new List<Account>());
            }
        }


        public void OpenNewAccount (Customer accountOwner)
        {
            Account newAccount = new Account(0, accountOwner);

            if (_accountsListForCustomer is null || _accounts is null || _accountByNumber is null)
                throw new ArgumentNullException("One of the accounts lists is null.");
            else
            {
                if (accountOwner is null)
                    throw new ArgumentNullException("Customer provided is null.");
                if (_customers.Contains(accountOwner) == false)
                    throw new CustomerNotFoundException("Customer is not registered on this bank.");

                _accountsListForCustomer[accountOwner].Add(newAccount);
                _accounts.Add(newAccount);
                _accountByNumber.Add(newAccount.AccountNumber, newAccount);
            }

        }

        public void Deposit (Customer accountToDepositOwner, int accountCreationNumber ,int amountToDeposit)
        {
            GetAccount(accountToDepositOwner, accountCreationNumber).AddMoney(amountToDeposit);
            _totalMoneyInBank += amountToDeposit;
        }

        public void Withdraw (Customer ownerAccountToWithdraw, int accountCreationNumber, int amountToWithdraw)
        {
            GetAccount(ownerAccountToWithdraw, accountCreationNumber).WithdrawMoney(amountToWithdraw);
           _totalMoneyInBank -= amountToWithdraw;
        }

        public float GetTotalBalanceCustomer (Customer customerOwner)
        {
            if (_accountsListForCustomer is null || _customers is null)
                throw new ArgumentNullException("Accounts Customers lists is null");
            else
            {
                if (customerOwner is null)
                    throw new ArgumentNullException("Customer provided is null.");
                if (_customers.Contains(customerOwner) == false)
                    throw new CustomerNotFoundException("The customer provided can not be found");
                if (_accountsListForCustomer[customerOwner].Count == 0)
                    throw new AccountNotFoundException("No accounts assigned to this customer.");

                float totalBalance = 0;
                foreach(Account account in _accountsListForCustomer[customerOwner])
                {
                    totalBalance += account.Balance;
                }

                return totalBalance;
            }
        }

        public float GetAccountBalance (Customer accountOwner, int accountCreationNumber)
        {
            return GetAccount(accountOwner, accountCreationNumber).Balance;
        } 

        public void CloseAccount (Customer customerOwner, int accountCreationNumber)
        {
            if (_accounts is null || _accountByNumber is null || _accountsListForCustomer is null)
            _accounts.Remove(GetAccount(customerOwner, accountCreationNumber));
            _accountByNumber.Remove(GetAccount(customerOwner, accountCreationNumber).AccountNumber);
            _accountsListForCustomer[customerOwner].Remove(GetAccount(customerOwner, accountCreationNumber));
        }

        public void ChargeAnnualCommission (float precentage)
        {
            if (_accounts is null)
                throw new ArgumentNullException("Accounts list is null");
            else
            {
                if (precentage <= 0)
                    throw new IllegalIncomeException("Percentage provided is less or equal to zero");
                if (_accounts.Count == 0)
                    throw new AccountNotFoundException("No accounts on the accounts list");

                foreach (Account account in _accounts)
                {
                    float currentBalance = account.Balance;

                    if (currentBalance > 0)
                    {
                        account.WithdrawMoney((account.Balance * precentage) / 100);
                        _profits += ((currentBalance * precentage) / 100);
                    }

                    else if (currentBalance == 0)
                    {
                        account.WithdrawMoney(account.Balance - ((100 * precentage) / 100) * -1);
                        _profits += ((100 * precentage) / 100);
                    }

                    else
                    {
                        account.WithdrawMoney((((account.Balance * -1) * precentage) / 100) * 2);
                        _profits += (((currentBalance * -1) * precentage) / 100) * 2;
                        _totalMoneyInBank -= (((currentBalance * -1) * precentage) / 100) * 2;
                    }
                }
            }
        }

        public void JoinAccounts (Customer customerOwner, int firstAccountCreationNumber, int secondAccountCreationNumber)
        {
            if ( _accounts is null || _accountByNumber is null)
                throw new ArgumentNullException("List is null");
            else
            { 
            if (firstAccountCreationNumber == secondAccountCreationNumber)
                    throw new IllegalIDException("Accounts' numbers are equal.");

                Account newAccount = GetAccount(customerOwner, firstAccountCreationNumber) + GetAccount(customerOwner, secondAccountCreationNumber);

                _accounts.Remove(GetAccount(customerOwner, firstAccountCreationNumber));
                _accounts.Remove(GetAccount(customerOwner, secondAccountCreationNumber));
                _accountByNumber.Remove(GetAccount(customerOwner, firstAccountCreationNumber).AccountNumber);
                _accountByNumber.Remove(GetAccount(customerOwner, secondAccountCreationNumber).AccountNumber);
                _accountsListForCustomer[customerOwner].Remove(GetAccount(customerOwner, secondAccountCreationNumber));
                _accountsListForCustomer[customerOwner].Remove(GetAccount(customerOwner, firstAccountCreationNumber));
                
                _accounts.Add(newAccount);
                _accountByNumber.Add(newAccount.AccountNumber, newAccount);
                _accountsListForCustomer[customerOwner].Add(newAccount);

            }

        }

        private Account GetAccount(Customer owner, int accountCreationNumber)
        {
            if (_customers is null || _accountsListForCustomer is null)
                throw new ArgumentNullException("Lists are null");
            else
            {
                if (owner is null)
                    throw new ArgumentNullException("Owner provided is null.");
                if (accountCreationNumber <= 0)
                    throw new IllegalIDException("Account creation number is less or equal to zero");
                if (_customers.Contains(owner) == false)
                    throw new CustomerNotFoundException("This customer is not registered in this bank");
                if (_accountsListForCustomer[owner].Count == 0)
                    throw new AccountNotFoundException("No accounts assigned to this customer.");
                if (_accountsListForCustomer[owner].Count < accountCreationNumber)
                    throw new AccountNotFoundException("There is no account with this creation number");

                return _accountsListForCustomer[owner][accountCreationNumber - 1];
            }
        }

        // check
        private void InitializeSaving ()
        {
            this.TotalMoneyInBank_Save = this._totalMoneyInBank;
            this.Profits_Save = this._profits;
            this.Customers_Save = this._customers;
            this.CustomerByNumber_Save = this._customerByCustomerNumber;
            this.CustomerById_Save = this._customerByID;
            this.Accounts_Save = this._accounts;
            this.AccountByNumber_Save = this._accountByNumber;
            this.AccountsCustomersList_Save = this._accountsListForCustomer;
        }
        //check
        private void CloseSaving ()
        {
            this.TotalMoneyInBank_Save = 0;
            this.Profits_Save = 0;
            this.Customers_Save = null;
            this.CustomerByNumber_Save = null;
            this.CustomerById_Save = null;
            this.Accounts_Save = null;
            this.AccountByNumber_Save = null;
            this.AccountsCustomersList_Save = null;
        }
        // check deleted in the end, check with list, check without list
        public void SaveAllData ()
        {
            if (this._customers != null && this._customers.Count != 0)
            {

                foreach (Customer customer in _customers)
                {
                    customer.InitializeSaving(this);
                }
            }

            if (this._accounts != null && this._accounts.Count != 0)
            {
                foreach (Account account in _accounts)
                {
                    account.InitializeSaving(this);
                }
            }

            this.InitializeSaving();

            XmlSerializer serializeBank = new XmlSerializer(typeof(Bank));

            using (Stream fileStream = new FileStream(@"c:\users\me\desktop\bank.xml", FileMode.Create))
            {
                serializeBank.Serialize(fileStream, this);
            }

            this.CloseSaving();

            if (this._customers != null && this._customers.Count != 0)
            {
                foreach (Customer customer in _customers)
                {
                    customer.CloseSaving(this);
                }
            }

            if (this._accounts != null && this._accounts.Count != 0)
            {
                foreach (Account account in _accounts)
                {
                    account.CloseSaving(this);
                }
            }
        }

    }
}
