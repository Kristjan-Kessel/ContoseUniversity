using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContoseUniversity.Data;
using ContoseUniversity.Models;

namespace ContoseUniversity.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly SchoolContext _context;

        public InstructorsController(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instructors.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HireDate,FirstMidName,LastName")] Instructor instructor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(instructor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persist " + "see your system administrator.");
            }
            return View(instructor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return NotFound();
            }
            return View(instructor);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructorToUpdate = await _context.Instructors.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Instructor>(instructorToUpdate, "", s => s.FirstMidName, s => s.LastName, s => s.HireDate))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persist " + "see your system administrator.");
                }
            }
            return View(instructorToUpdate);
        }
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }
            var instructor = await _context.Instructors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "Deletion has failed, please try again, and if the problem presists " + "see your system administrator";
            }
            return View(instructor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}