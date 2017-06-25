using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels
{
    public class Desenvolvedora
    {
        [Key]
        public int DesenvolvedoraID { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(25, ErrorMessage = "Tamanho máximo de caracteres é 25!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(25, ErrorMessage = "Tamanho máximo de caracteres é 25!")]
        [Display(Name ="País")]
        public string Pais { get; set; }
        public bool Ativo { get; set; }
    }
}