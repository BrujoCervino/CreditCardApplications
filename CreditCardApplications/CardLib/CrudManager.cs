using Globals.Enums;
using DatabaseBackEnd;
using System;

namespace CardLib
{
    public class CrudManager
    {
        public readonly CreditCardApplicant ApplicantData = new CreditCardApplicant();

        // User makes a new application for a new card:
        // Bill applies for an Amex Platinum card
        public static void CreateEntry() { }

        public static void RetrieveAll() { }

        // In terms of card application, 
        //edit an entry due to incorrect data (incorrect income entered etc.)
        public static void UpdateEntry() { }
        // In terms of card application, erase an entry due to a fraudulent application
        public static void DeleteEntry() { }

        public static void Update(Titles title, string firstName, string middleName,
            string surname, DateTime birthDate, string email, int mobileNumber, int homePhoneNumber,
            string houseNumber, string postalCode,  decimal annualPersonalIncome, decimal otherHouseholdIncome)
        {
            
        }
    }

}
