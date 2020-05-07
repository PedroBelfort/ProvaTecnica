using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProvaTecnica.Models;
using ProvaTecnica.Models.Contexto;

namespace ProvaTecnica.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly Contexto _context;

        public UsuariosController(Contexto context)
        {
            _context = context;
        }

        // GET: Usuarios
        // Método para a apresentação da lista de Usuários criados
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Usuarios.Include(u => u.Perfil);
            return View(await contexto.ToListAsync());
        }

        // GET: Usuarios/Details/5
        // Método para exibir os detalhes de um determinado Usuário
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        // Método para a exibição dos campos para o cadastro de um Usuário
        public IActionResult Create()
        {
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Nome");
            return View();
        }

        // POST: Usuarios/Create
        // Método para a submissão do cadastro de um determinado Usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Senha,Email,PerfilId")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Nome", usuario.PerfilId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        // Método para acionar a edição de um determinado Usuário
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Nome", usuario.PerfilId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Método para submeter a edição de um determinado Usuário 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Senha,Email,PerfilId")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Id", usuario.PerfilId);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        // Método para acionar a exclusão de um determinado Usuário
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        // Método para a submissão da exclusão de um determinado Usuário
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Método interno para verificação da existência de um determinado Usuário
        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
