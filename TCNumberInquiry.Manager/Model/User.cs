using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCNumberInquiry.Core.Entities;

namespace TCNumberInquiry.Module.Model
{
    public class User
    {
        public User(long id, long identyNumber, string firstName,string lastName, DateTime birthDate)
        {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.IdentyNumber = identyNumber;
        }

        public long Id { get; set; }
        [Required,MaxLength(11)]
        public long IdentyNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required,DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public Users GetUsers()
        {
            Users users = new Users();
            users.Id = this.Id;
            users.BirthDate = this.BirthDate;
            users.FirstName = this.FirstName;
            users.LastName = this.LastName;
            users.IdentyNumber = this.IdentyNumber;

            return users;
        }
    }
}
