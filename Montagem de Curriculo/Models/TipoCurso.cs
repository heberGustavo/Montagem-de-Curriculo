using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Models
{
    public class TipoCurso
    {
        public int TipoCursoId { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(50, ErrorMessage = "Informe menos caracteres!")]
        public string Tipo { get; set; }
        public ICollection<FormacaoAcademica> FormacaoAcademicas { get; set; } //Pode estar relacionado a varias Formações Academicas
    }
}
