using ContoseUniversity.Data;
using ContoseUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ContoseUniversity.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolContext _context;

        public CoursesController(SchoolContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Retrieve a list of courses including department information
            var courses = await _context.Courses
                .Include(c => c.Department)
                .ToListAsync();

            // Create a list of CourseViewModel objects to hold the data for each course
            var courseViewModels = new List<CourseViewModel>();

            // Populate each CourseViewModel
            foreach (var course in courses)
            {
                var courseViewModel = new CourseViewModel
                {
                    course = course,
                    assignedInstructors = await _context.CourseAssignments
                        .Where(ca => ca.CourseId == course.CourseId)
                        .Select(ca => ca.Instructor)
                        .ToListAsync(),
                    assignedStudents = await _context.Enrollments
                        .Where(ca => ca.CourseId == course.CourseId)
                        .Select(ca => ca.Student)
                        .ToListAsync(),
                };

                courseViewModels.Add(courseViewModel);
            }

            // Pass the list of CourseViewModels to the view
            return View(courseViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            var courseViewModel = new CourseViewModel
            {
                course = course,
                assignedInstructors = await _context.CourseAssignments
                .Where(ca => ca.CourseId == course.CourseId)
                .Select(ca => ca.Instructor)
                .ToListAsync(),
                assignedStudents = await _context.Enrollments
                .Where(ca => ca.CourseId == course.CourseId)
                .Select(ca => ca.Student)
                .ToListAsync(),

            };

            return View(courseViewModel);
        }

        //get create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            return View();
        }

        //post create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Credits,DepartmentId")] Course course)
        {

            ModelState.Remove("Department");
            ModelState.Remove("Enrollments");
            ModelState.Remove("CourseAssignments");

            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", course.DepartmentId);

            return View(course);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(s => s.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int courseId)
        {
            Course course = await _context.Courses
                .SingleAsync(c => c.CourseId == courseId);

            //remove any associated course assignments
            var assignments = await _context.CourseAssignments
            .Where(ca => ca.CourseId == courseId)
            .ToListAsync();

            foreach (var assignment in assignments)
            {
                _context.CourseAssignments.Remove(assignment);
            }

            //remove enrollments
            var enrollments = await _context.Enrollments
                .Where(e => e.CourseId == courseId)
                .ToListAsync();

            foreach(var enrollment in enrollments)
            {
                _context.Enrollments.Remove(enrollment);
            }

            await _context.SaveChangesAsync();


            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name");
            ViewData["Instructors"] = await _context.Instructors.ToListAsync();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,Credits,DepartmentId")] Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If ModelState is not valid, redisplay the form with validation errors
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "DepartmentId", "Name", course.DepartmentId);
            return View(course);
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }

    }

}
