using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CSIMediaTest.Models
{
    public enum Directions
    {
        Ascending,
        Descending
    }

    public class Sequence
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Sequence")]
        public string NewSequence { get; set; }

        [Required]
        public Directions Direction { get; set; }

        [Display(Name = "Time Taken")]
        public Double TimeTaken { get; set; }
    }

    public class SequenceDBContext : DbContext
    {
        public DbSet<Sequence> Sequences { get; set; }
    }
}