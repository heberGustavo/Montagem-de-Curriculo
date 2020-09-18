using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Montagem_de_Curriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    { 
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.UsuarioId);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique(); //Não permite repetir email

            builder.Property(u => u.Senha).IsRequired().HasMaxLength(50);

            //Collections
            builder.HasMany(u => u.Curriculos).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.InformacoesLogin).WithOne(u => u.Usuario).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Usuarios");
        }
    }
}
