using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;
using System;
using System.Linq;

namespace DespesaCartao.Domain.Concrete
{
    public class EFDespesaRepository : IDespesaRepository
    {
        private EFDespesaCartaoContext context = new EFDespesaCartaoContext();

        private IGerenciadorParcelamento gerenciadorParcela;

        public EFDespesaRepository(IGerenciadorParcelamento gp)
        {
            gerenciadorParcela = gp;
        }

        public IQueryable<Despesa> Despesas
        {
            get { return context.Despesas; }
        }

        public void SalvarDespesa(Despesa despesa)
        {
            if (despesa.DespesaID == 0)
            {
                despesa.DataCriacao = DateTime.Now;
                context.Despesas.Add(despesa);
            }
            else
            {
                var dbEntry = context.Despesas.Find(despesa.DespesaID);
                if (dbEntry != null)
                {
                    dbEntry.DataCompra = despesa.DataCompra;
                    dbEntry.CartaoID = despesa.CartaoID;
                    dbEntry.ClienteID = despesa.ClienteID;
                    dbEntry.DescricaoProduto = despesa.DescricaoProduto;
                    dbEntry.LojaID = despesa.LojaID;
                    dbEntry.QtdParcelas = despesa.QtdParcelas;
                    dbEntry.ValorTotal = despesa.ValorTotal;
                }
            }
            context.SaveChanges();
            gerenciadorParcela.CriarParcelamento(despesa);
            context.SaveChanges();
        }
    }
}
