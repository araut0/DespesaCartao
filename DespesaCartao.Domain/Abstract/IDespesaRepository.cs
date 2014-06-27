using DespesaCartao.Domain.Entities;
using System.Linq;

namespace DespesaCartao.Domain.Abstract
{
    public interface IDespesaRepository
    {
        IQueryable<Despesa> Despesas { get; }
        void SalvarDespesa(Despesa despesa);
    }
}
