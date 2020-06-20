using DatabaseBackEnd;
using Globals.Enums;
using NUnit.Framework;
using System;

namespace DatabaseBackEndTests
{
    public class ApplicantTests
    {
        public Applicant ApplicantToTest;
        
        [SetUp]
        public void Setup()
        {
            ApplicantToTest = new Applicant()
            {
                TitleId = (int)Titles.Ms,
                FirstName = "Elle",
                Surname = "Driver",
                BirthDate = new DateTime(1960, (int)Months.December, 03),
                Email = "elle.trykillbea@assassinmail.com",
                MobileNum = "0800001066",
                HomeTelephoneNum = "01858 444444",
                AnnualPersonalIncome = 80_000,
                OtherHouseholdIncome = 15_000
            };
        }

        [Test(Author = "K McEvaddy")]
        public void EqualsExcludesId()
        {
            // Arrange
            Applicant cloneExceptId = ApplicantToTest.CreateMemberwiseClone();
            // Act
            cloneExceptId.ApplicantId = ApplicantToTest.ApplicantId + 1;
            // Assert
            Assert.AreNotEqual(cloneExceptId.ApplicantId, ApplicantToTest.ApplicantId);
            Assert.AreEqual(cloneExceptId, ApplicantToTest);
        }

        [Test(Author = "K McEvaddy")]
        public void CloneMemberwiseWorks()
        {
            // Arrange
            Applicant clone = ApplicantToTest.CreateMemberwiseClone();
            Applicant cloneImproper = ApplicantToTest.CreateMemberwiseClone();
            // Act
            cloneImproper.FirstName = "Daryl";
            cloneImproper.Surname = "Hannah";
            // Assert
            Assert.AreEqual(clone, ApplicantToTest);
            Assert.AreNotEqual(cloneImproper, ApplicantToTest);
        }
    }
}