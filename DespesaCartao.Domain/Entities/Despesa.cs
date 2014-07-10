using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System;

namespace DespesaCartao.Domain.Entities
{
    public class Despesa
    {
        [HiddenInput(DisplayValue=false)]
        public int DespesaID { get; set; }
        [Required(ErrorMessage="O campo data de compra é obrigatório.")]
        public DateTime DataCompra { get; set; }
        public DateTime DataCriacao { get; set; }
        [Required(ErrorMessage="O campo descrição do produto é obrigatório.")]
        public string DescricaoProduto { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Por favor entre com um valor positivo")]
        [Required(ErrorMessage = "O campo valor do produto é obrigatório.")]
        [DataType(DataType.Currency)]
        public decimal ValorTotal { get; set; }
        [Required(ErrorMessage="O campo quantidade de parcelas é obrigatório")]
        public int QtdParcelas { get; set; }
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int ClienteID { get; set; }
        public virtual Cliente Comprador { get; set; }
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int LojaID { get; set; }
        public virtual Loja Loja{ get; set; }
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int CartaoID { get; set; }
        public virtual Cartao Cartao { get; set; }
    }
}
