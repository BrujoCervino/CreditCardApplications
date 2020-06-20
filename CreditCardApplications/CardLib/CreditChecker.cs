using DatabaseBackEnd;

namespace CrudOperations
{
    public abstract class CreditChecker
    {
        // The number of the months until the applicant can reapply with a chance of being approved
        // (One application every 3 months hurts an applicant's score)
        // Readonly because these should depend on the economy:
        // More approvals and higher limits in good economy and vice versa.
        public readonly int MinMonthsUntilCanReapply = 3;
        // The number of the months after which the customer is more likely to be approved
        public readonly int HealthyMonthsUntilCanReapply = 6;

        // Users under this age cannot apply for a credit card
        public readonly int MinAgeUntilCanApply = 18;
        
        // Checks the credit for the given applicant.
        // Returns the approval state (whether the application was approved and the limit given)
        public abstract CreditApproval PerformCreditCheck(in Applicant applicantToCheck);

        // Returns the number of months since the applicant last applied
        // Negative = they have never applied
        // Zero = they applied within this month
        // Positive = they have applied before
        protected abstract int GetMonthsSinceLastApplied(Applicant applicantToCheck); 

        // Calculates and returns the credit limit for applicantToCheck
        // If result is 0 or less, the application failed.
        // Otherwise, the application succeeded.
        protected abstract decimal CalculateCreditLimit(in Applicant applicantToCheck);

        // Returns (applicant's total debt divided by their total income)
        protected abstract decimal GetDebtToIncomeRatio(in Applicant applicantToCheck);

        // Returns (applicant's total debt divided by their credit limit)
        protected abstract decimal GetCreditUtilisation(Applicant applicantToCheck);

        // Returns whether the applicant is (MinAgeUntilCanApply) years old or over
        protected abstract bool ApplicantIsOldEnoughToApplyForACard(in Applicant applicantToCheck);
    }
}
