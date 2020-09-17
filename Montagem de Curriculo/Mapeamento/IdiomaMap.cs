using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Montagem_de_Curriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Mapeamento
{
    public class IdiomaMap : IEntityTypeConfiguration<Idioma>
    {
        public void Configure(EntityTypeBuilder<Idioma> builder)
        {
            builder.HasKey(i => i.IdiomaId);

            builder.Property(i => i.Nome).IsRequired().HasMaxLength(50);
            builder.HasIndex(i => i.Nome).IsUnique(); //Unico

            builder.Property(i => i.Nivel).IsRequired().HasMaxLength(50);

            //Collections
            builder.HasOne(i => i.Curriculo).WithMany(i => i.Idiomas).HasForeignKey(i => i.CurriculoId);

            builder.ToTable("Idiomas");
        }
    }
}
