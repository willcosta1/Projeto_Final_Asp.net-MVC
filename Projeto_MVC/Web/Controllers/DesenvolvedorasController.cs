using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BaseModels;
using Web.Models;

namespace Web.Controllers
{
    public class DesenvolvedorasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View(db.Desenvolvedoras.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desenvolvedora desenvolvedora = db.Desenvolvedoras.Find(id);
            if (desenvolvedora == null)
            {
                return HttpNotFound();
            }
            return View(desenvolvedora);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DesenvolvedoraID,Nome,Pais,Ativo")] Desenvolvedora desenvolvedora)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((db.Desenvolvedoras.FirstOrDefault(x => x.Nome == desenvolvedora.Nome)) == null)
                    {
                        desenvolvedora.Ativo = true;
                        db.Desenvolvedoras.Add(desenvolvedora);
                        db.SaveChanges();
                        TempData["Mensagem"] = "Criada com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Desenvolvedora já existente!";
                        return View(desenvolvedora);
                    }
                }
                catch
                {
                    TempData["Mensagem"] = "Ocorreu um erro!";
                    return View(desenvolvedora);
                }

            }

            return View(desenvolvedora);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desenvolvedora desenvolvedora = db.Desenvolvedoras.Find(id);
            if (desenvolvedora == null)
            {
                return HttpNotFound();
            }
            return View(desenvolvedora);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesenvolvedoraID,Nome,Pais,Ativo")] Desenvolvedora desenvolvedora)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(desenvolvedora).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Mensagem"] = "Desenvolvedora modificada com sucesso!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["Mensagem"] = "Ocorreu um erro!";
                    return View(desenvolvedora);
                }
            }
            return View(desenvolvedora);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desenvolvedora desenvolvedora = db.Desenvolvedoras.Find(id);
            if (desenvolvedora == null)
            {
                return HttpNotFound();
            }
            return View(desenvolvedora);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Desenvolvedora desenvolvedora = db.Desenvolvedoras.Find(id);
                desenvolvedora.Ativo = false;
                db.Entry(desenvolvedora).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Mensagem"] = "Desenvolvedora excluida com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["Mensagem"] = "Ocorreu um erro!";
                return View();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
