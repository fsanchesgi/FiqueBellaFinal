using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Data;
using FiqueBellaFinal.Models;
using Microsoft.AspNetCore.Authorization;

namespace FiqueBellaFinal.Controllers
{
    public class SugestaoController : Controller
    {
        private readonly AppDbContext _context;

        public SugestaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sugestaos
        public async Task<IActionResult> Index()
        {
              return _context.Sugestaos != null ? 
                          View(await _context.Sugestaos.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Sugestaos'  is null.");
        }

        // GET: Sugestaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sugestaos == null)
            {
                return NotFound();
            }

            var sugestao = await _context.Sugestaos
                .FirstOrDefaultAsync(m => m.SugestaoId == id);
            if (sugestao == null)
            {
                return NotFound();
            }

            return View(sugestao);
        }

        // GET: Sugestaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sugestaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SugestaoId,Nome,Texto")] Sugestao sugestao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sugestao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sugestao);
        }

        // GET: Sugestaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sugestaos == null)
            {
                return NotFound();
            }

            var sugestao = await _context.Sugestaos.FindAsync(id);
            if (sugestao == null)
            {
                return NotFound();
            }
            return View(sugestao);
        }

        // POST: Sugestaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SugestaoId,Nome,Texto")] Sugestao sugestao)
        {
            if (id != sugestao.SugestaoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sugestao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SugestaoExists(sugestao.SugestaoId))
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
            return View(sugestao);
        }

        [Authorize("Admin")]
        // GET: Sugestaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sugestaos == null)
            {
                return NotFound();
            }

            var sugestao = await _context.Sugestaos
                .FirstOrDefaultAsync(m => m.SugestaoId == id);
            if (sugestao == null)
            {
                return NotFound();
            }

            return View(sugestao);
        }

        // POST: Sugestaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sugestaos == null)
            {
                return Problem("Entity set 'AppDbContext.Sugestaos'  is null.");
            }
            var sugestao = await _context.Sugestaos.FindAsync(id);
            if (sugestao != null)
            {
                _context.Sugestaos.Remove(sugestao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SugestaoExists(int id)
        {
          return (_context.Sugestaos?.Any(e => e.SugestaoId == id)).GetValueOrDefault();
        }
    }
}
