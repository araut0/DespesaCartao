using System.Collections.Generic;
using System.Linq;
using DespesaCartao.Domain.Entities;
using DespesaCartao.Domain.Abstract;

namespace DespesaCartao.Domain.Concrete
{
    public class EFLojaRepository : ILojaRepository
    {
        private EFDespesaCartaoContext context = new EFDespesaCartaoContext();

        public IQueryable<Loja> Lojas
        {
            get { return context.Lojas; }
        }

        public void SalvarLoja(Loja loja)
        {
            if (loja.LojaID == 0)
            {
                context.Lojas.Add(loja);
            }
            else
            {
                Loja dbEntry = context.Lojas.Find(loja.LojaID);
                if (dbEntry != null)
                {
                    dbEntry.Nome = loja.Nome;
                    dbEntry.SegmentoID = loja.SegmentoID;
                }
            }
            context.SaveChanges();
        }

        public Loja DeletarLoja(int lojaId)
        {
            Loja lojaADeletar = context.Lojas.Find(lojaId);
            if (lojaADeletar!=null)
            {
                context.Lojas.Remove(lojaADeletar);
                context.SaveChanges();
            }
            return lojaADeletar;
        }
    }
}
