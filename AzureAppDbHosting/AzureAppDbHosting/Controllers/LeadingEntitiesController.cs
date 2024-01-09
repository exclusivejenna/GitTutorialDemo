using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AzureAppDbHosting.Data;
using AzureAppDbHosting.Models;

namespace AzureAppDbHosting.Controllers
{
    public class LeadingEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeadingEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeadingEntities
        public async Task<IActionResult> Index()
        {
              return _context.Leads != null ? 
                          View(await _context.Leads.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Leads'  is null.");
        }

        // GET: LeadingEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leads == null)
            {
                return NotFound();
            }

            var leadingEntity = await _context.Leads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leadingEntity == null)
            {
                return NotFound();
            }

            return View(leadingEntity);
        }

        // GET: LeadingEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeadingEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LeadDate,LeadSource,Name,Mobile,Email")] LeadingEntity leadingEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leadingEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leadingEntity);
        }

        // GET: LeadingEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leads == null)
            {
                return NotFound();
            }

            var leadingEntity = await _context.Leads.FindAsync(id);
            if (leadingEntity == null)
            {
                return NotFound();
            }
            return View(leadingEntity);
        }

        // POST: LeadingEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LeadDate,LeadSource,Name,Mobile,Email")] LeadingEntity leadingEntity)
        {
            if (id != leadingEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leadingEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeadingEntityExists(leadingEntity.Id))
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
            return View(leadingEntity);
        }

        // GET: LeadingEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leads == null)
            {
                return NotFound();
            }

            var leadingEntity = await _context.Leads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leadingEntity == null)
            {
                return NotFound();
            }

            return View(leadingEntity);
        }

        // POST: LeadingEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leads == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Leads'  is null.");
            }
            var leadingEntity = await _context.Leads.FindAsync(id);
            if (leadingEntity != null)
            {
                _context.Leads.Remove(leadingEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeadingEntityExists(int id)
        {
          return (_context.Leads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
