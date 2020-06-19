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
        [SetUp]
        public void Setup()
        {
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanCreateValidEntry()
        {
            List<Applicant> oldApplicants = CrudOperations.CrudManager.RetrieveAll();
            CrudOperations.CrudManager.CreateEntry(Titles.Mr, "Harry", "James",
                "Potter", new DateTime(2000, 12, 2), "harry.potter@mugglemail.com", "022222222222", "022222222222", 27_000, 9_999);

            List<Applicant> currentApplicants = CrudOperations.CrudManager.RetrieveAll();

            Assert.Greater(currentApplicants.Count, oldApplicants.Count);
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanReadValidEntry()
        {
            List<Applicant> entries = CrudOperations.CrudManager.RetrieveAll();
            Assert.NotNull(entries);
            Assert.Greater(entries.Count, 0);
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanUpdateValidEntry()
        {
            // Get applicants
            var oldApplicants = CrudManager.RetrieveAll();
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
            // Get applicant to edit
            CrudManager.CreateEntry
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
            var currentApplicants = CrudManager.RetrieveAll();
            var applicantToEdit = currentApplicants.First(a => a.FirstName == "Brian" && a.Surname == "Griffin");
            // Get database again
            applicantToEdit.TitleId = (int)Titles.Mrs;
            applicantToEdit.FirstName = "Lois";
            //CrudManager.UpdateEntry();
            // Get edited applicant


            //Assert.AreNotEqual();
            Assert.Fail();
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanDeleteValidEntry()
        {
            // Get applicants
            var oldApplicants = CrudManager.RetrieveAll();
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
            CrudManager.CreateEntry
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
            var currentApplicants = CrudManager.RetrieveAll();
            Applicant applicantToDelete = currentApplicants.First(a => a.FirstName == "Brian" && a.Surname == "Griffin");

            CrudManager.DeleteEntry(applicantToDelete);

            var finalApplicants = CrudManager.RetrieveAll();

            Assert.Greater(currentApplicants.Count, oldApplicants.Count);
            Assert.Greater(finalApplicants.Count, currentApplicants.Count);
            Assert.Contains(applicantToAdd, currentApplicants);
            Assert.False(finalApplicants.Contains(applicantToAdd));
        }
    }
}