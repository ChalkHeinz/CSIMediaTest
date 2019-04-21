using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CSIMediaTest.Models;
using CSIMediaTest.ViewModels;

namespace CSIMediaTest.Controllers
{
    public class HomeController : Controller
    {
        private SequenceDBContext dBContext = new SequenceDBContext();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(Sequence form)
        {
            var sequence = form.NewSequence;
            Directions order = form.Direction;
            Stopwatch stopwWatch = new Stopwatch();
            double seconds = 0;
            long[] temp;

            //Try prep sequence data, return index if data are not ints
            try
            {
                temp = sequence.Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            }
            catch 
            {
                return RedirectToAction("Index");
            }
            //Sort's runtime 
            stopwWatch.Start();
            Array.Sort(temp);
            stopwWatch.Stop();

            if (order == Directions.Descending)
                Array.Reverse(temp);

            seconds = stopwWatch.Elapsed.TotalMilliseconds;
            //End of manipulation of sequence data
            sequence = String.Join(" ", temp);

            Sequence sequenceObject = new Sequence {
                NewSequence = sequence,
                Direction = order,
                TimeTaken = seconds
            };

            dBContext.Sequences.Add(sequenceObject);
            dBContext.SaveChanges();

            return RedirectToAction("SequenceList", sequenceObject);

        }

        
        public ActionResult SequenceList(Sequence sequenceObject)
        {
            SequenceResultViewModel sequenceResultViewModel = new SequenceResultViewModel();

            sequenceResultViewModel.Sequences = dBContext.Sequences.ToList();
            sequenceResultViewModel.NewSequence = sequenceObject;

            return View(sequenceResultViewModel);
        }

        public void Export()
        {
            var data = dBContext.Sequences.ToList();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=numberwang.xml");
            Response.ContentType = "text/xml";

            var serializer = new System.Xml.Serialization.XmlSerializer(data.GetType());
            serializer.Serialize(Response.OutputStream, data);

        }
    }
}