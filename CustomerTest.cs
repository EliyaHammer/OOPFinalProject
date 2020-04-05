using System;
using FinalProject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFinalProject
{
    [TestClass]
    public class CustomerTest
    {
        //CTOR TESTS:
        [TestMethod]
        public void Constructor()
        {
            Customer customerTest = new Customer(313589913,"customerTest", 0546913832);
            Assert.AreEqual("customerTest", customerTest.Name);
            Assert.AreEqual(313589913, customerTest.CustomerID);
            Assert.AreEqual(0546913832, customerTest.PhonerNumber);
            Assert.AreEqual(1, customerTest.CustomerNumber);
        }


        //OPERATOR == TESTS:
        [TestMethod]
        public void OperatorEqualizer ()
        {
            Customer customerTest = new Customer(313589913, "customerTest", 0546913832);
            Customer customerTestTwo = new Customer(3135133, "customerTestTwo", 546913832);
            Assert.IsTrue(customerTest == customerTest);
            Assert.IsFalse(customerTest == customerTestTwo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OperatorEqualizerNullException()
        {
            Customer customerTest = null;
            Customer customerTestTwo = new Customer(3135133, "customerTestTwo", 546913832);
            bool areEqual = (customerTest == customerTestTwo);
        }


        //OPERATOR != TESTS:
        [TestMethod]
        public void OperatorNotEqualizer()
        {
            Customer customerTest = new Customer(313589913, "customerTest", 0546913832);
            Customer customerTestTwo = new Customer(3135133, "customerTestTwo", 546913832);
            Assert.IsFalse(customerTest != customerTest);
            Assert.IsTrue(customerTest != customerTestTwo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OperatorNotEqualizerNullException()
        {
            Customer customerTest = null;
            Customer customerTestTwo = new Customer(3135133, "customerTestTwo", 546913832);
            bool areEqual = (customerTest != customerTestTwo);
        }

        //EQUALS TESTS:

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EqualsNullExceptionTwo()
        {
            Customer customerTest = null;
            Customer customerTestTwo = new Customer(3135133, "customerTestTwo", 546913832);
            bool areEqual = customerTestTwo.Equals(customerTest);
        }

        [TestMethod]
        [ExpectedException(typeof(TypeAccessException))]
        public void EqualsWrongTypeException()
        {
            Customer customer = new Customer(1212, "eliya", 05454);
            Account accountTest = new Account(3000, customer);
            bool equal = customer.Equals(accountTest);
        }

        [TestMethod]
        public void Equals()
        {
            Customer customerTest = new Customer(31351336, "customerTest", 0546913832);
            Customer customerTestTwo = new Customer(3135133, "customerTestTwo", 546913832);
            Assert.IsTrue(customerTest.Equals(customerTest));
            Assert.IsFalse(customerTest.Equals(customerTestTwo));
        }


        //HASHCODETESTS:
        [TestMethod]
        public void GetHashCodeTest ()
        {
            Customer customerTest = new Customer(3135133, "customerTestTwo", 546913832);
            Assert.AreEqual(customerTest.CustomerNumber, customerTest.GetHashCode());
        }

        //INITIALIZE SAVING TESTS:
        [TestMethod]
        public void InitializeSaving()
        {
            Bank Leumi = new Bank("Leumi", "Tel Aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Leumi.AddNewCustomer(accountOwner);
            accountOwner.InitializeSaving(Leumi);
            Assert.AreEqual(1, accountOwner.NumberOfTotalCustomers_Save);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void InitializeSavingNotFound()
        {
            Bank Leumi = new Bank("Leumi", "Tel Aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            accountOwner.InitializeSaving(Leumi);
        }

        //CLOSE SAVING TESTS:
        [TestMethod]
        public void CloseSaving()
        {
            Bank Leumi = new Bank("Leumi", "Tel Aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            Leumi.AddNewCustomer(accountOwner);
            accountOwner.InitializeSaving(Leumi);
            accountOwner.CloseSaving(Leumi);
            Assert.AreEqual(0, accountOwner.NumberOfTotalCustomers_Save);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomerNotFoundException))]
        public void CloseSavingNotFound()
        {
            Bank Leumi = new Bank("Leumi", "Tel Aviv");
            Customer accountOwner = new Customer(2323, "eliya", 05454);
            accountOwner.CloseSaving(Leumi);
        }

    }
}
