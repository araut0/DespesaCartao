using System.Data.Entity;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Concrete
{
    public class EFDespesaCartaoContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Segmento> Segmentos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Parcela> Parcelas { get; set; }
    }
}
