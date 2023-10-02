using ContoseUniversity.Data;
using ContoseUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContoseUniversity.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var schoolContext = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .ToListAsync();

            return View(schoolContext);
            
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var student = new Student();
            student.Enrollments = new List<Enrollment>();
            PopulateAssignedCourseData(student);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnrollmentDate,FirstMidName,LastName")] Student student, string[] selectedCourses)
        {
            if (selectedCourses != null)
            {
                student.Enrollments = new List<Enrollment>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = new Enrollment
                    {
                        StudentId = student.Id,
                        CourseId = int.Parse(course)
                    };
                    student.Enrollments.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedCourseData(student);
            return View(student);


        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            if(student.Enrollments == null)
            {
                student.Enrollments = new List<Enrollment>();
            }

            PopulateAssignedCourseData(student); 

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Students
                .Include(i => i.Enrollments)
                .ThenInclude(i => i.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Student>(studentToUpdate, "",
                i => i.FirstMidName,
                i => i.LastName,
                i => i.EnrollmentDate))
            {

                UpdateStudentCourses(selectedCourses, studentToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                            "Try Again, and if the problem persists, " +
                            "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateStudentCourses(selectedCourses, studentToUpdate);
            PopulateAssignedCourseData(studentToUpdate);

            return View();

        }

        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Deletion has failed, Please try again or contact admin";
            }


            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SchoolContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void UpdateStudentCourses(string[] selectedCourses, Student studentToUpdate)
        {
            if (selectedCourses == null)
            {
                studentToUpdate.Enrollments = new List<Enrollment>();
                return;
            }
            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var studentCourses = new HashSet<int>(studentToUpdate.Enrollments.Select(c => c.CourseId));
            foreach (var course in _context.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseId.ToString()))
                {
                    if (!studentCourses.Contains(course.CourseId))
                    {
                        studentToUpdate.Enrollments.Add(new Enrollment { StudentId = studentToUpdate.Id, CourseId = course.CourseId });
                    }
                    else
                    {
                        if (studentCourses.Contains(course.CourseId))
                        {
                            Enrollment courseToRemove = studentToUpdate.Enrollments
                                .FirstOrDefault(e => e.CourseId == course.CourseId);
                            _context.Remove(courseToRemove);
                        }
                    }
                }
            }
        }

        private void PopulateAssignedCourseData(Student student)
        {
            var allCourses = _context.Courses;
            var studentCourses = new HashSet<int>(student.Enrollments.Select(c => c.CourseId));
            var vm = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                vm.Add(new AssignedCourseData
                {
                    CourseId = course.CourseId,
                    Title = course.Title,
                    Assigned = studentCourses.Contains(course.CourseId)
                });
            }
            ViewData["Courses"] = vm;
        }


    }
}
