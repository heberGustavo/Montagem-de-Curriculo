using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Models
{
    public class Curriculo
    {
        public int CurriculoId { get; set; }
        public string Nome { get; set; }

        //FK
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        //Collections
        public ICollection<Objetivo> Objetivos { get; set; }
        public ICollection<FormacaoAcademica> FormacaoAcademicas { get; set; }
        public ICollection<ExperienciaProfissional> ExperienciaProfissionals { get; set; }
        public ICollection<Idioma> Idiomas { get; set; }


    }
}
