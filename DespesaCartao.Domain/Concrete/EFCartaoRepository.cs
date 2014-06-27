using System.Collections.Generic;
using System.Linq;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;
using System;

namespace DespesaCartao.Domain.Concrete
{
    public class EFCartaoRepository : ICartaoRepository
    {
        private EFDespesaCartaoContext context = new EFDespesaCartaoContext();

        public IQueryable<Cartao> Cartoes
        {
            get { return context.Cartoes; }
        }

        public Cartao SalvarCartao(Cartao cartao)
        {
            if (cartao.CartaoID == 0)
            {
                context.Cartoes.Add(cartao);
            }
            else
            {
                Cartao dbEntry = context.Cartoes.Find(cartao.CartaoID);
                if (dbEntry!=null)
                {
                    dbEntry.Bandeira = cartao.Bandeira;
                    dbEntry.Fornecedor = cartao.Fornecedor;
                    dbEntry.DiaVencimento = cartao.DiaVencimento;
                }
            }
            context.SaveChanges();
            return cartao;
        }

        public Cartao DeletarCartao(int cartaoId)
        {
            Cartao cartao = context.Cartoes.Find(cartaoId);
            if (cartao!=null)
            {
                context.Cartoes.Remove(cartao);
                context.SaveChanges();
            }
            return cartao;
        }

        public int BuscarDiaVencimento(int cartaoId)
        {
            Cartao cartao = Cartoes.FirstOrDefault(c => c.CartaoID == cartaoId);
            if (cartao != null)
                return cartao.DiaVencimento;

            throw new ArgumentException("cartão não encontrado");
        }
    }
}
