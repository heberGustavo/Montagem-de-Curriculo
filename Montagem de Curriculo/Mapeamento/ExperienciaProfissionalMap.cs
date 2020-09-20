using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Montagem_de_Curriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Mapeamento
{
    public class ExperienciaProfissionalMap : IEntityTypeConfiguration<ExperienciaProfissional>
    {
        public void Configure(EntityTypeBuilder<ExperienciaProfissional> builder)
        {
            builder.HasKey(e => e.ExperienciaProfissionalId);

            builder.Property(e => e.NomeEmpresa).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Cargo).IsRequired().HasMaxLength(50);
            builder.Property(e => e.AnoInicio).IsRequired();
            builder.Property(e => e.AnoFim).IsRequired();
            builder.Property(e => e.DescricaoAtividades).IsRequired().HasMaxLength(500);

            builder.HasOne(e => e.Curriculo).WithMany(e => e.ExperienciaProfissionals).HasForeignKey(e => e.CurriculoId);

            builder.ToTable("ExperienciasProfissionais");
        }
    }
}
