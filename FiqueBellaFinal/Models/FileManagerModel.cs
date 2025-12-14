using Microsoft.EntityFrameworkCore;

namespace FiqueBellaFinal.Models
{
    [Keyless] // ✅ ADICIONADO
    public class FileManagerModel
    {
        public FileInfo[] Files { get; set; }
        public IFormFile IFormFile { get; set; }
        public List<IFormFile> IFormFiles { get; set; }
        public string PathImagesAtendimento { get; set; }
    }
}
