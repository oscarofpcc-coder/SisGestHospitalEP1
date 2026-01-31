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
    public class CitasController : Controller
    {
        private readonly HospitalDbContext _context;

        public CitasController(HospitalDbContext context)
        {
            _context = context;
        }




        // GET: CitaMedicas
        public async Task<IActionResult> Index()
        {
            var hospitalDbContext = _context.Citas.Include(c => c.Doctor).Include(c => c.Paciente);
            return View(await hospitalDbContext.ToListAsync());
        }

        // GET: CitaMedicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.Citas
                .Include(c => c.Doctor)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // GET: CitaMedicas/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "Apellido");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Apellido");
            return View();
        }

        public IActionResult CreateModal()
        {
            //ViewBag.Titulo = "Nueva Cita";
            //ViewBag.ActionUrl = Url.Action("CreateAjax");
            //return PartialView("_FormModal", new Cita());
            ViewBag.Titulo = "Nueva Cita";
            ViewBag.ActionUrl = Url.Action("CreateAjax");

            ViewBag.Pacientes = new SelectList(
                _context.Pacientes.Select(p => new {
                    Id = p.PacienteId,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }).ToList(),
                "Id",
                "NombreCompleto"
            );

            ViewBag.Doctores = new SelectList(
                _context.Doctores.Select(d => new {
                    Id = d.DoctorId,
                    NombreCompleto = d.Nombre + " " + d.Apellido
                }).ToList(),
                "Id",
                "NombreCompleto"
            );

            return PartialView("_FormModal", new Cita());
        }


        // POST: CitaMedicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaId,PacienteId,DoctorId,FechaHora,Motivo,Estado")] Cita citaMedica)
        {
           bool ocupado = await _context.Citas.AnyAsync(c =>
           c.DoctorId == citaMedica.DoctorId &&
           c.FechaHora == citaMedica.FechaHora &&
           c.Estado != "Cancelada");

            if (ocupado)
            {
                ModelState.AddModelError("", "El doctor ya tiene una cita en esa fecha y hora.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(citaMedica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "Apellido", citaMedica.DoctorId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Apellido", citaMedica.PacienteId);
            return View(citaMedica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAjax(Cita cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos inválidos");

            _context.Add(cita);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET: CitaMedicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.Citas.FindAsync(id);
            if (citaMedica == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "Apellido", citaMedica.DoctorId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Apellido", citaMedica.PacienteId);
            return View(citaMedica);
        }

        public async Task<IActionResult> EditModal(int id)
        {
            //var cita = await _context.Citas.FindAsync(id);
            //if (cita == null) return NotFound();

            //ViewBag.Titulo = "Editar Cita";
            //ViewBag.ActionUrl = Url.Action("EditAjax");
            //return PartialView("_FormModal", cita);
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            ViewBag.Titulo = "Editar Cita";
            ViewBag.ActionUrl = Url.Action("EditAjax");

            ViewBag.Pacientes = new SelectList(
                _context.Pacientes.Select(p => new {
                    Id = p.PacienteId,
                    NombreCompleto = p.Nombre + " " + p.Apellido
                }).ToList(),
                "Id",
                "NombreCompleto",
                cita.PacienteId // 👈 seleccionado automático
            );

            ViewBag.Doctores = new SelectList(
                _context.Doctores.Select(d => new {
                    Id = d.DoctorId,
                    NombreCompleto = d.Nombre + " " + d.Apellido
                }).ToList(),
                "Id",
                "NombreCompleto",
                cita.DoctorId // 👈 seleccionado automático
            );

            return PartialView("_FormModal", cita);
        }



        // POST: CitaMedicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaId,PacienteId,DoctorId,FechaHora,Motivo,Estado")] Cita citaMedica)
        {
            if (id != citaMedica.CitaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citaMedica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaMedicaExists(citaMedica.CitaId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctores, "DoctorId", "Apellido", citaMedica.DoctorId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "PacienteId", "Apellido", citaMedica.PacienteId);
            return View(citaMedica);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAjax(Cita cita)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos inválidos");

            _context.Update(cita);
            await _context.SaveChangesAsync();
            return Ok();
        }


        // GET: CitaMedicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaMedica = await _context.Citas
                .Include(c => c.Doctor)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.CitaId == id);
            if (citaMedica == null)
            {
                return NotFound();
            }

            return View(citaMedica);
        }

        // POST: CitaMedicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citaMedica = await _context.Citas.FindAsync(id);
            if (citaMedica != null)
            {
                _context.Citas.Remove(citaMedica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaMedicaExists(int id)
        {
            return _context.Citas.Any(e => e.CitaId == id);
        }
    }
}
