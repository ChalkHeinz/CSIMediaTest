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

        public ActionResult Index(Sequence form)
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

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", form);
            }

            //Try prep sequence data, return index if data are not ints
            temp = sequence.Split(' ').Select(n => Convert.ToInt64(n)).ToArray();

            //Sort's runtime 
            stopwWatch.Start();
            Array.Sort(temp);
            stopwWatch.Stop();

            //Reverse if user selects descending
            if (order == Directions.Descending)
                Array.Reverse(temp);

            seconds = stopwWatch.Elapsed.TotalMilliseconds;
            //Join long array to create new sequence
            sequence = String.Join(" ", temp);

            //Create new sequence object
            Sequence sequenceObject = new Sequence
            {
                NewSequence = sequence,
                Direction = order,
                TimeTaken = seconds
            };

            //Add to database
            dBContext.Sequences.Add(sequenceObject);
            dBContext.SaveChanges();

            return RedirectToAction("SequenceList", sequenceObject);

        }

        public ActionResult SequenceList(Sequence sequenceObject, string order)
        {
            SequenceResultViewModel sequenceResultViewModel = new SequenceResultViewModel();
            var temp = dBContext.Sequences.ToList();

            //Sort sequences in ascending order for display
            temp = temp.OrderBy(seq => seq.TimeTaken).ToList();

            //Adding data to view model
            sequenceResultViewModel.Sequences = temp;
            sequenceResultViewModel.NewSequence = sequenceObject;

            return View(sequenceResultViewModel);
        }

        public void Export()
        {
            //Code based from http://techfunda.com/howto/310/export-data-into-xml-from-mvc
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