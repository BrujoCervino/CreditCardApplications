using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseBackEnd
{
    // Explicitly shallow clone: clearer than native ICloneable (which produces ambiguously deep or shallow copies)
    public interface IMemberwiseCloneable<TCloneType>
    {
        TCloneType CreateMemberwiseClone();
    }
    
    public partial class Applicant : IMemberwiseCloneable<Applicant>
    {
        public Applicant CreateMemberwiseClone()
        {
            return new Applicant
            {
                TitleId = this.TitleId,
                FirstName = this.FirstName,
                MiddleName = this.MiddleName,
                Surname = this.Surname,
                BirthDate = this.BirthDate,
                Email = this.Email,
                MobileNum = this.MobileNum,
                HomeTelephoneNum = this.HomeTelephoneNum,
                AnnualPersonalIncome = this.AnnualPersonalIncome,
                OtherHouseholdIncome = this.OtherHouseholdIncome
            };
        }

        public override bool Equals(object obj)
        {
            if (null != obj)
            {
                Applicant a = (Applicant)obj;
                return
                    TitleId == a.TitleId
                    && FirstName == a.FirstName
                    && MiddleName == a.MiddleName
                    && Surname == a.Surname
                    && BirthDate == a.BirthDate
                    && Email == a.Email
                    && MobileNum == a.MobileNum
                    && HomeTelephoneNum == HomeTelephoneNum
                    && AnnualPersonalIncome == a.AnnualPersonalIncome
                    && OtherHouseholdIncome == a.OtherHouseholdIncome; 
            }
            return false;
        }

        public bool EqualsIncludingId(in Applicant obj)
        {
            if(null != obj)
            {
                return Equals(obj) 
                    && obj.ApplicantId == ApplicantId;
            }
            return false;
        }

        // Not happy with this (hash codes are dodgy with entity framework workflows): but research has provided no alternative.
        public override int GetHashCode()
        {
            return ApplicantId;
        }
    }
}
