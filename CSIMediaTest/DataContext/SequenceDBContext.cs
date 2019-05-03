using CSIMediaTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CSIMediaTest.DataContext
{
    public class SequenceDBContext : DbContext
    {
        public virtual DbSet<Sequence> Sequences { get; set; }

        public SequenceDBContext() : base("MyConnectionStringName")
        {

        }
    }
}