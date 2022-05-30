using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CastBank.Data;
using CastBank.Models;
using Core.Flash;

namespace CastBank.Controllers
{
    public class EmprestimosController : Controller
    {
        private IFlasher f;
        private readonly CastBankContext _context;

        public EmprestimosController(CastBankContext context, IFlasher f)
        {
            this.f = f;
            _context = context;
        }

        // GET: Emprestimos
        public async Task<IActionResult> Index()
        {
            var castBankContext = _context.Emprestimo.Include(e => e.Empresa);
            return View(await castBankContext.ToListAsync());
        }

        // GET: Emprestimos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // GET: Emprestimos/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "Id", "Nome");

            return View();
        }

        // POST: Emprestimos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valor,Parcelas,EmpresaId")] Emprestimo emprestimo)
        {
            var empresa = await _context.Empresa.FindAsync(emprestimo.EmpresaId);
            if (empresa.EmprestimoAtivo)
            {
                f.Flash(Types.Danger, "A empresa já possui um empréstimo ativo.", dismissable: true);
                return RedirectToAction(nameof(Create));
            }

            if (ModelState.IsValid)
            {
                empresa.EmprestimoAtivo = true;
                _context.Add(emprestimo);
                emprestimo.ValorParcelas = (emprestimo.Valor / emprestimo.Parcelas) + (emprestimo.Valor * 0.06);
                await _context.SaveChangesAsync();

                f.Flash(Types.Success, "Empréstimo realizado com sucesso", dismissable: true);
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "Id", "CNPJ", emprestimo.EmpresaId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo.FindAsync(id);
            if (emprestimo == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "Id", "CNPJ", emprestimo.EmpresaId);
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,Parcelas,EmpresaId")] Emprestimo emprestimo)
        {
            if (id != emprestimo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emprestimo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmprestimoExists(emprestimo.Id))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresa, "Id", "CNPJ", emprestimo.EmpresaId);
            return View(emprestimo);
        }

        // GET: Emprestimos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emprestimo = await _context.Emprestimo
                .Include(e => e.Empresa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        // POST: Emprestimos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emprestimo = await _context.Emprestimo.FindAsync(id);
            var empresa = await _context.Empresa.FindAsync(emprestimo.EmpresaId);
            empresa.EmprestimoAtivo = false;
            _context.Emprestimo.Remove(emprestimo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmprestimoExists(int id)
        {
            return _context.Emprestimo.Any(e => e.Id == id);
        }
    }
}
