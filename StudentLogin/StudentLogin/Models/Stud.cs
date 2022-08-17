using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentLogin.Models
{
    public class Stud
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public string mobileno { get; set; }
    }
}