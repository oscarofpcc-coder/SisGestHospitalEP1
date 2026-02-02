using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SisGestionCitasMedicas.Data;
using SisGestionCitasMedicas.Models;

namespace SisGestionCitasMedicas
{
    public class CatalogosController : Controller
    {
        private readonly HospitalDbContext _context;

        public CatalogosController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Catalogos
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.Catalogos.Include(c => c.Tabla);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: Catalogoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos
                .Include(c => c.Tabla)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // GET: Catalogoes/Create
        public IActionResult Create()
        {
            ViewData["TablaId"] = new SelectList(_context.Tablas, "TablaId", "CodTabla");
            return View();
        }

        // POST: Catalogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodCatalogo,Descripcion,Estado,TablaId")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TablaId"] = new SelectList(_context.Tablas, "TablaId", "CodTabla", catalogo.TablaId);
            return View(catalogo);
        }

        // GET: Catalogoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound();
            }
            ViewData["TablaId"] = new SelectList(_context.Tablas, "TablaId", "CodTabla", catalogo.TablaId);
            return View(catalogo);
        }

        // POST: Catalogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodCatalogo,Descripcion,Estado,TablaId")] Catalogo catalogo)
        {
            if (id != catalogo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogoExists(catalogo.Id))
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
            ViewData["TablaId"] = new SelectList(_context.Tablas, "TablaId", "CodTabla", catalogo.TablaId);
            return View(catalogo);
        }

        // GET: Catalogoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos
                .Include(c => c.Tabla)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // POST: Catalogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo != null)
            {
                _context.Catalogos.Remove(catalogo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogoExists(int id)
        {
            return _context.Catalogos.Any(e => e.Id == id);
        }
    }
}
