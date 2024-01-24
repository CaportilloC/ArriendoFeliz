using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArriendoFeliz;
using ArriendoFeliz.Entidades;

namespace ArriendoFeliz.Controllers
{
    public class ArriendosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArriendosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult PatenteDisponible(string Patente, int id)
        {
            bool isAvailable = !_context.Arriendos.Any(w => w.Patente == Patente && w.Id != id && !w.EstaBorrado);
            return Json(isAvailable);
        }

        public async Task<IActionResult> Index()
        {
            var arriendos = await _context.Arriendos
                .Include(a => a.Cliente)
                .Include(a => a.Modelo)
                .Include(a => a.Marca)
                .Where(a => !a.EstaBorrado)
                .Select(a => new
                {
                    Id = a.Id,
                    Cliente = $"{a.Cliente.Nombres} {a.Cliente.Apellidos}  #{a.Cliente.Rut}",
                    Modelo = a.Modelo.NombreModelo,
                    Marca = a.Marca.NombreMarca,
                    Patente = a.Patente,
                    FechaInicio = a.FechaInicio,
                    FechaFin = a.FechaFin,
                    DiasArriendo = a.DiasArriendo,
                })
                .ToListAsync();

            ViewBag.DataSource = arriendos;
            return View();
        }

        public async Task<IActionResult> Reporte()
        {
            DateTime fechaActual = DateTime.Now.Date;

            var arriendos = await _context.Arriendos
                .Include(a => a.Cliente)
                .Include(a => a.Modelo)
                .Include(a => a.Marca)
                .Where(a => !a.EstaBorrado && a.FechaFin != null && a.FechaFin.Value.Date >= fechaActual)
                .OrderBy(a => a.FechaFin)
                .Select(a => new
                {
                    Id = a.Id,
                    Cliente = $"{a.Cliente.Nombres} {a.Cliente.Apellidos}  #{a.Cliente.Rut}",
                    Modelo = a.Modelo.NombreModelo,
                    Marca = a.Marca.NombreMarca,
                    Patente = a.Patente,
                    FechaInicio = a.FechaInicio,
                    FechaFin = a.FechaFin,
                    DiasArriendo = a.DiasArriendo,
                })
                .ToListAsync();

            ViewBag.DataSource = arriendos;
            return View();
        }

        public IActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes.Where(c => !c.EstaBorrado)
                .Select(c => new { Id = c.Id, NombreCompleto = $"{c.Nombres} {c.Apellidos} #{c.Rut}" })
                .ToList(), "Id", "NombreCompleto");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "NombreMarca");
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "NombreModelo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,ModeloId,MarcaId,FechaInicio,FechaFin,Patente,EstaBorrado")] Arriendo arriendo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(arriendo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellidos", arriendo.ClienteId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "NombreMarca", arriendo.MarcaId);
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "NombreModelo", arriendo.ModeloId);
            return View(arriendo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arriendo = await _context.Arriendos.FindAsync(id);
            if (arriendo == null)
            {
                return NotFound();
            }
            ViewBag.ClienteId = new SelectList(_context.Clientes.Where(c => !c.EstaBorrado)
                .Select(c => new { Id = c.Id, NombreCompleto = $"{c.Nombres} {c.Apellidos} #{c.Rut}" })
                .ToList(), "Id", "NombreCompleto");
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "NombreMarca", arriendo.MarcaId);
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "NombreModelo", arriendo.ModeloId);
            return View(arriendo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,ModeloId,MarcaId,FechaInicio,FechaFin,Patente,EstaBorrado")] Arriendo arriendo)
        {
            if (id != arriendo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(arriendo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArriendoExists(arriendo.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Apellidos", arriendo.ClienteId);
            ViewData["MarcaId"] = new SelectList(_context.Marcas, "MarcaId", "NombreMarca", arriendo.MarcaId);
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "ModeloId", "NombreModelo", arriendo.ModeloId);
            return View(arriendo);
        }

        public async Task<ActionResult> SoftDelete(int id)
        {
            var arriendo = await _context.Arriendos.AsTracking().FirstOrDefaultAsync(g => g.Id == id);

            if (arriendo is null)
            {
                return NotFound();
            }

            arriendo.EstaBorrado = true;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool ArriendoExists(int id)
        {
            return _context.Arriendos.Any(e => e.Id == id);
        }
    }
}
