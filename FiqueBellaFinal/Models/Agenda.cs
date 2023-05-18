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

        [Display(Name = "Horário do agendamento")]
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0: hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Horario { get; set; }

    }
}
