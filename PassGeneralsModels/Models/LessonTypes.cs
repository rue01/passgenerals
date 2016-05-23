using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassGeneralsModels.Models
{
    public class LessonTypes
    {
        [Key]
        public int Id { get; set; }

        public string LessonType { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson lesson { get; set; }
    }
}