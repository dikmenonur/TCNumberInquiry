using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCNumberInquiry.Core.Entitiy
{
    public class IdentificationNumberRequest 
    {
        public IdentificationNumberRequest(long identificationNumber, string fistName, string lastNane, int birthDate)
        {
            this.IdentificationNumber = identificationNumber;
            this.FistName = fistName;
            this.LastNane = lastNane;
            this.BirthDate = birthDate;
        }

        public long IdentificationNumber { get; set; }
        public string FistName { get; set; }
        public string LastNane { get; set; }
        public int BirthDate { get; set; }

    }
}
