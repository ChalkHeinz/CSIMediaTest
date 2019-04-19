using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSIMediaTest.Models;

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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            var sequence = Request.Form[0];
            var order = Request.Form[1];

            //Sort's runtime 
            var startTime = DateTime.Now;
            sequence = String.Concat(sequence.OrderBy(c => c));
            var endTme = DateTime.Now;

            double seconds = (endTme - startTime).TotalSeconds;

            Sequence sequenceObject = new Sequence {
                NewSequence = sequence,
                Direction = order,
                TimeTaken = seconds
            };

            dBContext.Sequences.Add(sequenceObject);
            dBContext.SaveChanges();

            return RedirectToAction("SequenceList");

        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult SequenceList()
        {
            return View();
        }

    }
}