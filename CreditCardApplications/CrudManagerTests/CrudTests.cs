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
        public void CrudManagerCanCreateValidApplication()
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
        public void CrudManagerCanUpdateValidApplication()
        {
            // Old
            Applicant oldApplicant = CreateApplication();
            int oldCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            Applicant applicantToEdit = oldApplicant.CreateMemberwiseClone();
            applicantToEdit.FirstName = "Uma";
            applicantToEdit.MiddleName = "Karuna";
            applicantToEdit.Surname = "Thurman";
            CrudManager.UpdateApplication(oldApplicant, applicantToEdit);
            // Final
            var finalApplicants = CrudManager.RetrieveAllApplications();
            int finalCount = finalApplicants.Count;
            Applicant finalApplicant = null; ;
            // Pre-assertions
            Assert.DoesNotThrow
            (
                () => finalApplicant = finalApplicants.First(a => a.Equals(applicantToEdit))
            );
            // Assertions
            Assert.AreEqual(finalCount, oldCount);
            Assert.AreNotEqual(oldApplicant, finalApplicant);
            Assert.AreEqual(applicantToEdit, finalApplicant);
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanDeleteValidEntry()
        {
            throw new NotImplementedException();
        }
    }
}