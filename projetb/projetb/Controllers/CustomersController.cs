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
    public class CustomersController : Controller
    {
        agendaEntities dbj = new agendaEntities();

        [HttpGet]
        public ActionResult addcustomers() //afficher le formulaire d'ajout client
        {
            return View();
        }

        [HttpPost]
        public ActionResult addcustomers(Customers customer)//ajouter de nouveaux clients
        {

            if (ModelState.IsValid)
            {
                dbj.Customers.Add(customer);
                dbj.SaveChanges();
                TempData["SuccessMessage"] = "C'est dans la boite !";
                return RedirectToAction("CustomersList");
            }
            return View(customer);
        }
    }
}