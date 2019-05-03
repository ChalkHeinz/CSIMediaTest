using CSIMediaTest.DataContext;
using CSIMediaTest.Models;
using CSIMediaTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSIMediaTest.Controllers
{
    public class SequenceListController : Controller
    {
        private SequenceDBContext _dBContext;

        public SequenceListController(SequenceDBContext dbContext)
        {
            _dBContext = dbContext;
        }

        public SequenceListController()
        {
            _dBContext = new SequenceDBContext();
        }

        public ActionResult SequenceList(Sequence sequenceObject)
        {
            var sequenceResultViewModel = new SequenceResultViewModel();

            //Adding data to view model
            sequenceResultViewModel.Sequences = _dBContext.Sequences.OrderBy(seq => seq.TimeTaken).ToList();
            sequenceResultViewModel.NewSequence = sequenceObject;

            return View(sequenceResultViewModel);
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