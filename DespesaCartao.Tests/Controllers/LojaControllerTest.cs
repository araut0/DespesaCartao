using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DespesaCartao.Domain.Entities;
using DespesaCartao.Domain.Abstract;
using Moq;
using DespesaCartao.Controllers;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace DespesaCartao.Tests.Controllers
{
    [TestClass]
    public class LojaControllerTest
    {
        [TestMethod]
        public void IndexGet_DeveRetornarTodasLojas()
        {
            Mock<ILojaRepository> mock = new Mock<ILojaRepository>();
            Loja loja1 = GetLoja("L1", 1);
            Loja loja2 = GetLoja("L2", 2);
            mock.Setup(m => m.Lojas).Returns(new Loja[] { loja1, loja2 }.AsQueryable());
            var controller = new LojaController(mock.Object);
            ActionResult result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Loja[] lojas = ((IEnumerable<Loja>)((ViewResult)result).ViewData.Model).ToArray();
            Assert.AreEqual("L1", lojas[0].Nome);
            Assert.AreEqual("L2", lojas[1].Nome);
            Assert.AreEqual(2, lojas.Length);
        }

        [TestMethod]
        public void CreateGet_DeveRetornarViewEdit()
        {
            Mock<ILojaRepository> mock = new Mock<ILojaRepository>();
            var controller = new LojaController(mock.Object);
            ActionResult result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Edit", ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void EditGet_DeveRetornarView()
        {
            Mock<ILojaRepository> mock = new Mock<ILojaRepository>();
            Loja loja = GetLoja("L1", 1);
            mock.Setup(m => m.Lojas).Returns(new Loja[] { loja }.AsQueryable());
            var controller = new LojaController(mock.Object);
            ActionResult result = controller.Edit(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("L1", ((Loja)((ViewResult)result).ViewData.Model).Nome);
        }

        [TestMethod]
        public void EditPost_ComLojaValidaDeveSalvarERetornarAction()
        {
            Mock<ILojaRepository> mock = new Mock<ILojaRepository>();
            Loja loja = GetLoja("L1", 1);
            mock.Setup(m => m.SalvarLoja(loja));
            var controller = new LojaController(mock.Object);
            ActionResult result = controller.Edit(loja);
            mock.Verify(m => m.SalvarLoja(loja));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EditPost_ComLojaInvalidaNaoDeveSalvarERetornarView()
        {
            Mock<ILojaRepository> mock = new Mock<ILojaRepository>();
            mock.Setup(m => m.SalvarLoja(It.IsAny<Loja>()));
            var controller = new LojaController(mock.Object);
            controller.ModelState.AddModelError("", "erro");
            ActionResult result = controller.Edit(new Loja());
            mock.Verify(m => m.SalvarLoja(It.IsAny<Loja>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeletePost_ComIdValidoDeveExcluirERetornarAction()
        {
            Mock<ILojaRepository> mock = new Mock<ILojaRepository>();
            Loja loja = GetLoja("L1", 1);
            mock.Setup(m => m.DeletarLoja(1)).Returns(loja);
            var controller = new LojaController(mock.Object);
            ActionResult result = controller.Delete(1);
            mock.Verify(m => m.DeletarLoja(1));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        private Loja GetLoja(string nome, int? id = null)
        {
            return new Loja { 
                LojaID = id.HasValue ? (int)id : 1,
                Nome = nome
            };
        }
    }
}
