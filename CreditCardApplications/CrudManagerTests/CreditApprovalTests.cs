using CrudOperations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudOperationsTests
{
    // Mainly for future-proofing
    public class CreditApprovalTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test(Author = "K McEvaddy")]
        public void FailedGivesZeroCreditLimit()
        {
            // Arrange, Act
            CreditApproval creditApproval = CreditApproval.Failed();
            // Assert
            Assert.Zero(creditApproval.CreditLimit);
        }

        [Test(Author = "K McEvaddy")]
        public void FailedActuallyFails()
        {
            // Arrange, Act
            CreditApproval creditApproval = CreditApproval.Failed();
            // Assert
            Assert.IsFalse(creditApproval.Approved);
        }

        [Test(Author = "K McEvaddy")]
        public void SucceededActuallySucceeds()
        {
            // Arrange, Act
            CreditApproval creditApproval = 
                CreditApproval.Succeeded(CreditApproval.MinCreditLimit);
            // Assert
            Assert.IsTrue(creditApproval.Approved);
        }

        [Test(Author = "K McEvaddy")]
        public void SucceededGivesValidCreditLimitIfAttemptBelowMinimum()
        {
            // Arrange, Act
            CreditApproval creditApproval = CreditApproval.Succeeded(0M);
            // Assert
            Assert.AreEqual(CreditApproval.MinCreditLimit, creditApproval.CreditLimit);
        }

        [Test(Author = "K McEvaddy")]
        public void SucceededGivesValidCreditLimitIfAttemptAboveMinimum() 
        {
            // Arrange, Act
            CreditApproval creditApproval = CreditApproval.Succeeded(CreditApproval.MinCreditLimit + 10M);
            // Assert
            Assert.Greater(creditApproval.CreditLimit, CreditApproval.MinCreditLimit);
        }
    }
}
