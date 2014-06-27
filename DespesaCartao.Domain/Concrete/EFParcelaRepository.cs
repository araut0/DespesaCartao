using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Concrete
{
    public class EFParcelaRepository : IParcelaRepository
    {
        private EFDespesaCartaoContext context = new EFDespesaCartaoContext();

        public IQueryable<Parcela> Parcelas
        {
            get { return context.Parcelas; }
        }

        public void SalvarParcela(Parcela parcela)
        {
            if (parcela.ParcelaID == 0)
            {
                context.Parcelas.Add(parcela);
            }
            else
            {
                var dbEntry = context.Parcelas.Find(parcela.ParcelaID);
                if (dbEntry != null)
                {
                    dbEntry.PagamentoEfetuado = parcela.PagamentoEfetuado;
                    dbEntry.Valor = parcela.Valor;
                    dbEntry.Vencimento = parcela.Vencimento;
                }
            }
            context.SaveChanges();
        }

        public Parcela DeletarParcela(int parcelaId)
        {
            throw new NotImplementedException();
        }
    }
}
