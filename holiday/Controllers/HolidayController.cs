#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Holidays.Models;
using holiday.Data;
using System.Security.Claims;

namespace holiday.Controllers
{
    public class HolidayController : Controller
    { 

        private readonly ApplicationDbContext _context;

        public HolidayController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Holiday/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Holiday/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Holiday.Where(j => j.HolidayName.Contains(SearchPhrase)).ToListAsync());
        }

        // Get: Holiday index with sort options
        public ActionResult Index(string sortOrder)

        {
            // Get username string to filter database from for personal holiday records
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = id;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var holidays = from s in _context.Holiday.Where(j => j.UserId.Contains(id)) select s;

            // Get email string to filter holiday records that are to be approved by the user
            string approver = User.FindFirstValue(ClaimTypes.Name);
            var approve = from s in _context.Holiday.Where(j => j.Approver.Contains(approver)) select s;
            approve = approve.Where(m => m.Approved == 0);

            // Check if records exist
            if (approve != null)
            {
                ViewBag.toApprove = approve.ToList();
            }
        
           
            switch (sortOrder)
            {
                case "Date":
                    holidays = _context.Holiday.Where(j => j.UserId.Contains(id)).OrderBy(s => s.StartDate);
                    break;
                default:
                    holidays = _context.Holiday.Where(j => j.UserId.Contains(id)).OrderBy(s => s.HolidayName);
                    break;
            }

            return View(holidays.ToList());
        }

        /// POST: Called from /Index/ as a method of approval of a specific holiday by line manager
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var approve = await _context.Holiday.FirstOrDefaultAsync(m => m.id == id);
            approve.Approved = 1;
            _context.Update(approve);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ///  POST: Called from /Index/ as a method of approval of a specific holiday by line manager
        public async Task<IActionResult> Decline(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var decline = await _context.Holiday.FirstOrDefaultAsync(m => m.id == id);
            decline.Approved = 2;
            _context.Update(decline);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Holiday/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holiday
                .FirstOrDefaultAsync(m => m.id == id);
            if (holiday == null)
            {
                return NotFound();
            }

            return View(holiday);
        }

        // GET: Holiday/Create
        public IActionResult Create()
        {

            //TODO Calculate cost automatically from weekdays in view

            // Created for hidden field in view to store owner of entry
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Id = id;

            //Calculate current cost of submitted holidays and send to view
            //Denial of creation will be handled in browser

            // Get holidays currently created by user
            var holidays = _context.Holiday.Where(j => j.UserId.Contains(id));
            // Check for valid entried in the database
            if (holidays != null)
            {
                // Iterate over the variable and add 'cost' to int total
                int total = 0;
                foreach (var holiday in holidays)
                    total += holiday.Cost;

                ViewBag.Total = total;
            }
            else { }
            return View();
        }

        // POST: Holiday/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,HolidayName,UserId,StartDate,EndDate,Approved,Type,Cost,Approver")] Holiday holiday)
        {
            if (ModelState.IsValid)
            {
                _context.Add(holiday);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(holiday);
        }

        // GET: Holiday/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holiday.FindAsync(id);
            if (holiday == null)
            {
                return NotFound();
            }
            return View(holiday);
        }

        // POST: Holiday/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,HolidayName,UserId,StartDate,EndDate,Approved,Type,Cost,Approver")] Holiday holiday)
        {
            if (id != holiday.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {




                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HolidayExists(holiday.id))
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
            return View(holiday);
        }

        // GET: Holiday/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holiday = await _context.Holiday
                .FirstOrDefaultAsync(m => m.id == id);
            if (holiday == null)
            {
                return NotFound();
            }

            return View(holiday);
        }

        // POST: Holiday/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var holiday = await _context.Holiday.FindAsync(id);
            _context.Holiday.Remove(holiday);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HolidayExists(int id)
        {
            return _context.Holiday.Any(e => e.id == id);
        }
    }
}
