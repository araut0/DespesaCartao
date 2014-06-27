using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DespesaCartao.Domain.Entities
{
    public class Loja
    {
        [HiddenInput(DisplayValue=false)]
        public int LojaID { get; set; }
        [Required(ErrorMessage = "O nome do segmento é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da loja deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        [Required]
        [HiddenInput(DisplayValue = false)]
        public int SegmentoID { get; set; }
        public virtual Segmento Segmento { get; set; }
    }
}
