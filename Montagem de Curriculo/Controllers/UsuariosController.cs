using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Montagem_de_Curriculo.Models;
using Montagem_de_Curriculo.ViewModels;

namespace Montagem_de_Curriculo.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto _context;

        public UsuariosController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("UsuarioId,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();

                //Armazena as Informações de Login
                InformacaoLogin informacao = new InformacaoLogin
                {
                    UsuarioId = usuario.UsuarioId,
                    EnderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Data = DateTime.Now.ToShortDateString(),
                    Horario = DateTime.Now.ToShortTimeString()
                };

                _context.Add(informacao);
                await _context.SaveChangesAsync();

                //Armazena o ID do usuario em uma sessao para usar mais tarde
                HttpContext.Session.SetInt32("UsuarioId", usuario.UsuarioId);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, usuario.Email)
                };

                //Cria conexão do Usuario
                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                //Loga o usuario
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Curriculos");
            }
            return View(usuario);
        }

        [HttpGet]
        public IActionResult Login()
        {
            /* Se o USUARIO estiver LOGAR corre o risco de adicionar algo errado na sessão dele,
             por isso é bom limpar todas as sessão ANTES de LOGAR */
            if (User.Identity.IsAuthenticated)
                HttpContext.Session.Clear();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                if (_context.Usuarios.Any(u => u.Email == login.Email && u.Senha == login.Senha))
                {
                    /*Para gravar nas Informações de Log
                     - Grava o ID do usuario cadastrado
                     */
                    var id = _context.Usuarios.Where(u => u.Email == login.Email && u.Senha == login.Senha)
                        .Select(u => u.UsuarioId).Single();

                    InformacaoLogin informacao = new InformacaoLogin
                    {
                        UsuarioId = id,
                        EnderecoIP = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                        Data = DateTime.Now.ToShortDateString(),
                        Horario = DateTime.Now.ToShortTimeString()
                    };

                    _context.Add(informacao);
                    await _context.SaveChangesAsync();

                    //Realizar Login
                    HttpContext.Session.SetInt32("UsuarioId", id);

                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Email, login.Email)
                    };

                    //Cria conexão do Usuario
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Curriculos");
                }
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear(); //Limpa as sessões

            return RedirectToAction("Login", "Usuarios");
        }

        //Verifica se usuario existe
        public JsonResult UsuarioExiste(string email)
        {
            //Se não encontrar o email, deixa cadastrar
            if (!_context.Usuarios.Any(u => u.Email == email))
                return Json(true);
            else
                return Json("Email existente");
        }
    }
}
