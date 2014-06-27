using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Abstract
{
    public interface IGerenciadorParcelamento
    {
        void CriarParcelamento(Despesa despesa);
    }
}
