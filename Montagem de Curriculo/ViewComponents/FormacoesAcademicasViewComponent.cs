using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Montagem_de_Curriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.ViewComponents
{
    public class FormacoesAcademicasViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public FormacoesAcademicasViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int curriculoId)
        {
            return View(await _contexto.FormacaoAcademicas.Include(f => f.TipoCurso).Where(f => f.CurriculoId == curriculoId).ToListAsync());
        }
    }
}