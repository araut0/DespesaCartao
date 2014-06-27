using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DespesaCartao.Domain.Entities
{
    public class Parcela
    {
        public int ParcelaID { get; set; }
        public int DespesaID { get; set; }
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public bool PagamentoEfetuado { get; set; }
    }
}
