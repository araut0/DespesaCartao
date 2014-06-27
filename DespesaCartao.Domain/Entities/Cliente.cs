using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DespesaCartao.Domain.Entities
{
    public class Cliente
    {
        [HiddenInput(DisplayValue = false)]
        public int ClienteID { get; set; }
        [Required(ErrorMessage="O campo nome é obrigatório")]
        [StringLength(100, MinimumLength=3, ErrorMessage="O nome deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage = "O campo descrição deve conter no máximo 300 caracteres.")]
        public string Descricao { get; set; }
    }
}
