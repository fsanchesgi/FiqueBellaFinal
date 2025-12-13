using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Data;
using FiqueBellaFinal.Models;
using FiqueBellaFinal.Areas.Admin.Services;

namespace FiqueBellaFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminContabilidadeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RelatorioContabilidadeServices _relatorioServices;

        public AdminContabilidadeController(AppDbContext context, RelatorioContabilidadeServices relatorioServices)
        {
            _context = context;
            _relatorioServices = relatorioServices;
        }

        // GET: Admin/AdminContabilidade
        public async Task<IActionResult> Index()
        {
            //var appDbContext = _context.Contabilidades.Include(c => c.EntradaSaida).Include(c => c.Tipo);
            var appDbContext = _context.Contabilidades
                .Include(a => a.EntradaSaida)
                .Include(a => a.Tipo)
                .OrderBy(a => a.Data);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AdminContabilidade/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contabilidades == null)
            {
                return NotFound();
            }

            var contabilidade = await _context.Contabilidades
                .Include(c => c.EntradaSaida)
                .Include(c => c.Tipo)
                .FirstOrDefaultAsync(m => m.ContabilidadeId == id);
            if (contabilidade == null)
            {
                return NotFound();
            }

            return View(contabilidade);
        }

        // GET: Admin/AdminContabilidade/Create
        public IActionResult Create()
        {
            ViewData["EntradaSaidaId"] = new SelectList(_context.Set<EntradaSaida>(), "EntradaSaidaId", "Descricao");
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "TipoDesc");
            return View();
        }

        // POST: Admin/AdminContabilidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContabilidadeId,Descricao,Data,Valor,TipoId,EntradaSaidaId")] Contabilidade contabilidade)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(contabilidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntradaSaidaId"] = new SelectList(_context.Set<EntradaSaida>(), "EntradaSaidaId", "Descricao", contabilidade.EntradaSaidaId);
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "TipoDesc", contabilidade.TipoId);
            return View(contabilidade);
        }

        // GET: Admin/AdminContabilidade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contabilidades == null)
            {
                return NotFound();
            }

            var contabilidade = await _context.Contabilidades.FindAsync(id);
            if (contabilidade == null)
            {
                return NotFound();
            }
            ViewData["EntradaSaidaId"] = new SelectList(_context.Set<EntradaSaida>(), "EntradaSaidaId", "Descricao", contabilidade.EntradaSaidaId);
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "TipoDesc", contabilidade.TipoId);
            return View(contabilidade);
        }

        // POST: Admin/AdminContabilidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContabilidadeId,Descricao,Data,Valor,TipoId,EntradaSaidaId")] Contabilidade contabilidade)
        {
            if (id != contabilidade.ContabilidadeId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(contabilidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContabilidadeExists(contabilidade.ContabilidadeId))
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
            ViewData["EntradaSaidaId"] = new SelectList(_context.Set<EntradaSaida>(), "EntradaSaidaId", "Descricao", contabilidade.EntradaSaidaId);
            ViewData["TipoId"] = new SelectList(_context.Tipos, "TipoId", "TipoDesc", contabilidade.TipoId);
            return View(contabilidade);
        }

        // GET: Admin/AdminContabilidade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Contabilidades == null)
            {
                return NotFound();
            }

            var contabilidade = await _context.Contabilidades
                .Include(c => c.EntradaSaida)
                .Include(c => c.Tipo)
                .FirstOrDefaultAsync(m => m.ContabilidadeId == id);
            if (contabilidade == null)
            {
                return NotFound();
            }

            return View(contabilidade);
        }

        // POST: Admin/AdminContabilidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contabilidades == null)
            {
                return Problem("Entity set 'AppDbContext.Contabilidades'  is null.");
            }
            var contabilidade = await _context.Contabilidades.FindAsync(id);
            if (contabilidade != null)
            {
                _context.Contabilidades.Remove(contabilidade);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContabilidadeExists(int id)
        {
          return (_context.Contabilidades?.Any(e => e.ContabilidadeId == id)).GetValueOrDefault();
        }
        public async Task<IActionResult> Relatorio(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await _relatorioServices.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
    }
}
