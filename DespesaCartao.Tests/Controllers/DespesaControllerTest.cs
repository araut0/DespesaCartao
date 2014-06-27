using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Controllers;
using System.Web.Mvc;
using DespesaCartao.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using DespesaCartao.Domain.Concrete;

namespace DespesaCartao.Tests.Controllers
{
    [TestClass]
    public class DespesaControllerTest
    {
        [TestMethod]
        public void EditPost_ComDespesaValidaDeveSalvarERetornarAction()
        {
            Mock<IDespesaRepository> mock = new Mock<IDespesaRepository>();
            Despesa despesa = new Despesa();
            mock.Setup(m => m.SalvarDespesa(despesa));
            var controller = new DespesaController(mock.Object);
            ActionResult result = controller.Edit(despesa);
            mock.Verify(m => m.SalvarDespesa(despesa));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void EditPost_ComDespesaInvalidaNaoDeveSalvarERetornarView()
        {
            Mock<IDespesaRepository> mock = new Mock<IDespesaRepository>();
            Despesa despesa = new Despesa();
            mock.Setup(m => m.SalvarDespesa(It.IsAny<Despesa>()));
            var controller = new DespesaController(mock.Object);
            controller.ModelState.AddModelError("", "erro");
            ActionResult result = controller.Edit(despesa);
            mock.Verify(m => m.SalvarDespesa(It.IsAny<Despesa>()), Times.Never());
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeveCriarParcelamentoParaDespesaValida()
        {
            Despesa despesa = new Despesa();
            despesa.DespesaID = 1;
            despesa.CartaoID = 1;
            despesa.Cartao = new Cartao() { CartaoID = 1, DiaVencimento = 15 };
            despesa.DataCompra = DateTime.Parse("10/01/2014");
            despesa.ValorTotal = 1000M;
            despesa.QtdParcelas = 2;
            Mock<IParcelaRepository> mock = new Mock<IParcelaRepository>();
            Mock<ICartaoRepository> mock2 = new Mock<ICartaoRepository>();
            mock2.Setup(m => m.BuscarDiaVencimento(despesa.CartaoID)).Returns(despesa.Cartao.DiaVencimento);
            GerenciadorParcelamento parcelamento = new GerenciadorParcelamento(mock.Object, mock2.Object);
            parcelamento.CriarParcelamento(despesa);
            mock.Verify(m => m.SalvarParcela(It.IsAny<Parcela>()), Times.Exactly(despesa.QtdParcelas));
        }

        [TestMethod]
        public void CalcularVencimentoPrimeiraParcela()
        {
            Mock<IParcelaRepository> mock = new Mock<IParcelaRepository>();
            Mock<ICartaoRepository> mock2 = new Mock<ICartaoRepository>();
            GerenciadorParcelamento parcelamento = new GerenciadorParcelamento(mock.Object, mock2.Object);
            int diaVencimentoCartao = 15;

            DateTime dataCompra = DateTime.Parse("10/01/2014");
            DateTime result = parcelamento.CalcularVencimento(dataCompra, diaVencimentoCartao);
            Assert.AreEqual(DateTime.Parse("15/02/2014").Date, result.Date);
            
            dataCompra = DateTime.Parse("02/01/2014");
            result = parcelamento.CalcularVencimento(dataCompra, diaVencimentoCartao);
            Assert.AreEqual(DateTime.Parse("15/01/2014").Date, result.Date);
        }
    }
}
