using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FiqueBellaFinal.Models
{
    public class Agenda
    {
        public int AgendaId { get; set; }

        [Display(Name = "Dia do agendamento")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Dia { get; set; }

        [StringLength(10, ErrorMessage = "O tamanho máximo é 10 caracteres.")]
        [Required(ErrorMessage = "Informe horário do agendamento.")]
        [Display(Name = "Horário")]
        public String Horario { get; set; }

        public int ProcedimentoId { get; set; }
        public virtual Procedimento Procedimento { get; set; }

        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
