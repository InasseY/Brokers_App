using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using projetb.Models.data;


namespace projetb.Controllers
{
    public class BrokersController : Controller
    {
        agendaEntities dbobj = new agendaEntities();
        // GET: Brokers
        public ActionResult Brokers(Brokers obj)
        {
            if (obj != null)
                return View(obj);
            else
                return View();
        }

        [HttpGet]
        public ActionResult addbroker()
        {
            return View();
        }
        [HttpPost]
        public ActionResult addbroker(Brokers brokers)
        {
            if (ModelState.IsValid)
            {
                dbobj.Brokers.Add(brokers);
                dbobj.SaveChanges();
                TempData["SuccessMessage"] = "C'est dans la boite !";
                return RedirectToAction("BrokersList");
            }
            return View(brokers);
        }

        public ActionResult BrokersList()
        {
            var brokers = dbobj.Brokers.ToList();
            return View(brokers);
        }
        public ActionResult delete(int id)
        {
            var brokers = dbobj.Brokers.Where(model => model.idBroker == id).First();
            dbobj.Brokers.Remove(brokers);
            dbobj.SaveChanges();

            var list = dbobj.Brokers.ToList();
            return View("BrokersList", list);
        }
    }
}
