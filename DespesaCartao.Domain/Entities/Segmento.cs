using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DespesaCartao.Domain.Entities
{
    public class Segmento
    {
        [HiddenInput(DisplayValue=false)]
        public int SegmentoID { get; set; }
        [Required(ErrorMessage="O nome do segmento é obrigatório.")]
        [StringLength(100, MinimumLength=3, ErrorMessage="O nome do segmento deve conter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage="O campo descrição deve conter no máximo 300 caracteres.")]
        public string Descricao { get; set; }
    }
}
