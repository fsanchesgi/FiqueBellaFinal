using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiqueBellaFinal.Models
{
    public class FileManagerModel
    {
        [NotMapped] // <- EF Core irá ignorar esta propriedade
        public FileInfo[] Files { get; set; }

        [NotMapped] // <- EF Core também ignora tipos não mapeáveis
        public IFormFile IFormFile { get; set; }

        [NotMapped]
        public List<IFormFile> IFormFiles { get; set; }

        public string PathImagesAtendimento { get; set; } // essa sim será mapeada no banco
    }
}
