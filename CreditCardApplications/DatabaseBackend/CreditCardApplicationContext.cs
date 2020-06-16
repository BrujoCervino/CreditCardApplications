using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBackEnd
{
    public class CreditCardApplicationContext : DbContext
    {
        public DbSet<Applicant> Applicants { get; set; }

#warning Hide the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CreditCardApplicationsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }

    public class Applicant
    {
        public int ApplicantId { get; set; }
        public int TitleId { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
#warning need to change name from Surame to Surname
        public string Surame { get; set; }

        public DateTime BirthDate { get; set; }

        public string Email { get; set; }
        public string MobileNum { get; set; }
        public string HomeTelephoneNum { get; set; }
        public decimal AnnualPersonalIncome { get; set; }
        public decimal OtherHouseholdIncome { get; set; }
    }

}
