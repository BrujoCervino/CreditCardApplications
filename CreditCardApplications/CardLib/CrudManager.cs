using Globals.Enums;
using DatabaseBackEnd;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CrudOperations 
{
#warning it may be worth having a wrapper class for this: create entry is not the most descriptive term. Better would be CreditApplications.Apply(Applicant a)
    public class CrudManager
    {
        // The appluca
        public readonly Applicant ApplicantData = new Applicant();

        // User makes a new application for a new card:
        // E.g. Bill applies for an Amex Platinum card
        public static void CreateApplication(in Titles title, in string firstName, in string middleName, in string surname, 
            in DateTime birthDate, in string email, in string mobileNum, in string homeTelephoneNum, 
            in decimal annualPersonalIncome, in decimal OtherHouseholdIncome) 
        {
            using var db = new CreditCardApplicationContext();
            db.Applicants.Add(new Applicant
            {
                ApplicantId = 0,
                TitleId = (int)title,
                FirstName = firstName,
                MiddleName = middleName,
                Surname = surname,
                BirthDate = birthDate,
                Email = email,
                MobileNum = mobileNum,
                HomeTelephoneNum = homeTelephoneNum,
                AnnualPersonalIncome = annualPersonalIncome,
                OtherHouseholdIncome = OtherHouseholdIncome
            });
            db.SaveChanges();
        }

        public static List<Applicant> RetrieveAllApplications() 
        {
            using var db = new CreditCardApplicationContext();
            return db.Applicants.ToList();
        }

        // In terms of card application, 
        //  edit an entry due to incorrect data (incorrect income entered etc.)
        public static void UpdateApplication(in Applicant applicantToUpdate) 
        {
            using var db = new CreditCardApplicationContext();
            var applicant = db.Applicants
                .First(a => a.Email == "janine@janinemail.com" && new DateTime(1905, 2, 08) == a.BirthDate);

            applicant.Email = "janine@spleenmail.com";

            db.SaveChanges();
            // #ToDo
#warning Write a test for this^
        }

        public static void DeleteApplication(in Applicant applicant) 
        {
            using var db = new CreditCardApplicationContext();
            var applicants = db.Applicants.ToList(); // For comparison

            db.Applicants.Remove(applicant);
            db.SaveChanges();
#warning Write a test for this^
        }

        // 
        public static void DeleteAllEntries()
        {

        }

    }

}
