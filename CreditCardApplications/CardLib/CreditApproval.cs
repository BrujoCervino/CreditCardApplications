using System;
using System.Collections.Generic;
using System.Text;

namespace CrudOperations 
{
    // The data passed around for credit limit given and whether the application succeeded.
    // Used Factory Pattern for clarity and simplicity:
    // For failures, CreditApproval.Failed()
    // For success, CreditApproval.Succeeded(in decimal creditLimit)
    public sealed class CreditApproval
    {
        // The minimal credit limit to give to customers.
#warning This should live elsewhere and change according to global circumstances (less given in a recession)
        public const decimal MinCreditLimit = 1_000;
        
        // Whether the application was approved
        public readonly bool Approved;
        // The credit limit given (if the application was approved)
        public readonly decimal CreditLimit;

        // Disapproves an application
        public static CreditApproval Failed() 
            => new CreditApproval();

        // Approves an application, granting a specified credit limit 
        public static CreditApproval Succeeded(in decimal creditLimit) 
            => new CreditApproval(creditLimit);

        // Constructor for a failed application
        private CreditApproval()
        {
            Approved = false;
            CreditLimit = 0;
        }

        // Constructor for an approved application
        private CreditApproval(in decimal creditLimit)
        {
            Approved = true;
            CreditLimit = Math.Max(MinCreditLimit, creditLimit);
        }
    }
}
