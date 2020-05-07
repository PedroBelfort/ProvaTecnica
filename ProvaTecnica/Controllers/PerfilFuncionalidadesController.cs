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
    public class PerfilFuncionalidadesController : Controller
    {
        private readonly Contexto _context;

        public PerfilFuncionalidadesController(Contexto context)
        {
            _context = context;
        }

        // GET: PerfilFuncionalidades
        // Método para a apresentação da lista de PerfilFuncionalidades criadas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.PerfilFuncionalidade.Include(p => p.Funcionalidade).Include(p => p.Perfil);
            return View(await contexto.ToListAsync());
        }

        // GET: PerfilFuncionalidades/Details/5
        // Método para exibir os detalhes de um determinado PerfilFuncionalidade
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilFuncionalidade = await _context.PerfilFuncionalidade
                .Include(p => p.Funcionalidade)
                .Include(p => p.Perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilFuncionalidade == null)
            {
                return NotFound();
            }

            return View(perfilFuncionalidade);
        }

        // GET: PerfilFuncionalidades/Create
        // Método para a exibição dos campos para o cadastro de um PerfilFuncionalidade
        public IActionResult Create()
        {
            ViewData["FuncionalidadeId"] = new SelectList(_context.Funcionalidades, "Id", "Nome");
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Nome");
            return View();
        }

        // POST: PerfilFuncionalidades/Create
        // Método para a submissão do cadastro de um determinado PerfilFuncionalidade
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PerfilId,FuncionalidadeId")] PerfilFuncionalidade perfilFuncionalidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfilFuncionalidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FuncionalidadeId"] = new SelectList(_context.Funcionalidades, "Id", "Id", perfilFuncionalidade.FuncionalidadeId);
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Id", perfilFuncionalidade.PerfilId);
            return View(perfilFuncionalidade);
        }

        // GET: PerfilFuncionalidades/Edit/5
        // Método para acionar a edição de um determinado PerfilFuncionalidade
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilFuncionalidade = await _context.PerfilFuncionalidade.FindAsync(id);
            if (perfilFuncionalidade == null)
            {
                return NotFound();
            }
            ViewData["FuncionalidadeId"] = new SelectList(_context.Funcionalidades, "Id", "Nome", perfilFuncionalidade.FuncionalidadeId);
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Nome", perfilFuncionalidade.PerfilId);
            return View(perfilFuncionalidade);
        }

        // POST: PerfilFuncionalidades/Edit/5
        // Método para submeter a edição de um determinado PerfilFuncionalidade 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PerfilId,FuncionalidadeId")] PerfilFuncionalidade perfilFuncionalidade)
        {
            if (id != perfilFuncionalidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfilFuncionalidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfilFuncionalidadeExists(perfilFuncionalidade.Id))
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
            ViewData["FuncionalidadeId"] = new SelectList(_context.Funcionalidades, "Id", "Id", perfilFuncionalidade.FuncionalidadeId);
            ViewData["PerfilId"] = new SelectList(_context.Perfis, "Id", "Id", perfilFuncionalidade.PerfilId);
            return View(perfilFuncionalidade);
        }

        // GET: PerfilFuncionalidades/Delete/5
        // Método para aclioar a exclusão de um determinado PerfilFuncionalidade
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var perfilFuncionalidade = await _context.PerfilFuncionalidade
                .Include(p => p.Funcionalidade)
                .Include(p => p.Perfil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (perfilFuncionalidade == null)
            {
                return NotFound();
            }

            return View(perfilFuncionalidade);
        }

        // POST: PerfilFuncionalidades/Delete/5
        // Método para a submissão da exclusão de um determinado PerfilFuncionalidade
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var perfilFuncionalidade = await _context.PerfilFuncionalidade.FindAsync(id);
            _context.PerfilFuncionalidade.Remove(perfilFuncionalidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Método interno para verificação da existencia de um determinado PerfilFuncionalidade
        private bool PerfilFuncionalidadeExists(int id)
        {
            return _context.PerfilFuncionalidade.Any(e => e.Id == id);
        }
    }
}
