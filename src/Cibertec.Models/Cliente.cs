using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Cibertec.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Direccion { get; set; }
        [Computed]
        public ICollection<IFormFile> Imagen { get; set; }
        public string Foto { get; set; }
        public void SetFoto(string foto)
        {
            Foto = foto;
        }
        [Computed]
        public ICollection<IFormFile> Archivo { get; set; }
        public string Doc { get; set; }
        public void SetDoc(string doc)
        {
            Doc = doc;
        }
        [Computed]
        public int Total { get; set; }
    }
}