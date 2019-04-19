using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CSIMediaTest.Models
{
    public class Sequence
    {
        //Look into data annotations 
        public int ID { get; set; }
        public string NewSequence { get; set; }
        public string Direction { get; set; }
        public Double TimeTaken { get; set; }
    }

    public class SequenceDBContext : DbContext
    {
        public DbSet<Sequence> Sequences { get; set; }
    }
}