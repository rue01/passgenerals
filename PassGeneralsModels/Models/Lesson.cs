using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassGeneralsModels.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        //[Required]
        //[StringLength(100, MinimumLength = 2)]
        [Display(Name = "Lesson package name")]
        public string LessonType { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of lesson")]
        public DateTime DateOfLesson { get; set; }

        [Display(Name = "Length of lesson (hr)")]
        [Range(1, 4)]
        public int DurationOfLesson { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }


        [Required]
        public int CustomerId { get; set; }
        public virtual Customer customer { get; set; }
    }
}