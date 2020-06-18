using DatabaseBackEnd;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrudManager
{
    // Goals
    interface ICreditChecker
    {
        bool CheckCredit(in Applicant applicantToCheck);
    }
}
