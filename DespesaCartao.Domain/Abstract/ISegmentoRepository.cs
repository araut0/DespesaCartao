using System.Linq;
using System.Collections.Generic;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Abstract
{
    public interface ISegmentoRepository
    {
        IQueryable<Segmento> Segmentos { get; }
        void SalvarSegmento(Segmento segmento);
        Segmento DeletarSegmento(int segmentoId);
    }
}
