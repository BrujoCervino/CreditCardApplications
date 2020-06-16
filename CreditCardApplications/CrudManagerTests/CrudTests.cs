using CardLib;
using DatabaseBackEnd;
using Globals.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CrudManagerTests
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
            List<Applicant> oldApplicants = CrudManager.RetrieveAll();
            CrudManager.CreateEntry(Globals.Enums.Titles.Mr, "Harry", "James", 
                "Potter", new DateTime(2000, 12, 2), "harry.potter@mugglemail.com", "022222222222", "022222222222", 27_000, 9_999);

            List<Applicant> currentApplicants = CrudManager.RetrieveAll();

            Assert.Greater(currentApplicants.Count, oldApplicants.Count);
        }

        [Test(Author ="K McEvaddy")]
        public void CrudManagerCanReadValidEntry()
        {
            List<Applicant> entries = CrudManager.RetrieveAll();
            Assert.NotNull(entries);
            Assert.Greater(entries.Count, 0);
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanUpdateValidEntry()
        {
            throw new NotImplementedException();
        }

        [Test(Author = "K McEvaddy")]
        public void CrudManagerCanDeleteValidEntry()
        {
            throw new NotImplementedException();
        }
    }
}