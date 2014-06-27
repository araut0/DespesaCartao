using System;
using System.Collections.Generic;
using System.Linq;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Abstract
{
    public interface ILojaRepository
    {
        IQueryable<Loja> Lojas { get; }
        void SalvarLoja(Loja loja);
        Loja DeletarLoja(int lojaId);
    }
}
