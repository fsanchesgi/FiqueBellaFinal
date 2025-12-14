using Microsoft.EntityFrameworkCore;

namespace FiqueBellaFinal.Models
{
    [Keyless] // ✅ ADICIONADO
    public class ConfigurationImagens
    {
        public string NomePastaImagensAtendimentos { get; set; }
    }
}
