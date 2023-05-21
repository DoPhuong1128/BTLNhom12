using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTL_Nhom12.Data;
using BTL_Nhom12.Models;

namespace BTL_Nhom12.Controllers
{
    public class NhacungcapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhacungcapController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nhacungcap
        public async Task<IActionResult> Index()
        {
              return _context.Nhacungcap != null ? 
                          View(await _context.Nhacungcap.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Nhacungcap'  is null.");
        }

        // GET: Nhacungcap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Nhacungcap == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap
                .FirstOrDefaultAsync(m => m.MaNCC == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // GET: Nhacungcap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nhacungcap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNCC,TenNCC,SDTNCC,DiaChi,Email")] Nhacungcap nhacungcap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhacungcap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhacungcap);
        }

        // GET: Nhacungcap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Nhacungcap == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap.FindAsync(id);
            if (nhacungcap == null)
            {
                return NotFound();
            }
            return View(nhacungcap);
        }

        // POST: Nhacungcap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaNCC,TenNCC,SDTNCC,DiaChi,Email")] Nhacungcap nhacungcap)
        {
            if (id != nhacungcap.MaNCC)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhacungcap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhacungcapExists(nhacungcap.MaNCC))
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
            return View(nhacungcap);
        }

        // GET: Nhacungcap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Nhacungcap == null)
            {
                return NotFound();
            }

            var nhacungcap = await _context.Nhacungcap
                .FirstOrDefaultAsync(m => m.MaNCC == id);
            if (nhacungcap == null)
            {
                return NotFound();
            }

            return View(nhacungcap);
        }

        // POST: Nhacungcap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Nhacungcap == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nhacungcap'  is null.");
            }
            var nhacungcap = await _context.Nhacungcap.FindAsync(id);
            if (nhacungcap != null)
            {
                _context.Nhacungcap.Remove(nhacungcap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhacungcapExists(string id)
        {
          return (_context.Nhacungcap?.Any(e => e.MaNCC == id)).GetValueOrDefault();
        }
    }
}
