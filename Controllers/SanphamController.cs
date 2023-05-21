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
    public class SanphamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SanphamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sanpham
        public async Task<IActionResult> Index()
        {
              return _context.Sanpham != null ? 
                          View(await _context.Sanpham.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Sanpham'  is null.");
        }

        // GET: Sanpham/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Sanpham == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // GET: Sanpham/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sanpham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSanPham,TenSanPham,DVT,NSX,Kichco,Color,GiaTien,Soluongton")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanpham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanpham);
        }

        // GET: Sanpham/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Sanpham == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            return View(sanpham);
        }

        // POST: Sanpham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaSanPham,TenSanPham,DVT,NSX,Kichco,Color,GiaTien,Soluongton")] Sanpham sanpham)
        {
            if (id != sanpham.MaSanPham)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanpham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.MaSanPham))
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
            return View(sanpham);
        }

        // GET: Sanpham/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Sanpham == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanpham
                .FirstOrDefaultAsync(m => m.MaSanPham == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: Sanpham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Sanpham == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sanpham'  is null.");
            }
            var sanpham = await _context.Sanpham.FindAsync(id);
            if (sanpham != null)
            {
                _context.Sanpham.Remove(sanpham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(string id)
        {
          return (_context.Sanpham?.Any(e => e.MaSanPham == id)).GetValueOrDefault();
        }
    }
}
