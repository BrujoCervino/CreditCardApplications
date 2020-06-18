using DatabaseBackEnd;
using Globals.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace CrudOperations
{
    // A dummy credit checker -
    // Use this until we can get an API from Experian/TransUnion/Equifax (credit bureaus).
    // Limitation(s):
    // - Assumes the customer will only ever have/have had cards from the bank to which this credit checker belongs (they could be massively indebted to another bank)
    public class InternalCreditChecker : CreditChecker
    {
        // Checks the credit for the given applicant.
        // Returns the approval state (whether the application was approved and the limit given)
        public override CreditApproval PerformCreditCheck(in Applicant applicantToCheck)
        {
            CreditApproval approval = null;

            if (null != applicantToCheck)
            {
                // Aproval details
                decimal creditLimitToGive = decimal.MinValue;
                bool approved = false;

                // Credit check details
                int monthsSinceLastApplication = GetMonthsSinceLastApplied(applicantToCheck);
                decimal debtToIncomeRatio = GetDebtToIncomeRatio(applicantToCheck);
                decimal creditUtilisation = GetCreditUtilisation(applicantToCheck);

                // Evaluate how long ago the applicant last applied (if ever) 
                //  (longer ago is better, because the applicant seems less desperate (more likely to pay on time))
                // If has applied recently,
                if (monthsSinceLastApplication < MinMonthsUntilCanReapply)
                {

                } // If applied between 3 and 6 months ago,
                else if (monthsSinceLastApplication >= MinMonthsUntilCanReapply && monthsSinceLastApplication < HealthyMonthsUntilCanReapply)
                {

                } // Else: applied longer than 6 months ago or never applied:
                else
                {

                }

                // Evaluate how much total debt the applicant has compared to their total income:
                //  (less is better, because the applicant seems less desperate (more likely to pay on time))


                // Evaluate how much of the customer's credit limit (if they have any cards) they are using
                //  (less is better, because the applicant seems less desperate (more likely to pay on time))


                approval = approved ? CreditApproval.Succeeded(creditLimitToGive) : CreditApproval.Failed();

            }
            return approval;
        }

        // Returns the number of months since the applicant last applied
        // Negative = they have never applied
        // Zero = they applied within this month
        // Positive = they have applied before
        protected override int GetMonthsSinceLastApplied(Applicant applicantToCheck)
        {
            throw new NotImplementedException();
            ImmutableList<Applicant> applicants = CrudManager.RetrieveAll().ToImmutableList();
            
            // If this customer is valid and is already in our records,
            if (null != applicantToCheck 
                && applicants != null 
                && applicants
                .Exists(a => 
                    a.TitleId == applicantToCheck.TitleId
                    && a.FirstName == applicantToCheck.FirstName
                    && a.MiddleName == applicantToCheck.MiddleName
                    && a.Surname == applicantToCheck.Surname
                    && a.Email == applicantToCheck.Email))
            {
                return -1;
                //return (DateTime.Now - applicantToCheck.DateLastApplied.Month);
            }
            return -1;
        }

        // Calculates and returns the credit limit for applicantToCheck
        // If result is 0 or less, the application failed.
        // Otherwise, the application succeeded.
        protected override decimal CalculateCreditLimit(in Applicant applicantToCheck)
        {
            throw new NotImplementedException();
        }

        // Returns applicant's total debt divided by their total income
        protected override decimal GetDebtToIncomeRatio(in Applicant applicantToCheck)
        {
            throw new NotImplementedException();
        }

        // Returns (applicant's total debt divided by their credit limit)
        protected override decimal GetCreditUtilisation(Applicant applicantToCheck)
        {
            throw new NotImplementedException();
        }

        // Returns whether the applicant is (MinAgeUntilCanApply) years old or over
        protected override bool ApplicantIsOldEnoughToApplyForACard(in Applicant applicantToCheck)
        {
            throw new NotImplementedException();
        }
    }
}
