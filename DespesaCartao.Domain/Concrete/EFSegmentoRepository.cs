using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Concrete
{
    public class EFSegmentoRepository : ISegmentoRepository
    {
        private EFDespesaCartaoContext context = new EFDespesaCartaoContext();

        public IQueryable<Segmento> Segmentos
        {
            get { return context.Segmentos; }
        }

        public void SalvarSegmento(Segmento segmento)
        {
            if (segmento.SegmentoID == 0)
            {
                context.Segmentos.Add(segmento);
            }
            else
            {
                Segmento dbEntry = context.Segmentos.Find(segmento.SegmentoID);
                if (dbEntry!=null)
                {
                    dbEntry.Nome = segmento.Nome;
                    dbEntry.Descricao = segmento.Descricao;
                }
            }
            context.SaveChanges();
        }

        public Segmento DeletarSegmento(int segmentoId)
        {
            Segmento segmento = context.Segmentos.Find(segmentoId);
            if (segmento!=null)
            {
                context.Segmentos.Remove(segmento);
                context.SaveChanges();
            }
            return segmento;
        }
    }
}
