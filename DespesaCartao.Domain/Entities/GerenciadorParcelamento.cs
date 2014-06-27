using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Concrete;

namespace DespesaCartao.Domain.Entities
{
    public class GerenciadorParcelamento : IGerenciadorParcelamento
    {
        private IParcelaRepository parcelaRepository;
        private ICartaoRepository cartaoRepository;

        public GerenciadorParcelamento(IParcelaRepository repo, ICartaoRepository repo2)
        {
            parcelaRepository = repo;
            cartaoRepository = repo2;
        }

        List<Parcela> Parcelas = new List<Parcela>();

        public void CriarParcelamento(Despesa despesa)
        {
            decimal valor = Decimal.Divide(despesa.ValorTotal, despesa.QtdParcelas);
            int diaVencimento = cartaoRepository.BuscarDiaVencimento(despesa.CartaoID);
            DateTime vencimentoParcela = CalcularVencimento(despesa.DataCompra, diaVencimento);
            for (int i = 1; i <= despesa.QtdParcelas; i++)
            {
                Parcela parcela = new Parcela();
                parcela.DespesaID = despesa.DespesaID;
                parcela.PagamentoEfetuado = false;
                parcela.Valor = valor;
                if (i > 1)
                    vencimentoParcela = vencimentoParcela.AddMonths(1);
                parcela.Vencimento = vencimentoParcela;
                parcelaRepository.SalvarParcela(parcela);
            }
        }

        public DateTime CalcularVencimento(DateTime dataCompra, int diaVencimentoCartao)
        {
            DateTime dataVencimentoCartao = new DateTime(dataCompra.Year, dataCompra.Month, diaVencimentoCartao);
            DateTime dataCorteCartao = dataVencimentoCartao.AddDays(-10);
            if (dataCompra < dataCorteCartao)
                return dataVencimentoCartao.Date;
            else
                return dataVencimentoCartao.AddMonths(1).Date;
        }
    }
}
