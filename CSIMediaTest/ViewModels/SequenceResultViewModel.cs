using CSIMediaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSIMediaTest.ViewModels
{
    public class SequenceResultViewModel
    {
        public IList<Sequence> Sequences{ get; set;}
        public Sequence NewSequence { get; set; }
    }
}