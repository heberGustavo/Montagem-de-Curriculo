using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Montagem_de_Curriculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Montagem_de_Curriculo.ViewComponents
{
    public class ExperienciasProfissionaisViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public ExperienciasProfissionaisViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return View(await _contexto.ExperienciasProfissionais.Where(e => e.CurriculoId == id).ToListAsync());
        }
    }
}
