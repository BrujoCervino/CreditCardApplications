using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBackEnd
{
    // Contains data for an applicant: income, etc.
    public class CreditCardApplicant
    {
        // ~~ Personal Details ~~
        public Globals.Enums.Titles Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        // ~~ Contact Details ~~
        public string Email { get; set; }
        public int MobileNumber { get; set; }
        public int HomePhoneNumber { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }

        // ~~ Financial Details ~~
        public decimal AnnualPersonalIncome { get; set; }
        public decimal OtherHouseholdIncome { get; set; }
    }
}
