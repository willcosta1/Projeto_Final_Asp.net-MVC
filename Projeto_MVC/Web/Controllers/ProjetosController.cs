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
    public class ProjetosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            var projetos = db.Projetos.Include(p => p._Desenvolvedora).Include(p => p._Jogo);
            return View(projetos.ToList());
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }
        
        public ActionResult Create()
        {
            ViewBag.DesenvolvedoraID = new SelectList(db.Desenvolvedoras, "DesenvolvedoraID", "Nome");
            ViewBag.JogoID = new SelectList(db.Jogos, "JogoID", "Nome");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjetoID,Nome,DesenvolvedoraID,JogoID,Lancamento,Ativo")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((db.Projetos.FirstOrDefault(x => x.Nome == projeto.Nome)) == null)
                    {
                        projeto.Ativo = true;
                        db.Projetos.Add(projeto);
                        db.SaveChanges();
                        TempData["Mensagem"] = "Criado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Projeto já existente!";
                        return View(projeto);
                    }
                }
                catch
                {
                    TempData["Mensagem"] = "Ocorreu um erro!";
                    return View(projeto);
                }

            }

            ViewBag.DesenvolvedoraID = new SelectList(db.Desenvolvedoras, "DesenvolvedoraID", "Nome", projeto.DesenvolvedoraID);
            ViewBag.JogoID = new SelectList(db.Jogos, "JogoID", "Nome", projeto.JogoID);
            return View(projeto);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesenvolvedoraID = new SelectList(db.Desenvolvedoras, "DesenvolvedoraID", "Nome", projeto.DesenvolvedoraID);
            ViewBag.JogoID = new SelectList(db.Jogos, "JogoID", "Nome", projeto.JogoID);
            return View(projeto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjetoID,Nome,DesenvolvedoraID,JogoID,Lancamento,Ativo")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(projeto).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Mensagem"] = "Projeto modificado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["Mensagem"] = "Ocorreu um erro!";
                    return View(projeto);
                }
            }
            ViewBag.DesenvolvedoraID = new SelectList(db.Desenvolvedoras, "DesenvolvedoraID", "Nome", projeto.DesenvolvedoraID);
            ViewBag.JogoID = new SelectList(db.Jogos, "JogoID", "Nome", projeto.JogoID);
            return View(projeto);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projetos.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Projeto projeto = db.Projetos.Find(id);
                projeto.Ativo = false;
                db.Entry(projeto).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Mensagem"] = "Projeto excluido com sucesso!";
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
