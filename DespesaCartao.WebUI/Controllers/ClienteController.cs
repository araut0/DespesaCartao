using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepository repository;

        public ClienteController(IClienteRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View(repository.Clientes);
        }

        public ActionResult Create()
        {
            return View("Edit", new Cliente());
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                repository.SalvarCliente(cliente);
                TempData["message"] = string.Format("O cliente {0} foi salvo com sucesso.", cliente.Nome);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", cliente);
            }
        }

        [HttpPost]
        public ActionResult Delete(int clienteId)
        {
            Cliente clienteExcluido = repository.DeletarCliente(clienteId);
            if (clienteExcluido != null)
            {
                TempData["message"] = string.Format("O cliente {0} foi excluido com sucesso", clienteExcluido.Nome);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int clienteId)
        {
            Cliente cliente = repository.Clientes.FirstOrDefault(c => c.ClienteID == clienteId);
            return View(cliente);
        }

        [ChildActionOnly]
        public ActionResult MontarClientes(int idClienteSelecionado = 0)
        {
            ViewBag.IDClienteSelecionado = idClienteSelecionado;
            return PartialView(repository.Clientes);
        }
    }
}