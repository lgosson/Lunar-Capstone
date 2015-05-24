using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LunarLogic.Models
{
    public class Contact
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "*First name is required")]
        [StringLength(20, ErrorMessage = "*First name cannot be larger than 20 characters")]
        public string FirstName { get; set; }
        [StringLength(20, ErrorMessage = "*Last name cannot be larger than 20 characters")]
        public string LastName { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "*Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "*Please enter valid email")]
        public string Email { get; set; }
        [StringLength(200, ErrorMessage = "*Comment must not be larger than 200 characters")]
        public string Comment { get; set; }
        public string Services { get; set; }
    }
}