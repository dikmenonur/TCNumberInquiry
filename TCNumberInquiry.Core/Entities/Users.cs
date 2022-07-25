using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TCNumberInquiry.Core.Entities
{
    
    [Table("user")]
    public class Users
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("IdentyNumber")]
        public long IdentyNumber { get; set; }
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
