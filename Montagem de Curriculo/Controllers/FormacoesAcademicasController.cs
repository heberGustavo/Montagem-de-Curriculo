using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Montagem_de_Curriculo.Models;

namespace Montagem_de_Curriculo.Controllers
{
    public class FormacoesAcademicasController : Controller
    {
        private readonly Contexto _context;

        public FormacoesAcademicasController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Create(int id)
        {
            FormacaoAcademica formacao = new FormacaoAcademica();
            formacao.CurriculoId = id;

            ViewData["TipoCursoId"] = new SelectList(_context.TipoCursos, "TipoCursoId", "Tipo");
            return View(formacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FormacaoAcademicaId,Instituicao,AnoInicio,AnoFim,NomeCurso,TipoCursoId,CurriculoId")] FormacaoAcademica formacaoAcademica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formacaoAcademica);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Curriculos", new { id = formacaoAcademica.CurriculoId});
            }

            ViewData["TipoCursoId"] = new SelectList(_context.TipoCursos, "TipoCursoId", "Tipo", formacaoAcademica.TipoCursoId);
            return View(formacaoAcademica);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formacaoAcademica = await _context.FormacaoAcademicas.FindAsync(id);
            if (formacaoAcademica == null)
            {
                return NotFound();
            }
            
            ViewData["TipoCursoId"] = new SelectList(_context.TipoCursos, "TipoCursoId", "Tipo", formacaoAcademica.TipoCursoId);
            return View(formacaoAcademica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FormacaoAcademicaId,Instituicao,AnoInicio,AnoFim,NomeCurso,TipoCursoId,CurriculoId")] FormacaoAcademica formacaoAcademica)
        {
            if (id != formacaoAcademica.FormacaoAcademicaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formacaoAcademica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormacaoAcademicaExists(formacaoAcademica.FormacaoAcademicaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Curriculos", new { id = formacaoAcademica.CurriculoId });
            }
            
            ViewData["TipoCursoId"] = new SelectList(_context.TipoCursos, "TipoCursoId", "Tipo", formacaoAcademica.TipoCursoId);
            return View(formacaoAcademica);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var formacaoAcademica = await _context.FormacaoAcademicas.FindAsync(id);
            _context.FormacaoAcademicas.Remove(formacaoAcademica);
            await _context.SaveChangesAsync();
            return Json("Formação Acadêmica excluída");
        }

        private bool FormacaoAcademicaExists(int id)
        {
            return _context.FormacaoAcademicas.Any(e => e.FormacaoAcademicaId == id);
        }
    }
}
