using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespesaCartao.Domain.Abstract;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Concrete
{
    public class EFClienteRepository : IClienteRepository
    {
        private EFDespesaCartaoContext context = new EFDespesaCartaoContext();

        public IQueryable<Cliente> Clientes { get { return context.Clientes;  } }

        public void SalvarCliente(Cliente cliente)
        {
            if (cliente.ClienteID == 0)
            {
                context.Clientes.Add(cliente);
            }
            else
            {
                Cliente dbEntry = context.Clientes.Find(cliente.ClienteID);
                if (dbEntry != null)
                {
                    dbEntry.Nome = cliente.Nome;
                    dbEntry.Descricao = cliente.Descricao;
                }
            }
            context.SaveChanges();
        }

        public Cliente DeletarCliente(int clienteId)
        {
            var clienteADeletar = context.Clientes.Find(clienteId);
            if (clienteADeletar != null)
            {
                context.Clientes.Remove(clienteADeletar);
                context.SaveChanges();
            }
            return clienteADeletar;
        }
    }
}
