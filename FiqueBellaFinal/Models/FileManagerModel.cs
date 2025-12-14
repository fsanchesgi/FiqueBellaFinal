using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiqueBellaFinal.Models
{
    [Keyless] // Não é tabela
    public class FileManagerModel
    {
        [NotMapped] // ← EF NÃO tenta mapear
        public FileInfo[] Files { get; set; }

        [NotMapped] // ← ESSENCIAL
        public IFormFile IFormFile { get; set; }

        [NotMapped] // ← ESSENCIAL
        public List<IFormFile> IFormFiles { get; set; }

        // string simples → OK para EF
        public string PathImagesAtendimento { get; set; }
    }
}
