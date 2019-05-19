using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CSIMediaTest.DataContext;
using CSIMediaTest.Models;
using CSIMediaTest.ViewModels;

namespace CSIMediaTest.Controllers
{
    public class HomeController : Controller
    {
        private SequenceDBContext _dBContext;

        public HomeController(SequenceDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public HomeController()
        {
            _dBContext = new SequenceDBContext();
        }

        [HttpPost]
        public ActionResult Index(Sequence form)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Create", "Sequence", form);
            }

            return View(form);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SequenceList(Sequence sequenceObject)
        {
            return View(new SequenceResultViewModel
            {
                Sequences = _dBContext.Sequences.OrderBy(seq => seq.TimeTaken).ToList(),
                NewSequence = sequenceObject
            });
        }

    }
}