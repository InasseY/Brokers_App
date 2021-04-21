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
                return RedirectToAction("Index","Home");
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
            Appointments appointments = dbj.Appointments.Find(id);
            dbj.Appointments.Remove(appointments);
            dbj.SaveChanges();
            
            return View("Index");
        }
    }

}