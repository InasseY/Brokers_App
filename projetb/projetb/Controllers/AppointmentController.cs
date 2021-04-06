using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projetb.Models.data;
using System.Data.Entity;
using System.Data;


namespace projetb.Controllers
{
    public class AppointmentController : Controller
    {
        agendaEntities dbj = new agendaEntities();

        [HttpGet]
        public ActionResult addAppointment() //afficher le formulaire de prise de rendez-vous
        {
            ViewBag.idBroker = new SelectList(dbj.Brokers,"idBroker","lastname");
            ViewBag.idCustomer = new SelectList(dbj.Customers, "idCustomer", "lastname");
            return View();
        }

        [HttpPost]
        public ActionResult addAppointment(Appointments appointment)//ajouter de nouveaux RDV
        {

            if (ModelState.IsValid)
            {
                dbj.Appointments.Add(appointment);
                dbj.SaveChanges();
                TempData["SuccessMessage"] = "C'est dans la boite !";
                return RedirectToAction("appointmentsList");
            }
            ViewBag.idBroker = new SelectList(dbj.Brokers, "idBroker", "lastname");
            ViewBag.idCustomer = new SelectList(dbj.Customers, "idCustomer", "lastname");
            return View(appointment);
        }

        public ActionResult appointmentsList()//afficher la liste des RDV
        {
            var appointment = dbj.Appointments.Include(model => model.Brokers);

            return View(appointment.ToList());
        }
        [HttpPost]
        public ActionResult Delete(int id) //supprimer un RDV
        {
            var apps = dbj.Appointments.Where(x => x.idBroker == id).ToList();
            foreach (var app in apps)
            {
                dbj.Appointments.Remove(app);
                dbj.SaveChanges();
            }
            var appointment = dbj.Appointments.Where(model => model.idAppointment == id).First();
            dbj.Appointments.Remove(appointment);
            dbj.SaveChanges();
            var list = dbj.Appointments.ToList();
            return View("appointments", list);
        }
    }

}