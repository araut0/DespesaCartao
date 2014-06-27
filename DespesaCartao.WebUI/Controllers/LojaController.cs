using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Controllers
{
    public class LojaController : Controller
    {
        private ILojaRepository repository;

        public LojaController(ILojaRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Lojas);
        }

        public ActionResult Create()
        {
            return View("Edit", new Loja());
        }

        [HttpPost]
        public ActionResult Edit(Loja loja)
        {
            if (ModelState.IsValid)
            {
                repository.SalvarLoja(loja);
                TempData["message"] = string.Format("A loja {0} foi salva com sucesso", loja.Nome);
                return RedirectToAction("Index");
            }
            else
            {
                return View(loja);
            }
        }

        [HttpPost]
        public ActionResult Delete(int lojaId)
        {
            Loja lojaExcluida = repository.DeletarLoja(lojaId);
            if (lojaExcluida != null)
            {
                TempData["message"] = string.Format("A loja {0} foi removida com sucesso.", lojaExcluida.Nome);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int lojaId)
        {
            Loja loja = repository.Lojas.FirstOrDefault(l => l.LojaID == lojaId);
            return View(loja);
        }

        [ChildActionOnly]
        public ActionResult MontarLojas(int idLojaSelecionado = 0)
        {
            @ViewBag.IDLojaSelecionado = idLojaSelecionado;
            return PartialView(repository.Lojas);
        }
    }
}