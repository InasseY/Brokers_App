using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using projetb.Models;
using System.Web.Mvc;
using projetb.Models.data; 

namespace projetb.Controllers
{
    public class HomeController : Controller
    {
        agendaEntities db = new agendaEntities(); 
        public ActionResult Index()
        {
            var Appointment = db.Appointments.ToList();
            return View (Appointment);
        }

    //   public ActionResult ListBrokers()
    //   {
    ////        return View(db.Brokers.ToList());
    ////    }


    //    public ActionResult About()
    //    {
    //        ViewBag.Message = "Your application description page.";

    //        return View();
    //    }

    //    public ActionResult Contact()
    //    {
    //        ViewBag.Message = "Your contact page.";

    //        return View();
    //    }
    }
}