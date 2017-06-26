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
    public class JogosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var jogoes = db.Jogos.Include(j => j._Categoria);
            return View(jogoes.ToList());
        }


        public JsonResult ListarJogosPorDesenvolvedora(int id)
        {
            var projetos = db.Projetos.Where(p => p.DesenvolvedoraID == id).ToList();

            List<Jogo> jogos = new List<Jogo>();

            foreach (var projeto in projetos)
            {
                jogos.Add(projeto._Jogo);
            }

            return Json(new SelectList(jogos, "JogoID", "Nome"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }
        
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JogoID,Nome,Descricao,CategoriaID,Ativo")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((db.Jogos.FirstOrDefault(x => x.Nome == jogo.Nome)) == null)
                    {
                        jogo.Ativo = true;
                        db.Jogos.Add(jogo);
                        db.SaveChanges();
                        TempData["Mensagem"] = "Criado com sucesso!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Jogo já existente!";
                        return View(jogo);
                    }
                }
                catch
                {
                    TempData["Mensagem"] = "Ocorreu um erro!";
                    return View(jogo);
                }

            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", jogo.CategoriaID);
            return View(jogo);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", jogo.CategoriaID);
            return View(jogo);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JogoID,Nome,Descricao,CategoriaID,Ativo")] Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(jogo).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["Mensagem"] = "Jogo modificado com sucesso!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["Mensagem"] = "Ocorreu um erro!";
                    return View(jogo);
                }
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", jogo.CategoriaID);
            return View(jogo);
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Jogo jogo = db.Jogos.Find(id);
                jogo.Ativo = false;
                db.Entry(jogo).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Mensagem"] = "Jogo excluido com sucesso!";
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
