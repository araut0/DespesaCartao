using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DespesaCartao.Domain.Entities;
using DespesaCartao.Domain.Abstract;

namespace DespesaCartao.Controllers
{
    public class DespesaController : Controller
    {
        private IDespesaRepository repository;

        public DespesaController(IDespesaRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Despesas);
        }

        public ActionResult Create()
        {
            return View("Edit", new Despesa());
        }

        [HttpPost]
        public ActionResult Edit(Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                repository.SalvarDespesa(despesa);
                TempData["message"] = string.Format("A despesa para o produto {0} foi salva com sucesso", despesa.DescricaoProduto);
                return RedirectToAction("Index");
            }
            else
            {
                return View(despesa);
            }
        }
    }
}
