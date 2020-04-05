using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Customer
    {
        //VARIABLES + PROPERTIES
        private static int NUMBER_OF_TOTAL_CUSTOMERS;
        public int NumberOfTotalCustomers_Save { get; private set; }
        readonly int _customerID;
        public int CustomerID
        {
            get { return _customerID; }
        }
        readonly int _customerNumber;
        public int CustomerNumber
        {
            get { return _customerNumber; }
        }
        public string Name { get; private set; }
        public int PhonerNumber { get; private set; }

        //CTOR
        private Customer() { }

        public Customer (int customerId, string name, int phoneNumber)
        {
            this._customerID = customerId;
            this.Name = name;
            this.PhonerNumber = phoneNumber;
            NUMBER_OF_TOTAL_CUSTOMERS++;
            if (NUMBER_OF_TOTAL_CUSTOMERS == 0)
                throw new ArgumentNullException("Number of customers is zero, can't assign to customer.");
            else
            {
                this._customerNumber = NUMBER_OF_TOTAL_CUSTOMERS;
            }
        }

        //METHODS
        public static bool operator == (Customer a, Customer b)
        {
            if (a is null || b is null)
            {
                throw new ArgumentNullException("One of the customers is null. Cannot compare.");
            }
            else
            {
                if (a.CustomerNumber == b.CustomerNumber)
                    return true;
                else
                    return false;
            }
        }

        public static bool operator !=(Customer a, Customer b)
        {
            if (a is null || b is null)
                throw new ArgumentNullException("One of the customers is null. Cannot compare.");
            else
            {
                return !(a == b);
            }
        }

        public override bool Equals(object a)
        {
            if (a is null)
                throw new ArgumentNullException("One of the customers is null. Cannot compare.");
            if ((a is Customer) == false)
                throw new TypeAccessException("Object inserted is not of type Customer.");
            else
                return this == (a as Customer);
        }

        public override int GetHashCode()
        {
            if (this.CustomerNumber == 0)
                throw new ArgumentNullException($"{this.Name}'s customer number is missing (is zero).");
            else
                return this.CustomerNumber;
        }

        public void InitializeSaving (Bank caller)
        {
            if ((caller.GetCustomerByID(this.CustomerID) is null) == false)
            {
                this.NumberOfTotalCustomers_Save = NUMBER_OF_TOTAL_CUSTOMERS;
            }
        }

        public void CloseSaving (Bank caller)
        {
            if ((caller.GetCustomerByID(this.CustomerID) is null) == false)
            {
                this.NumberOfTotalCustomers_Save = 0;
            }
        }
    }
}
