﻿using System;
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

        public ActionResult Index(Sequence form)
        {
            //Object was passed to invoke form validation
            return View();
        }

        public ActionResult Create(Sequence form)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", form);
            }

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
            return RedirectToAction("SequenceList", "SequenceList", sequenceObject);

        }

        public Tuple<Double, String> OrderSequence(string sequence, Directions direction)
        {
            var sequenceArray = sequence.Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var stopWatch = new Stopwatch();

            if(direction == Directions.Ascending)
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

       
    }
}