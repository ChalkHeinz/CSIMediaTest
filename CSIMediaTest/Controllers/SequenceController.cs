using CSIMediaTest.DataContext;
using CSIMediaTest.Models;
using CSIMediaTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSIMediaTest.Controllers
{
    public class SequenceController : Controller
    {
        private SequenceDBContext _dBContext;

        public SequenceController(SequenceDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public SequenceController()
        {
            _dBContext = new SequenceDBContext();
        }

        public ActionResult Create(Sequence form)
        {
            var orderedSequence = OrderSequence(form.NewSequence, form.Direction);

            //Create new sequence object
            var sequenceObject = new Sequence
            {
                NewSequence = orderedSequence.Item2,
                Direction = form.Direction,
                TimeTaken = orderedSequence.Item1
            };

            //Add to database
            _dBContext.Sequences.Add(sequenceObject);
            _dBContext.SaveChanges();
            return RedirectToAction("SequenceList", "Home", sequenceObject);

        }

        public Tuple<Double, String> OrderSequence(string sequence, Directions direction)
        {
            var sequenceArray = sequence.Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var stopWatch = new Stopwatch();

            if (direction == Directions.Ascending)
            {
                stopWatch.Start();
                Array.Sort(sequenceArray);
                stopWatch.Stop();
            }
            else
            {
                stopWatch.Start();
                Array.Sort(sequenceArray);
                Array.Reverse(sequenceArray);
                stopWatch.Stop();
            }

            return Tuple.Create(stopWatch.Elapsed.TotalMilliseconds, String.Join(" ", sequenceArray));
        }

        public void Export()
        {
            //Code based from http://techfunda.com/howto/310/export-data-into-xml-from-mvc
            var data = _dBContext.Sequences.ToList();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=numberwang.xml");
            Response.ContentType = "text/xml";

            var serializer = new System.Xml.Serialization.XmlSerializer(data.GetType());
            serializer.Serialize(Response.OutputStream, data);

        }
    }
}