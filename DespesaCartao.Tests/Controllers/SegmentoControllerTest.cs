using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;
using DespesaCartao.Controllers;
using System.Web.Mvc;

namespace DespesaCartao.Tests.Controllers
{
    [TestClass]
    public class SegmentoControllerTest
    {
        [TestMethod]
        public void IndexGet_DeveRetornarTodosSegmentos()
        {
            Mock<ISegmentoRepository> mock = new Mock<ISegmentoRepository>();
            Segmento segmento1 = GetSegmento("S1", 1);
            Segmento segmento2 = GetSegmento("S2", 2);
            mock.Setup(m => m.Segmentos).Returns(new Segmento[] { segmento1, segmento2 }.AsQueryable());
            var controller = new SegmentoController(mock.Object);
            ActionResult result = controller.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Segmento[] segmentos = ((IEnumerable<Segmento>)((ViewResult)result).Model).ToArray();
            Assert.AreEqual("S1", segmentos[0].Nome);
            Assert.AreEqual("S2", segmentos[1].Nome);
            Assert.AreEqual(2, segmentos.Length);
        }

        [TestMethod]
        public void CreateGet_DeveRetornarViewEdit()
        {
            Mock<ISegmentoRepository> mock = new Mock<ISegmentoRepository>();
            var controller = new SegmentoController(mock.Object);
            ActionResult result = controller.Create();
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Edit", ((ViewResult)result).ViewName);
        }

        [TestMethod]
        public void EditGet_DeveRetornarView()
        {
            Mock<ISegmentoRepository> mock = new Mock<ISegmentoRepository>();
            Segmento segmento = GetSegmento("S1", 1);
            mock.Setup(m => m.Segmentos).Returns(new Segmento[] { segmento }.AsQueryable());
            var controller = new SegmentoController(mock.Object);
            ActionResult result = controller.Edit(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("S1", ((Segmento)((ViewResult)result).Model).Nome);
        }

        [TestMethod]
        public void EditPost_ComDadosValidosDeveSalvarERetornarAction()
        {
            Mock<ISegmentoRepository> mock = new Mock<ISegmentoRepository>();
            Segmento segmento1 = GetSegmento("S1", 1);
            mock.Setup(m => m.SalvarSegmento(segmento1));
            var controller = new SegmentoController(mock.Object);
            ActionResult result = controller.Edit(segmento1);
            mock.Verify(m => m.SalvarSegmento(segmento1));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EditPost_ComDadosInvalidosNaoDeveSalvarERetornarView()
        {
            Mock<ISegmentoRepository> mock = new Mock<ISegmentoRepository>();
            mock.Setup(m => m.SalvarSegmento(It.IsAny<Segmento>()));
            var controller = new SegmentoController(mock.Object);
            controller.ModelState.AddModelError("", "erro");
            ActionResult result = controller.Edit(GetSegmento("S1", 1));
            mock.Verify(m => m.SalvarSegmento(It.IsAny<Segmento>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeletePost_ComIdValidoDeveDeletarERetornarAction()
        {
            Mock<ISegmentoRepository> mock = new Mock<ISegmentoRepository>();
            Segmento segmento1 = GetSegmento("S1", 1);
            mock.Setup(m => m.DeletarSegmento(1)).Returns(segmento1);
            var controller = new SegmentoController(mock.Object);
            ActionResult result = controller.Delete(1);
            mock.Verify(m => m.DeletarSegmento(1));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void MontarSegmentosGet_DeveRetornarPartialViewComTodosSegmentos()
        {
            Mock<ISegmentoRepository> mock = new Mock<ISegmentoRepository>();
            Segmento segmento1 = GetSegmento("S1", 1);
            Segmento segmento2 = GetSegmento("S2", 2);
            mock.Setup(m => m.Segmentos).Returns(new Segmento[] { segmento1, segmento2 }.AsQueryable());
            var controller = new SegmentoController(mock.Object);
            ActionResult result = controller.MontarSegmentos();
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
            Segmento[] segmentos = ((IEnumerable<Segmento>)((PartialViewResult)result).Model).ToArray();
            Assert.AreEqual("S1", segmentos[0].Nome);
            Assert.AreEqual("S2", segmentos[1].Nome);
            Assert.AreEqual(2, segmentos.Length);
            ActionResult result2 = controller.MontarSegmentos(1);
            Assert.AreEqual(1, ((PartialViewResult)result2).ViewBag.IDSegmentoSelecionado);
        }

        private Segmento GetSegmento(string nome, int? id = null, string descricao = null)
        {
            return new Segmento
            {
                SegmentoID = id.HasValue ? (int)id : 1,
                Nome = nome,
                Descricao = descricao
            };
        }
    }
}
