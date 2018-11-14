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
    public class ParkingInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkingInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ParkingInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkingInfos.ToListAsync());
        }

        // GET: ParkingInfoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingInfo = await _context.ParkingInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingInfo == null)
            {
                return NotFound();
            }

            return View(parkingInfo);
        }

        // GET: ParkingInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkingInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ParkingName,TimeRate,Fee,MaxFee,Location")] ParkingInfo parkingInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingInfo);
        }

        // GET: ParkingInfoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingInfo = await _context.ParkingInfos.FindAsync(id);
            if (parkingInfo == null)
            {
                return NotFound();
            }
            return View(parkingInfo);
        }

        // POST: ParkingInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ParkingName,TimeRate,Fee,MaxFee,Location")] ParkingInfo parkingInfo)
        {
            if (id != parkingInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingInfoExists(parkingInfo.Id))
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
            return View(parkingInfo);
        }

        // GET: ParkingInfoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingInfo = await _context.ParkingInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingInfo == null)
            {
                return NotFound();
            }

            return View(parkingInfo);
        }

        // POST: ParkingInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parkingInfo = await _context.ParkingInfos.FindAsync(id);
            _context.ParkingInfos.Remove(parkingInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingInfoExists(long id)
        {
            return _context.ParkingInfos.Any(e => e.Id == id);
        }
    }
}
