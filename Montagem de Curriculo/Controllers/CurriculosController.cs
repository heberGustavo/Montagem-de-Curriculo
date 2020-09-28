using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Montagem_de_Curriculo.Models;
using Montagem_de_Curriculo.ViewModels;
using Rotativa.AspNetCore;

namespace Montagem_de_Curriculo.Controllers
{
    public class CurriculosController : Controller
    {
        private readonly Contexto _context;

        public CurriculosController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            var contexto = _context.Curriculos.Include(c => c.Usuario).Where(c => c.UsuarioId == usuarioId);
            return View(await contexto.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CurriculoId == id);
            if (curriculo == null)
            {
                return NotFound();
            }

            return View(curriculo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            //Pega o ID do usuario e salva no Curriculo o ID como Chave Estrangeira
            curriculo.UsuarioId = int.Parse(HttpContext.Session.GetInt32("UsuarioId").ToString());

            if (ModelState.IsValid)
            {
                _context.Add(curriculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(curriculo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var curriculo = await _context.Curriculos.FindAsync(id);
            if (curriculo == null)
            {
                return NotFound();
            }
            return View(curriculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CurriculoId,Nome,UsuarioId")] Curriculo curriculo)
        {
            //Pega o ID do usuario e salva no Curriculo o ID como Chave Estrangeira
            curriculo.UsuarioId = int.Parse(HttpContext.Session.GetInt32("UsuarioId").ToString());

            if (id != curriculo.CurriculoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(curriculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurriculoExists(curriculo.CurriculoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(curriculo);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var curriculo = await _context.Curriculos.FindAsync(id);
            _context.Curriculos.Remove(curriculo);
            await _context.SaveChangesAsync();
            return Json(curriculo.Nome + " excluido com sucesso!");
        }

        private bool CurriculoExists(int id)
        {
            return _context.Curriculos.Any(e => e.CurriculoId == id);
        }

        public IActionResult VisualizarPDF(int id)
        {
            var idUsuario = HttpContext.Session.GetInt32("UsuarioId");

            CurriculoViewModel curriculo = new CurriculoViewModel();
            curriculo.Objetivos = _context.Objetivos.Where(o => o.Curriculo.UsuarioId == idUsuario).ToList();
            curriculo.FormacoesAcademicas = _context.FormacaoAcademicas.Include(f => f.TipoCurso).Where(f => f.Curriculo.UsuarioId == idUsuario).ToList();
            curriculo.ExperienciasProfissionais = _context.ExperienciasProfissionais.Where(e => e.Curriculo.UsuarioId == idUsuario).ToList();
            curriculo.Idiomas = _context.Idiomas.Where(i => i.Curriculo.UsuarioId == idUsuario).ToList();

            return new ViewAsPdf("PDF", curriculo) { FileName = "Curriculo_.pdf" };
        }
    }
}
