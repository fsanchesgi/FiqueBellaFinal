using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Context;
using FiqueBellaFinal.Models;

namespace FiqueBellaFinal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminAgendaController : Controller
    {
        private readonly AppDbContext _context;

        public AdminAgendaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminAgenda
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Agendas.Include(a => a.Cliente).Include(a => a.Procedimento);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/AdminAgenda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agendas == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .Include(a => a.Cliente)
                .Include(a => a.Procedimento)
                .FirstOrDefaultAsync(m => m.AgendaId == id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // GET: Admin/AdminAgenda/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome");
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "ProcedimentoId", "Nome");
            return View();
        }

        // POST: Admin/AdminAgenda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgendaId,Dia,Horario,ProcedimentoId,ClienteId")] Agenda agenda)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(agenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", agenda.ClienteId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "ProcedimentoId", "Nome", agenda.ProcedimentoId);
            return View(agenda);
        }

        // GET: Admin/AdminAgenda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agendas == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", agenda.ClienteId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "ProcedimentoId", "Nome", agenda.ProcedimentoId);
            return View(agenda);
        }

        // POST: Admin/AdminAgenda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgendaId,Dia,Horario,ProcedimentoId,ClienteId")] Agenda agenda)
        {
            if (id != agenda.AgendaId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(agenda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendaExists(agenda.AgendaId))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", agenda.ClienteId);
            ViewData["ProcedimentoId"] = new SelectList(_context.Procedimentos, "ProcedimentoId", "Nome", agenda.ProcedimentoId);
            return View(agenda);
        }

        // GET: Admin/AdminAgenda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agendas == null)
            {
                return NotFound();
            }

            var agenda = await _context.Agendas
                .Include(a => a.Cliente)
                .Include(a => a.Procedimento)
                .FirstOrDefaultAsync(m => m.AgendaId == id);
            if (agenda == null)
            {
                return NotFound();
            }

            return View(agenda);
        }

        // POST: Admin/AdminAgenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agendas == null)
            {
                return Problem("Entity set 'AppDbContext.Agendas'  is null.");
            }
            var agenda = await _context.Agendas.FindAsync(id);
            if (agenda != null)
            {
                _context.Agendas.Remove(agenda);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendaExists(int id)
        {
          return (_context.Agendas?.Any(e => e.AgendaId == id)).GetValueOrDefault();
        }
    }
}
