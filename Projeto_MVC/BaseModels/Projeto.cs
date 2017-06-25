using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels
{
    public class Projeto
    {
        [Key]
        public int ProjetoID { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(25, ErrorMessage = "Tamanho máximo de caracteres é 25!")]
        public string Nome { get; set; }
        public int DesenvolvedoraID { get; set; }
        [Display(Name = "Desenvolvedora")]
        public virtual Desenvolvedora _Desenvolvedora { get; set; }
        public int JogoID { get; set; }
        [Display(Name = "Jogo")]
        public virtual Jogo _Jogo { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Data de lançamento")]
        public string Lancamento { get; set; }
    }
}