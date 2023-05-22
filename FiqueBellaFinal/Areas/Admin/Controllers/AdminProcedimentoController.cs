using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Context;
using FiqueBellaFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace FiqueBellaFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize("Admin")]
    public class AdminProcedimentoController : Controller
    {
        private readonly AppDbContext _context;

        public AdminProcedimentoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminProcedimento
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Procedimentos.Include(p => p.Categoria);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AdminProcedimento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Procedimentos == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProcedimentoId == id);
            if (procedimento == null)
            {
                return NotFound();
            }

            return View(procedimento);
        }

        // GET: Admin/AdminProcedimento/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");
            return View();
        }

        // POST: Admin/AdminProcedimento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProcedimentoId,Nome,Descricao,Preco,QntdSessoes,Duracao,IsProcedimentoPreferido,EmEstoque,ImagemUrl,ImagemThumbnailUrl,CategoriaId")] Procedimento procedimento)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(procedimento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", procedimento.CategoriaId);
            return View(procedimento);
        }

        // GET: Admin/AdminProcedimento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Procedimentos == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos.FindAsync(id);
            if (procedimento == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", procedimento.CategoriaId);
            return View(procedimento);
        }

        // POST: Admin/AdminProcedimento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProcedimentoId,Nome,Descricao,Preco,QntdSessoes,Duracao,IsProcedimentoPreferido,EmEstoque,ImagemUrl,ImagemThumbnailUrl,CategoriaId")] Procedimento procedimento)
        {
            if (id != procedimento.ProcedimentoId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimentoExists(procedimento.ProcedimentoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", procedimento.CategoriaId);
            return View(procedimento);
        }

        // GET: Admin/AdminProcedimento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Procedimentos == null)
            {
                return NotFound();
            }

            var procedimento = await _context.Procedimentos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProcedimentoId == id);
            if (procedimento == null)
            {
                return NotFound();
            }

            return View(procedimento);
        }

        // POST: Admin/AdminProcedimento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Procedimentos == null)
            {
                return Problem("Entity set 'AppDbContext.Procedimentos'  is null.");
            }
            var procedimento = await _context.Procedimentos.FindAsync(id);
            if (procedimento != null)
            {
                _context.Procedimentos.Remove(procedimento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimentoExists(int id)
        {
          return (_context.Procedimentos?.Any(e => e.ProcedimentoId == id)).GetValueOrDefault();
        }
    }
}
