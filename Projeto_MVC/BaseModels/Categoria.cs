using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels
{
    public class Categoria
    {
        [Key]
        public int CategoriaID { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(25, ErrorMessage = "Tamanho máximo de caracteres é 25!")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}