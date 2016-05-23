using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PassGeneralsModels.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage="Enter valid first name with correct capitalisation")]
        [Display(Name = "First name")]
        public string CustomerFirstname { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Enter valid last name with correct capitalisation")]
        [Display(Name = "Last name")]
        public string CustomerLastname { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Collection Address")]
        [DataType(DataType.MultilineText)]
        public string CustomerAddress { get; set; }

        //[Required]
        [Display(Name = "Contact number")]
        [RegularExpression(@"\d{11}", ErrorMessage = "Contact number must be 11 digits")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        //[Required]
        public string ApplicationUserId { get;  }
        public virtual ApplicationUser User { get;  }
        //[ForeignKey("ApplicationUser")]
        //public virtual ApplicationUser ApplicationUser { get; set; }


        public virtual ICollection<Lesson> lessons {get; set;}
    }
}