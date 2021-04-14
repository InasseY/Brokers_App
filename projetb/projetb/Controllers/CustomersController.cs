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
        public ActionResult CustomersList()//afficher la liste des clients
        {
            var customer = dbj.Customers.ToList();
            return View(customer);
        }
       
        public ActionResult Delete(int id) //supprimer un client de la liste
        {
            var customer = dbj.Customers.Where(model => model.idCustomer == id).First();
            dbj.Customers.Remove(customer);
            dbj.SaveChanges();

            var list = dbj.Customers.ToList();
            return View("CustomersList", list);
        }
        public ActionResult profilC(int id) //afficher les informations clients
        {
            var custom = dbj.Customers.Where(s => s.idCustomer == id).First();
            return View(custom);
        }
        public ActionResult editC(int id) //afficher les informations du clients à modifier
        {
            var customer = dbj.Customers.Where(s => s.idCustomer == id).First();
            return View(customer);
        }
        [HttpPost]
        public ActionResult editC(Customers cust) //modifier et enregistrer les nouvelles données clients
        {
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(cust))
                {
                    dbj.Entry(cust).State = EntityState.Modified;
                    dbj.SaveChanges();
                    TempData["SuccessMessage"] = "Yeah c'est modifié !";
                    return RedirectToAction("CustomersList");
                }
            }
            return View(cust);
        }
    }
}