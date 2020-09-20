using Microsoft.EntityFrameworkCore;
using Montagem_de_Curriculo.Mapeamento;

namespace Montagem_de_Curriculo.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Curriculo> Curriculos { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<TipoCurso> TipoCursos { get; set; }
        public DbSet<FormacaoAcademica> FormacaoAcademicas { get; set; }
        public DbSet<Objetivo> Objetivos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ExperienciaProfissional> ExperienciasProfissionais { get; set; }
        public DbSet<InformacaoLogin> InformacoesLogin { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurriculoMap());
            modelBuilder.ApplyConfiguration(new IdiomaMap());
            modelBuilder.ApplyConfiguration(new TipoCursoMap());
            modelBuilder.ApplyConfiguration(new FormacaoAcademicaMap());
            modelBuilder.ApplyConfiguration(new ObjetivoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new InformacaoLoginMap());
            modelBuilder.ApplyConfiguration(new ExperienciaProfissionalMap());
        }
    }
}
