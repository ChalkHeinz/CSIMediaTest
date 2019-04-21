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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            var sequence = Request.Form[0];
            var order = Request.Form[1];
            Stopwatch stopwWatch = new Stopwatch();
            double seconds = 0;

            //Prep sequence data
            string[] temp = sequence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (order == "ascending")
            {
                //Sort's runtime 
                stopwWatch.Start();
                Array.Sort(temp);
                stopwWatch.Stop();
            }
            else if (order == "descending")
            {
                //Sort's runtime 
                stopwWatch.Start();
                Array.Sort(temp);
                stopwWatch.Stop();
                temp.Reverse();
                
            }

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

    }
}