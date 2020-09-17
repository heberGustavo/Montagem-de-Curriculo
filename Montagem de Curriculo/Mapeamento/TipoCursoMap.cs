using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Montagem_de_Curriculo.Models;

namespace Montagem_de_Curriculo.Mapeamento
{
    public class TipoCursoMap : IEntityTypeConfiguration<TipoCurso>
    {
        public void Configure(EntityTypeBuilder<TipoCurso> builder)
        {
            builder.HasKey(tc => tc.TipoCursoId);

            builder.Property(tc => tc.Tipo).IsRequired();
            builder.HasIndex(tc => tc.Tipo).IsUnique(); //Não pode ter curso igual

            /* 
              Tipo de curso pode estar relacionado a varias formações academicas mas
              Formação academica pode estar relacionado a somente um tipo de curso
            */
            builder.HasMany(tc => tc.FormacaoAcademicas).WithOne(tc => tc.TipoCurso)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
