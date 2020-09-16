using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Models
{
    public class Idioma
    {
        public int IdiomaId { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        public string Nivel { get; set; }

        //FK
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}
