using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Models
{
    public class Objetivo
    {
        public int ObjetivoId { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(1000, ErrorMessage = "Descrição muito longa!")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        //FK
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }
    }
}
