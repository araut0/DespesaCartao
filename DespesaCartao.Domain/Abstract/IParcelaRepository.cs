using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Abstract
{
    public interface IParcelaRepository
    {
        IQueryable<Parcela> Parcelas { get; }
        void SalvarParcela(Parcela parcela);
        Parcela DeletarParcela(int parcelaId);
    }
}
