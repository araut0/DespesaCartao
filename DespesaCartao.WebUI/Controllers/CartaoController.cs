using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;
using System.Web.Mvc;

namespace DespesaCartao.Controllers
{
    public class CartaoController : Controller
    {
        private ICartaoRepository repository;

        public CartaoController(ICartaoRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Cartoes);
        }

        public ActionResult Create()
        {
            return View("Edit", new Cartao());
        }

        public ActionResult Edit(int cartaoId)
        {
            Cartao cartao = repository.Cartoes.FirstOrDefault(c => c.CartaoID == cartaoId);
            return View(cartao);
        }

        [HttpPost]
        public ActionResult Edit(Cartao cartao)
        {
            if (ModelState.IsValid)
            {
                Cartao cartaoSalvo = repository.SalvarCartao(cartao);
                TempData["message"] = string.Format("O cartão {0} - {1} foi salvo com sucesso", cartaoSalvo.Bandeira, cartaoSalvo.Fornecedor);
                return RedirectToAction("Index");
            }
            else
            {
                return View(cartao);
            }
        }

        public ActionResult Delete(int cartaoId)
        {
            Cartao cartao = repository.DeletarCartao(cartaoId);
            if (cartao!=null)
            {
                TempData["message"] = string.Format("O cartão {0} - {1} foi removido com sucesso", cartao.Bandeira, cartao.Fornecedor);
            }
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult MontarCartoes(int idCartaoSelecionado = 0)
        {
            @ViewBag.IDCartaoSelecionado = idCartaoSelecionado;
            return PartialView(repository.Cartoes);
        }
    }
}
