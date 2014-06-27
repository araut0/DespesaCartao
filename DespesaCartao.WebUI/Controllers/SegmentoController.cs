using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Controllers
{
    public class SegmentoController : Controller
    {
        private ISegmentoRepository repository;

        public SegmentoController(ISegmentoRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Segmentos);
        }

        public ActionResult Create()
        {
            return View("Edit", new Segmento());
        }

        [HttpPost]
        public ActionResult Edit(Segmento segmento)
        {
            if (ModelState.IsValid)
            {
                repository.SalvarSegmento(segmento);
                TempData["message"] = string.Format("O segmento {0} foi salvo com sucesso.", segmento.Nome);
                return RedirectToAction("Index");
            }
            else
            {
                return View(segmento);
            }

        }

        public ActionResult Delete(int segmentoId)
        {
            Segmento segmento = repository.DeletarSegmento(segmentoId);
            if (segmento!=null)
            {
                TempData["message"] = string.Format("O segmento {0} foi removido com sucesso.", segmento.Nome);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int segmentoId)
        {
            Segmento segmento = repository.Segmentos.FirstOrDefault(s => s.SegmentoID == segmentoId);
            return View(segmento);
        }

        [ChildActionOnly]
        public ActionResult MontarSegmentos(int idSegmentoSelecionado = 0)
        {
            @ViewBag.IDSegmentoSelecionado = idSegmentoSelecionado;
            return PartialView(repository.Segmentos);
        }
    }
}