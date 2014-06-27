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
    public class CartaoControllerTest
    {
        [TestMethod]
        public void IndexGet_DeveRetornarTodosCartoesDeTodosClientes()
        {
            Mock<ICartaoRepository> mock = new Mock<ICartaoRepository>();
            Cartao cartao1 = GetCartao("C1");
            Cartao cartao2 = GetCartao("C2");
            mock.Setup(m => m.Cartoes).Returns(new Cartao[] { cartao1, cartao2 }.AsQueryable());
            var controller = new CartaoController(mock.Object);
            ActionResult result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Cartao[] cartoes = ((IEnumerable<Cartao>)((ViewResult)result).Model).ToArray();
            Assert.AreEqual(2, cartoes.Length);
            Assert.AreEqual("C1", cartoes[0].Bandeira);
            Assert.AreEqual("C2", cartoes[1].Bandeira);
        }

        [TestMethod]
        public void CreateGet_DeveRetornarViewEdit()
        {
            Mock<ICartaoRepository> mock = new Mock<ICartaoRepository>();
            var controller = new CartaoController(mock.Object);
            ActionResult result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Edit", ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void EditGet_ComIdValidoDeveRetornarCartaoEView()
        {
            Mock<ICartaoRepository> mock = new Mock<ICartaoRepository>();
            Cartao cartao = GetCartao("C1", null, 2);
            mock.Setup(m => m.Cartoes).Returns(new Cartao[] { cartao }.AsQueryable());
            var controller = new CartaoController(mock.Object);
            ActionResult result = controller.Edit(2);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("C1", ((Cartao)((ViewResult)result).Model).Bandeira);
        }

        [TestMethod]
        public void EditPost_ComDadosValidosDeveSalvarERetornarAction()
        {
            Mock<ICartaoRepository> mock = new Mock<ICartaoRepository>();
            Cartao cartao = GetCartao("C1", "F1", 2, 3);
            Cartao cartaoSalvo = GetCartao("C1", "F1", 2, 3, new Cliente() { Nome = "T1" });
            mock.Setup(m => m.SalvarCartao(cartao)).Returns(cartaoSalvo);
            var controller = new CartaoController(mock.Object);
            ActionResult result = controller.Edit(cartao);
            mock.Verify(m => m.SalvarCartao(cartao));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EditPost_ComDadosInvalidosNaoDeveSalvarERetornarView()
        {
            Mock<ICartaoRepository> mock = new Mock<ICartaoRepository>();
            Cartao cartao = GetCartao("C1", "F1", 2, 3);
            mock.Setup(m => m.SalvarCartao(cartao));
            var controller = new CartaoController(mock.Object);
            controller.ModelState.AddModelError("", "erro");
            ActionResult result = controller.Edit(cartao);
            mock.Verify(m => m.SalvarCartao(cartao), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeletePost_ComIdValidoDeveDeletarERetornarView()
        {
            Mock<ICartaoRepository> mock = new Mock<ICartaoRepository>();
            Cartao cartao = GetCartao("C1", "F1", 2, 3);
            Cartao cartaoExcluido = GetCartao("C1", "F1", 2, 3, new Cliente() { Nome = "T1" });
            mock.Setup(m => m.DeletarCartao(2)).Returns(cartaoExcluido);
            var controller = new CartaoController(mock.Object);
            ActionResult result = controller.Delete(2);
            mock.Verify(m => m.DeletarCartao(2));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        private Cartao GetCartao(string bandeira, string fornecedor = null, int? id = null, int? proprietarioId = null, Cliente proprietario = null, int? diaVencimento = null)
        {
            return new Cartao
            {
                CartaoID = id.HasValue ? (int)id : 1,
                Bandeira = bandeira,
                Fornecedor = fornecedor,
                DiaVencimento = diaVencimento.HasValue ? (int)diaVencimento : 1
            };
        }
    }
}
