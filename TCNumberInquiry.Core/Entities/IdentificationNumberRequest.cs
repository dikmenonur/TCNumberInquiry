using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCNumberInquiry.Core.Entities
{
    public class IdentificationNumberRequest
    {
        public IdentificationNumberRequest()
        {
        }
        public IdentificationNumberRequest(long identificationNumber, string fistName, string lastNane, int birthDate)
        {
            this.IdentificationNumber = identificationNumber;
            this.FirstName = fistName;
            this.LastName = lastNane;
            this.BirthDate = birthDate;
        }

        public long IdentificationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthDate { get; set; }

    }
}
