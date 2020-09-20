using Microsoft.EntityFrameworkCore.Migrations;

namespace Montagem_de_Curriculo.Migrations
{
    public partial class CriacaoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoCursos",
                columns: table => new
                {
                    TipoCursoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoCursos", x => x.TipoCursoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Senha = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Curriculos",
                columns: table => new
                {
                    CurriculoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculos", x => x.CurriculoId);
                    table.ForeignKey(
                        name: "FK_Curriculos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InformacoesLogin",
                columns: table => new
                {
                    InformacaoLoginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnderecoIP = table.Column<string>(nullable: false),
                    Data = table.Column<string>(nullable: false),
                    Horario = table.Column<string>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformacoesLogin", x => x.InformacaoLoginId);
                    table.ForeignKey(
                        name: "FK_InformacoesLogin_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienciasProfissionais",
                columns: table => new
                {
                    ExperienciaProfissionalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEmpresa = table.Column<string>(maxLength: 50, nullable: false),
                    Cargo = table.Column<string>(maxLength: 50, nullable: false),
                    AnoInicio = table.Column<int>(nullable: false),
                    AnoFim = table.Column<int>(nullable: false),
                    DescricaoAtividades = table.Column<string>(maxLength: 500, nullable: false),
                    CurriculoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienciasProfissionais", x => x.ExperienciaProfissionalId);
                    table.ForeignKey(
                        name: "FK_ExperienciasProfissionais_Curriculos_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormacoesAcademicas",
                columns: table => new
                {
                    FormacaoAcademicaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instituicao = table.Column<string>(maxLength: 50, nullable: false),
                    AnoInicio = table.Column<int>(nullable: false),
                    AnoFim = table.Column<int>(nullable: false),
                    NomeCurso = table.Column<string>(maxLength: 50, nullable: false),
                    TipoCursoId = table.Column<int>(nullable: false),
                    CurriculoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormacoesAcademicas", x => x.FormacaoAcademicaId);
                    table.ForeignKey(
                        name: "FK_FormacoesAcademicas_Curriculos_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormacoesAcademicas_TipoCursos_TipoCursoId",
                        column: x => x.TipoCursoId,
                        principalTable: "TipoCursos",
                        principalColumn: "TipoCursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Idiomas",
                columns: table => new
                {
                    IdiomaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 50, nullable: false),
                    Nivel = table.Column<string>(maxLength: 50, nullable: false),
                    CurriculoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Idiomas", x => x.IdiomaId);
                    table.ForeignKey(
                        name: "FK_Idiomas_Curriculos_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Objetivos",
                columns: table => new
                {
                    ObjetivoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(maxLength: 1000, nullable: false),
                    CurriculoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objetivos", x => x.ObjetivoId);
                    table.ForeignKey(
                        name: "FK_Objetivos_Curriculos_CurriculoId",
                        column: x => x.CurriculoId,
                        principalTable: "Curriculos",
                        principalColumn: "CurriculoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curriculos_Nome",
                table: "Curriculos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Curriculos_UsuarioId",
                table: "Curriculos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienciasProfissionais_CurriculoId",
                table: "ExperienciasProfissionais",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormacoesAcademicas_CurriculoId",
                table: "FormacoesAcademicas",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_FormacoesAcademicas_TipoCursoId",
                table: "FormacoesAcademicas",
                column: "TipoCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Idiomas_CurriculoId",
                table: "Idiomas",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_Idiomas_Nome",
                table: "Idiomas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InformacoesLogin_UsuarioId",
                table: "InformacoesLogin",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Objetivos_CurriculoId",
                table: "Objetivos",
                column: "CurriculoId");

            migrationBuilder.CreateIndex(
                name: "IX_TipoCursos_Tipo",
                table: "TipoCursos",
                column: "Tipo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienciasProfissionais");

            migrationBuilder.DropTable(
                name: "FormacoesAcademicas");

            migrationBuilder.DropTable(
                name: "Idiomas");

            migrationBuilder.DropTable(
                name: "InformacoesLogin");

            migrationBuilder.DropTable(
                name: "Objetivos");

            migrationBuilder.DropTable(
                name: "TipoCursos");

            migrationBuilder.DropTable(
                name: "Curriculos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
