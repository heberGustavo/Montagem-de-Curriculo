using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Models
{
    public class FormacaoAcademica
    {
        public int FormacaoAcademicaId { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        public string Instituicao { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [Range(1920, 2020, ErrorMessage = "Ano inválido!")]
        public int AnoInicio { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [Range(1920, 2020, ErrorMessage = "Ano inválido!")]
        public int AnoFim { get; set; }

        [Required(ErrorMessage = "Campo obrigátorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        public string NomeCurso { get; set; }

        //FK
        public int TipoCursoId { get; set; }
        public TipoCurso TipoCurso { get; set; }

        //FK
        public int CurriculoId { get; set; }
        public Curriculo Curriculo { get; set; }

    }
}
