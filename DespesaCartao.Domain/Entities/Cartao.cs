using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DespesaCartao.Domain.Entities
{
    public class Cartao
    {
        [HiddenInput(DisplayValue=false)]
        public int CartaoID { get; set; }
        [Required(ErrorMessage="O campo Fornecedor é obrigatório")]
        [StringLength(100)]
        public string Fornecedor { get; set; }
        [Required(ErrorMessage = "O campo Bandeira é obrigatório")]
        [StringLength(50)]
        public string Bandeira { get; set; }
        [Required(ErrorMessage = "O campo Vencimento é obrigatório")]
        [Range(1, 28, ErrorMessage="O dia do vencimento deve ser entre 1 e 28.")]
        [Display(Name="Dia de Vencimento")]
        public int DiaVencimento { get; set; }
    }
}
