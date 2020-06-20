using CrudOperations;
using DatabaseBackEnd;
using Globals.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudOperationsTests
{
    public class CrudTests
    {
        public Applicant ApplicantToTest;
        private Applicant ApplicantToTest_Edited;

        [SetUp]
        public void Setup()
        {
            ApplicantToTest = new Applicant()
            {
                TitleId = (int)Titles.Ms,
                FirstName = "The",
                Surname = "Bride",
                BirthDate = new DateTime(1970, (int)Months.April, 28),
                Email = "beatrix.kills.bill@assassinmail.com",
                MobileNum = "0800001066",
                HomeTelephoneNum = "01858 444444",
                AnnualPersonalIncome = 100_000,
                OtherHouseholdIncome = 10_000
            };

            ApplicantToTest_Edited = new Applicant()
            {
                TitleId = (int)Titles.Ms,
                FirstName = "Uma",
                MiddleName = "Karuna",
                Surname = "Thurman",
                BirthDate = new DateTime(1970, (int)Months.April, 28),
                Email = "beatrix.kills.bill@assassinmail.com",
                MobileNum = "0800001066",
                HomeTelephoneNum = "01858 444444",
                AnnualPersonalIncome = 100_000,
                OtherHouseholdIncome = 10_000
            };
        }

        [Author("K McEvaddy")]
        [Ignore(reason: "Helper method")]
        public Applicant CreateApplication()
        {
            return CrudManager.CreateApplication
            (
                (Titles)ApplicantToTest.TitleId,
                ApplicantToTest.FirstName,
                ApplicantToTest.MiddleName,
                ApplicantToTest.Surname,
                ApplicantToTest.BirthDate,
                ApplicantToTest.Email,
                ApplicantToTest.MobileNum,
                ApplicantToTest.HomeTelephoneNum,
                ApplicantToTest.AnnualPersonalIncome,
                ApplicantToTest.OtherHouseholdIncome
            );
        }

        [Author("K McEvaddy")]
        [TearDown]
        public void DeleteFromDatabase()
        {
            List<Applicant> applicantsToDelete = CrudManager
                .RetrieveAllApplications()
                .Where(a => a.Equals(ApplicantToTest) || a.Equals(ApplicantToTest_Edited)).ToList();
            if(null != applicantsToDelete && applicantsToDelete.Count > 0)
            {
                foreach(Applicant a in applicantsToDelete)
                {
                    if(null != a)
                    {
                        CrudManager.DeleteApplication(a);
                    }
                }
            }
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanCreateValidApplication()
        {
            // Old
            int oldApplicantsCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            Applicant applicantAdded = CreateApplication();
            int currentApplicantsCount = CrudManager.RetrieveAllApplications().Count;
            // Assertions
            Assert.AreEqual(ApplicantToTest, applicantAdded);
            Assert.Greater(currentApplicantsCount, oldApplicantsCount);
        }


        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanReadAllApplications()
        {
            List<Applicant> entries = CrudManager.RetrieveAllApplications();
            // Assertions
            Assert.NotNull(entries);
            Assert.Greater(entries.Count, 0);
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanUpdateValidApplication()
        {
            // Old
            Applicant originalApplicant = CreateApplication();
            int oldCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            Applicant applicantToEdit = originalApplicant.CreateMemberwiseClone();
            applicantToEdit.FirstName = "Uma";
            applicantToEdit.MiddleName = "Karuna";
            applicantToEdit.Surname = "Thurman";
            CrudManager.UpdateApplication(originalApplicant, applicantToEdit);
            // Final
            List<Applicant> finalApplicants = CrudManager.RetrieveAllApplications();
            int finalCount = finalApplicants.Count;
            // Assertions
            Assert.AreEqual(finalCount, oldCount);
            // The original remains in the database, just edited:
            Assert.False(finalApplicants.Contains(applicantToEdit));
            Assert.True(finalApplicants.Contains(originalApplicant));
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanDeleteValidEntry()
        {
            // Old
            int oldCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            Applicant applicant = CreateApplication();
            int currentCount = CrudManager.RetrieveAllApplications().Count;
            // Final
            CrudManager.DeleteApplication(applicant);
            List<Applicant> finalApplicants = CrudManager.RetrieveAllApplications();
            int finalCount = finalApplicants.Count;
            // Assertions
            Assert.Less(oldCount, currentCount);
            Assert.Less(finalCount, currentCount);
        }
    }
}