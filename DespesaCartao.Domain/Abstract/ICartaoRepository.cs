using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Abstract
{
    public interface ICartaoRepository
    {
        IQueryable<Cartao> Cartoes { get; }
        Cartao SalvarCartao(Cartao cartao);
        Cartao DeletarCartao(int cartaoId);
        int BuscarDiaVencimento(int cartaoId);
    }
}
