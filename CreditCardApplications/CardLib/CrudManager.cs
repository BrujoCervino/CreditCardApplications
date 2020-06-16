using Globals.Enums;
using DatabaseBackEnd;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CardLib
{
    public class CrudManager
    {
        public readonly CreditCardApplicant ApplicantData = new CreditCardApplicant();

        // User makes a new application for a new card:
        // Bill applies for an Amex Platinum card
        public static void CreateEntry(in Titles title, in string firstName, in string middleName, in string surname, 
            in DateTime birthDate, in string email, in string mobileNum, in string homeTelephoneNum, 
            in decimal annualPersonalIncome, in decimal OtherHouseholdIncome) 
        {
            using var db = new CreditCardApplicationContext();
            db.Add(new Applicant
            {
                ApplicantId = 0,
                TitleId = (int)title,
                FirstName = firstName,
                MiddleName = middleName,
                Surame = surname,
                BirthDate = birthDate,
                Email = email,
                MobileNum = mobileNum,
                HomeTelephoneNum = homeTelephoneNum,
                AnnualPersonalIncome = annualPersonalIncome,
                OtherHouseholdIncome = OtherHouseholdIncome
            });
            db.SaveChanges();
        }

        public static List<Applicant> RetrieveAll() 
        {
            using var db = new CreditCardApplicationContext();
            return db.Applicants.ToList<Applicant>();
        }

        // In terms of card application, 
        //edit an entry due to incorrect data (incorrect income entered etc.)
        public static void UpdateEntry() { }
        // In terms of card application, erase an entry due to a fraudulent application
        public static void DeleteEntry() { }

        // 
        public static void DeleteAllEntries()
        {

        }

    }

}
