using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CSIMediaTest.Models
{
    public class Sequence
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "The Sequence Field Should Not Be Empty")]
        [RegularExpression(@"^(?!.*  )[\d\s]*$", ErrorMessage = "The Sequence Must Only Contain Numbers (0-9) and Spaces")]
        [Display(Name = "Sequence")]
        public string NewSequence { get; set; }

        [Required]
        public Directions Direction { get; set; }

        [Display(Name = "Time Taken")]
        public Double TimeTaken { get; set; }
    }

    public enum Directions
    {
        Ascending,
        Descending
    }
}