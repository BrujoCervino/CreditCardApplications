using Globals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
        // Could not call the protected method "memberwiseclone" because I want applicantId to not carry over between clones
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

        // Very ugly but it works. I'd do this with reflection next time (via an array of properties).
        public bool OverwriteIfDifferentAndValidExceptId(in Applicant newApplicant)
        {
            bool anythingChanged = false;
            if(TitleId != newApplicant.TitleId && ((int[])Enum.GetValues(typeof(Titles))).Contains(newApplicant.TitleId))
            {
                TitleId = newApplicant.TitleId;
                anythingChanged = true;
            }
            if(FirstName != newApplicant.FirstName && null != newApplicant.FirstName)
            {
                FirstName = newApplicant.FirstName;
                anythingChanged = true;
            }
            if (MiddleName != newApplicant.MiddleName && null != newApplicant.MiddleName)
            {
                MiddleName = newApplicant.MiddleName;
                anythingChanged = true;
            }
            if (Surname != newApplicant.Surname && null != newApplicant.Surname)
            {
                Surname = newApplicant.Surname;
                anythingChanged = true;
            }
            if(BirthDate != newApplicant.BirthDate)
            {
                BirthDate = newApplicant.BirthDate;
                anythingChanged = true;
            }
            if (Email != newApplicant.Email && null != newApplicant.Email)
            {
                Email = newApplicant.Email;
                anythingChanged = true;
            }
            if (MobileNum != newApplicant.MobileNum && null != newApplicant.MobileNum)
            {
                MobileNum = newApplicant.MobileNum;
                anythingChanged = true;
            }
            if (HomeTelephoneNum != newApplicant.HomeTelephoneNum && null != newApplicant.HomeTelephoneNum)
            {
                HomeTelephoneNum = newApplicant.HomeTelephoneNum;
                anythingChanged = true;
            }
            if (AnnualPersonalIncome != newApplicant.AnnualPersonalIncome)
            {
                AnnualPersonalIncome = newApplicant.AnnualPersonalIncome;
                anythingChanged = true;
            }
            if (OtherHouseholdIncome != newApplicant.OtherHouseholdIncome)
            {
                OtherHouseholdIncome = newApplicant.OtherHouseholdIncome; 
                anythingChanged = true;
            }
            return anythingChanged;
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
