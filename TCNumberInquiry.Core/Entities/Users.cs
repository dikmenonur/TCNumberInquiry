using System;

namespace TCNumberInquiry.Core.Entities
{
    public class Users
    {
        public long Id { get; set; }
        public long IdentyNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
