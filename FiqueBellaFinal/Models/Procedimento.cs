using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace FiqueBellaFinal.Models
{
    public class Procedimento
    {
        public int ProcedimentoId { get; set; }

        [StringLength(40, ErrorMessage = "O tamanho máximo é 40 caracteres.")]
        [Required(ErrorMessage = "Informe o nome do procedimento.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres.")]
        [Required(ErrorMessage = "Informe a descrição do procedimento.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço do prato")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O preço deve estar entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de sessões.")]
        [Display(Name = "Quantidade de sessões")]
        public int QntdSessoes { get; set; }

        [StringLength(10, ErrorMessage = "O tamanho máximo é 10 caracteres.")]
        [Required(ErrorMessage = "Informe a duração do procedimento.")]
        [Display(Name = "Duração")]
        public string Duracao { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsProcedimentoPreferido { get; set; }

        [Display(Name = "Em promoção?")]
        public bool EmPromocao { get; set; }


        [StringLength(200, ErrorMessage = "O tamanho máximo é 200 caracteres.")]
        [Display(Name = "Caminho da imagem normal")]
        public string? ImagemUrl { get; set; }

        [StringLength(200, ErrorMessage = "O tamanho máximo é 200 caracteres.")]
        [Display(Name = "Caminho da imagem miniatura")]
        public string? ImagemThumbnailUrl { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
