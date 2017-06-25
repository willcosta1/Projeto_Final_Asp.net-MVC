using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels
{
    public class Jogo
    {
        [Key]
        public int JogoID { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(25, ErrorMessage = "Tamanho máximo de caracteres é 25!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(255, ErrorMessage = "Tamanho máximo de caracteres é 255!")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public int CategoriaID { get; set; }
        [Display(Name = "Categoria")]
        public virtual Categoria _Categoria { get; set; }
        public bool Ativo { get; set; }
    }
}