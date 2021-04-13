using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using projetb.Models.data;
using System.Data.Entity;
using System.Data;

namespace projetb.Controllers
{
    public class BrokersController : Controller
    {
        agendaEntities dbobj = new agendaEntities();


        [HttpGet]
        public ActionResult addbroker() //afficher le formulaire
        {

            return View();
        }
        

        [HttpPost]
        public ActionResult addbroker(Brokers brokers)//ajouter de nouveaux courtiers
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

        public ActionResult BrokersList()//afficher la liste des courtiers
        {
            var brokers = dbobj.Brokers.ToList();
            return View(brokers);
        }
        public ActionResult Delete(int id) //supprimer un courtier de la liste
        {
            var brokers = dbobj.Brokers.Where(model => model.idBroker == id).First();
            dbobj.Brokers.Remove(brokers);
            dbobj.SaveChanges();

            var list = dbobj.Brokers.ToList();
            return View("BrokersList", list);
        }
        public ActionResult Profil(int id) //afficher les informations du courtier
        {
            var bk = dbobj.Brokers.Where(s => s.idBroker == id).First();
            return View(bk);
        }
        public ActionResult Edit(int id) //afficher les informations du courtier à modifier
        {
            var brk = dbobj.Brokers.Where(s => s.idBroker == id).First();
            return View(brk);
        }
        [HttpPost]
        public ActionResult Edit(Brokers brk) //modifier et enregistrer les nouvelles données courtiers
        {
            if (ModelState.IsValid)
            {
                if (TryUpdateModel(brk))
                {
                    dbobj.Entry(brk).State = EntityState.Modified;
                    dbobj.SaveChanges();
                    TempData["SuccessMessage"] = "Yeah c'est modifié !";
                    return RedirectToAction("BrokersList");
                }
            }
            return View(brk);
        }
    }
}
