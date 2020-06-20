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

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanCreateAndDeleteValidApplicant()
        {
            throw new NotImplementedException();
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanCreateValidApplicant()
        {
            // Old
            var oldApplicantsCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            var applicantAdded = CreateApplication();
            var currentApplicantsCount = CrudManager.RetrieveAllApplications().Count;
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
        public void CrudManagerCanUpdateValidEntry()
        {
            // Old
            var oldApplicant = CreateApplication();
            int oldCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            var applicantToEdit = oldApplicant.CreateMemberwiseClone();
            applicantToEdit.FirstName = "Uma";
            applicantToEdit.MiddleName = "Karuna";
            applicantToEdit.Surname = "Thurman";
            // Final
            CrudManager.UpdateApplication(oldApplicant, applicantToEdit);
            // Assertions
            //Assert.AreEqual(preupdatedCount, updatedCount);
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanDeleteValidEntry()
        {
            // Get applicants
            var oldApplicants = CrudManager.RetrieveAllApplications();
            // Create new applicant
            var applicantToAdd = new Applicant()
            {
                TitleId = (int)Titles.Mr,
                FirstName = "Brian",
                Surname = "Griffin",
                Email = "brian@writernet.com",
                BirthDate = new DateTime(2000, 12, 20),
                MobileNum = "00",
                AnnualPersonalIncome = 25_000,
                OtherHouseholdIncome = 1_000,
                HomeTelephoneNum = "00"
            };
            // Get applicant to delete
            // Assume creation works via creation tests
            CrudManager.CreateApplication
            (
               (Titles)applicantToAdd.TitleId,
               applicantToAdd.FirstName,
               null,
               applicantToAdd.Surname,
               applicantToAdd.BirthDate,
               applicantToAdd.Email,
               applicantToAdd.MobileNum,
               applicantToAdd.HomeTelephoneNum,
               applicantToAdd.AnnualPersonalIncome,
               applicantToAdd.OtherHouseholdIncome
            );
            // Edit applicant
            var currentApplicants = CrudManager.RetrieveAllApplications();
            Applicant applicantToDelete = currentApplicants.First(a => a.FirstName == "Brian" && a.Surname == "Griffin");

            CrudManager.DeleteApplication(applicantToDelete);

            var finalApplicants = CrudManager.RetrieveAllApplications();

            Assert.Greater(currentApplicants.Count, oldApplicants.Count);
            Assert.Greater(finalApplicants.Count, currentApplicants.Count);
            Assert.Contains(applicantToAdd, currentApplicants);
            Assert.False(finalApplicants.Contains(applicantToAdd));
        }
    }
}