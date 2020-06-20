using CrudOperations;
using DatabaseBackEnd;
using Globals.Enums;
using Microsoft.EntityFrameworkCore;
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

        [Author("K McEvaddy")]
        [Ignore(reason: "Helper method")]
        public bool DeleteFromDatabase(List<Applicant> applicantsToDelete)
        {
            if(null != applicantsToDelete && applicantsToDelete.Count > 0)
            {
                foreach(Applicant a in applicantsToDelete)
                {
                    if(null != a)
                    {
                        CrudManager.DeleteApplication(a);
                    }
                }
                return true;
            }
            return false;
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
            Applicant oldApplicant = CreateApplication();
            int oldCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            Applicant applicantToEdit = oldApplicant.CreateMemberwiseClone();
            applicantToEdit.FirstName = "Uma";
            applicantToEdit.MiddleName = "Karuna";
            applicantToEdit.Surname = "Thurman";
            CrudManager.UpdateApplication(oldApplicant, applicantToEdit);
            // Final
            List<Applicant> finalApplicants = CrudManager.RetrieveAllApplications();
            int finalCount = finalApplicants.Count;
            Applicant finalApplicant = null; 
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
        public void CrudManagerCanDeleteValidEntry_Backup()
        {
            // Old
            int oldCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            Applicant applicant = CreateApplication();
            int currentCount = CrudManager.RetrieveAllApplications().Count;
            // Final
            CrudManager.DeleteApplication(applicant);
            var finalApplicants = CrudManager.RetrieveAllApplications();
            int finalCount = finalApplicants.Count;
            Applicant finalApplicant = null;
            // Pre-assertions
            TestDelegate result = () => finalApplicant = finalApplicants.First(a => a.Equals(applicant));
            Assert.Throws<InvalidOperationException>(result);
            // Assertions
            Assert.Less(oldCount, currentCount);
            Assert.Less(finalCount, currentCount);
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanDeleteValidEntry1()
        {
            // Old
            int oldCount = CrudManager.RetrieveAllApplications().Count;
            // Current
            Applicant applicant = CreateApplication();
            
            using(var db = new CreditCardApplicationContext())
            {
                db.Update(applicant);
            }
            int currentCount = CrudManager.RetrieveAllApplications().Count;
            //using (var db = new CreditCardApplicationContext())
            //{
            //    db.Update(applicant);
            //}
            // GET APPLICANT FROM THE DATABASE TO GET ITS KEY THEN DELETE (AVOIDS EXCEPTIONS)

            // using (var db = new CreditCardApplicationContext())
            // {
            //     applicant = db.Applicants.First(a => a.Equals(applicant));
            //
            //       // ; CrudManager.RetrieveAllApplications().First(a => a.Equals(applicant));
            // }

            // Final

            CrudManager.DeleteApplication(CreateApplication());

            var finalApplicants = CrudManager.RetrieveAllApplications();
            int finalCount = finalApplicants.Count;
            Applicant finalApplicant = null;
            // Pre-assertions
            TestDelegate result = () => finalApplicant = finalApplicants.First(a => a.Equals(applicant));
            Assert.Throws<InvalidOperationException>(result);
            // Assertions
            Assert.Less(oldCount, currentCount);
            Assert.Less(finalCount, currentCount);
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

            var finalApplicants = CrudManager.RetrieveAllApplications();
            int finalCount = finalApplicants.Count;
            Applicant finalApplicant = null;
            // Pre-assertions
            //TestDelegate result = () => finalApplicant = finalApplicants.First(a => a.Equals(applicant));
            //Assert.Throws<InvalidOperationException>(result);
            // Assertions
            Assert.Less(oldCount, currentCount);
            Assert.Less(finalCount, currentCount);
        }
    }
}