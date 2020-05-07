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
    public class FuncionalidadesController : Controller
    {
        private readonly Contexto _context;

        public FuncionalidadesController(Contexto context)
        {
            _context = context;
        }


        // GET: Funcionalidades
        //Médoto para listar as funcionalidades cadastradas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionalidades.ToListAsync());
        }

        // GET: Funcionalidades/Details/5 
        //Método para apresentar os atributos de uma determinada funcionalidade
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = await _context.Funcionalidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionalidade == null)
            {
                return NotFound();
            }

            return View(funcionalidade);
        }

        // GET: Funcionalidades/Create  
        // Método para apresentação do formulário de criação de uma funcionalidade
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionalidades/Create
        // Método para a submissão do formulários com os dados de funcionalidade
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Funcionalidade funcionalidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionalidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionalidade);
        }

        // GET: Funcionalidades/Edit/5
        // Método para acionar a  edição de uma determinada funcionalidade
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = await _context.Funcionalidades.FindAsync(id);
            if (funcionalidade == null)
            {
                return NotFound();
            }
            return View(funcionalidade);
        }

        // POST: Funcionalidades/Edit/5
        // Método para submeter o formulário da edição de uma funcionalidade
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Funcionalidade funcionalidade)
        {
            if (id != funcionalidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionalidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionalidadeExists(funcionalidade.Id))
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
            return View(funcionalidade);
        }

        // GET: Funcionalidades/Delete/5
        // Método para acionar a exclusão de uma funcionalidade
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = await _context.Funcionalidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionalidade == null)
            {
                return NotFound();
            }

            return View(funcionalidade);
        }

        // POST: Funcionalidades/Delete/5
        // Método para submeter a exclusão de uma determinada funcionalidade
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionalidade = await _context.Funcionalidades.FindAsync(id);
            _context.Funcionalidades.Remove(funcionalidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionalidadeExists(int id)
        {
            return _context.Funcionalidades.Any(e => e.Id == id);
        }
    }
}
