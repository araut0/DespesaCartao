using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Controllers;
using System.Web.Mvc;
using DespesaCartao.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DespesaCartao.Tests.Controllers
{
    [TestClass]
    public class ClienteControllerTest
    {
        [TestMethod]
        public void IndexGet_DeveRetornarTodosClientes()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            var cliente1 = GetCliente("joao", 1);
            var cliente2 = GetCliente("maria", 2);
            mock.Setup(m => m.Clientes).Returns(new Cliente[] { cliente1, cliente2 }.AsQueryable());
            ClienteController controller = new ClienteController(mock.Object);
            var result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Cliente[] clientes = ((IEnumerable<Cliente>)(((ViewResult)result).Model)).ToArray();
            Assert.AreEqual("joao", clientes[0].Nome);
            Assert.AreEqual("maria", clientes[1].Nome);
            Assert.AreEqual(2, clientes.Length);
        }

        [TestMethod]
        public void CreateGet_DeveRetornarViewEdit()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            ClienteController controller = new ClienteController(mock.Object);
            ActionResult result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Edit", ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void EditGet_ComIdValidoRetornaCliente()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            var cliente = GetCliente("joao", 1);
            mock.Setup(m => m.Clientes).Returns(new Cliente[] { cliente }.AsQueryable());
            ClienteController controller = new ClienteController(mock.Object);
            ActionResult result = controller.Edit(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("joao", ((Cliente)((ViewResult)result).ViewData.Model).Nome);
        }

        [TestMethod]
        public void EditPost_ComDadosValidosDeveSalvarERetornarAction()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            mock.Setup(m => m.SalvarCliente(It.IsAny<Cliente>()));
            var controller = new ClienteController(mock.Object);
            ActionResult result = controller.Edit(GetCliente("Joao", 1));
            mock.Verify(m => m.SalvarCliente(It.IsAny<Cliente>()));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EditPost_ComDadosInvalidosNaoDeveSalvarERetornarView()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            var controller = new ClienteController(mock.Object);
            controller.ModelState.AddModelError("", "erro");
            ActionResult result = controller.Edit(new Cliente());
            mock.Verify(m => m.SalvarCliente(It.IsAny<Cliente>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeletePost_ComIdValidoDeveExcluirERetornarAction()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            var cliente = GetCliente("joao", 1);
            mock.Setup(m => m.Clientes).Returns(new Cliente[] { cliente }.AsQueryable());
            mock.Setup(m => m.DeletarCliente(1)).Returns(cliente);
            var controller = new ClienteController(mock.Object);
            ActionResult result = controller.Delete(1);
            mock.Verify(m => m.DeletarCliente(1));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult)); 
        }

        [TestMethod]
        public void MontarClientesGet_DeveRetornarTodosClientesEPartialViewComViewBagDoSelecionado()
        {
            Mock<IClienteRepository> mock = new Mock<IClienteRepository>();
            var cliente1 = GetCliente("joao", 1);
            var cliente2 = GetCliente("maria", 2);
            mock.Setup(m => m.Clientes).Returns(new Cliente[] { cliente1, cliente2 }.AsQueryable());
            var controller = new ClienteController(mock.Object);
            ActionResult result = controller.MontarClientes(2);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            Assert.AreEqual(2, ((PartialViewResult)result).ViewBag.IDClienteSelecionado);
            Cliente[] clientes = ((IEnumerable<Cliente>)(((PartialViewResult)result).Model)).ToArray();
            Assert.AreEqual("joao", clientes[0].Nome);
            Assert.AreEqual("maria", clientes[1].Nome);
            Assert.AreEqual(2, clientes.Length);
        }

        private Cliente GetCliente(string nome, int? id = null, string descricao = null)
        {
            return new Cliente
            {
                ClienteID = id.HasValue ? id.Value : 1,
                Nome = nome,
                Descricao = descricao
            };
        }
    }
}
