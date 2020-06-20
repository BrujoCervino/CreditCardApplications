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
        public readonly Applicant ApplicantData = new Applicant();

        public static Applicant CreateApplication(in Titles title, in string firstName, in string middleName, in string surname, 
            in DateTime birthDate, in string email, in string mobileNum, in string homeTelephoneNum, 
            in decimal annualPersonalIncome, in decimal OtherHouseholdIncome) 
        {
            // Connect to database
            using var db = new CreditCardApplicationContext();
            // Create applicant
            var applicantCreated = new Applicant
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
            };
            db.Applicants.Add(applicantCreated);
            db.SaveChanges();
            // Return applicant
            return applicantCreated;
        }

        public static List<Applicant> RetrieveAllApplications() 
        {
            using var db = new CreditCardApplicationContext();
            return db.Applicants.ToList();
        }

        public static bool UpdateApplication(Applicant currentApplicant, in Applicant updatedApplicant) 
        {
            using var db = new CreditCardApplicationContext();
            //var applicant = db.Applicants
            //    .First(a => a.Email == "janine@janinemail.com" && new DateTime(1905, 2, 08) == a.BirthDate);
            var applicants = RetrieveAllApplications();
            int index = applicants.IndexOf(currentApplicant);
            if( index >= 0)
            {
                currentApplicant = applicants[index];
                if (currentApplicant.OverwriteIfDifferentAndValid(updatedApplicant))
                {
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
            //applicant.Email = "janine@spleenmail.com";
#warning Write a test for this^
        }

        public static void DeleteApplication(in Applicant applicant) 
        {
            using var db = new CreditCardApplicationContext();
            var applicants = db.Applicants.ToList();

            db.Applicants.Remove(applicant);
            db.SaveChanges();
#warning Write a test for this^
        }
    }

}
