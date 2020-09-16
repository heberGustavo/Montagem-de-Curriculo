using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Models
{
    public class TipoCurso
    {
        public int TipoCursoId { get; set; }
        public string Tipo { get; set; }
        public ICollection<FormacaoAcademica> FormacaoAcademicas { get; set; } //Pode estar relacionado a varias Formações Academicas
    }
}
