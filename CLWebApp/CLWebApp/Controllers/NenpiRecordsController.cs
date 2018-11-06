using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CLWebApp.Data;
using CLWebApp.Models;

namespace CLWebApp.Controllers
{
    public class NenpiRecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NenpiRecordsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NenpiRecords
        public async Task<IActionResult> Index()
        {
            return View(await _context.NenpiRecords.ToListAsync());
        }

        // GET: NenpiRecords/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nenpiRecord = await _context.NenpiRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nenpiRecord == null)
            {
                return NotFound();
            }

            return View(nenpiRecord);
        }

        // GET: NenpiRecords/Create
        public IActionResult Create()
        {
            // ユーザーリスト設定
            SetNameSelectListToViewBag();
            return View();
        }

        // POST: NenpiRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RefuelDate,Mileage,TripMileage,FuelCost")] NenpiRecord nenpiRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nenpiRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ユーザーリスト設定
            SetNameSelectListToViewBag();
            return View(nenpiRecord);
        }

        // GET: NenpiRecords/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nenpiRecord = await _context.NenpiRecords.FindAsync(id);
            if (nenpiRecord == null)
            {
                return NotFound();
            }
            return View(nenpiRecord);
        }

        // POST: NenpiRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,RefuelDate,Mileage,TripMileage,FuelCost")] NenpiRecord nenpiRecord)
        {
            if (id != nenpiRecord.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nenpiRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NenpiRecordExists(nenpiRecord.Id))
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
            return View(nenpiRecord);
        }

        // GET: NenpiRecords/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nenpiRecord = await _context.NenpiRecords
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nenpiRecord == null)
            {
                return NotFound();
            }

            return View(nenpiRecord);
        }

        // POST: NenpiRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var nenpiRecord = await _context.NenpiRecords.FindAsync(id);
            _context.NenpiRecords.Remove(nenpiRecord);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NenpiRecordExists(long id)
        {
            return _context.NenpiRecords.Any(e => e.Id == id);
        }

        /// <summary>
        /// ユーザーネームリスト情報をViewBagに設定
        /// </summary>
        /// <param name="context"></param>
        private void SetNameSelectListToViewBag()
        {
            var names = _context.Users.OrderBy(c => c.Email).Select(x => new { Id = x.Id, Value = x.FullName });
            ViewBag.Namae = new SelectList(names,"Id","Value");
        }
    }
}
