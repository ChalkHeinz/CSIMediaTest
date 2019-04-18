using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CSIMediaTest.Models
{
    public class Sequence
    {
        public int ID { get; set; }
        public int NewSequence { get; set; }
        public string Direction { get; set; }
        public DateTime TimeTaken { get; set; }
    }

    public class SequenceDBContext : DbContext
    {
        public DbSet<Sequence> Sequences { get; set; }
    }
}