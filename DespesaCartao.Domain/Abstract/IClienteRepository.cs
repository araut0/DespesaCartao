using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DespesaCartao.Domain.Entities;

namespace DespesaCartao.Domain.Abstract
{
    public interface IClienteRepository
    {
        IQueryable<Cliente> Clientes { get; }
        void SalvarCliente(Cliente cliente);
        Cliente DeletarCliente(int clienteId);
    }
}
