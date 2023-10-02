using ContoseUniversity.Models;
using NuGet.Packaging;

namespace ContoseUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            System.Diagnostics.Debug.WriteLine("Students");

            var students = new Student[]
            {
                new Student() {FirstMidName="Kort-Mort",LastName="Dort",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Karl Umberto",LastName="Kats",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Kristjan Georg",LastName="Kessel",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Henri",LastName="Jervson",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Kenneth-Marcus",LastName="Aljas",EnrollmentDate=DateTime.Now},
            };

            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();

            System.Diagnostics.Debug.WriteLine("Instructors");

            var instructors = new Instructor[]
            {
                new Instructor { FirstMidName = "George W", LastName = "Bush", HireDate = DateTime.Parse("2001/9/10")},
                new Instructor { FirstMidName = "Tarmo", LastName = "Härma", HireDate = DateTime.Parse("2005/2/14")},
                new Instructor { FirstMidName = "Eva", LastName = "Orav", HireDate = DateTime.Parse("2003/9/1")},
                new Instructor { FirstMidName = "Chungus", LastName = "Big", HireDate = DateTime.Parse("2016/1/1")}
            };

            foreach(Instructor instructor in instructors)
            {
                context.Instructors.Add(instructor);
            }
            context.SaveChanges();

            System.Diagnostics.Debug.WriteLine("departments");

            var departments = new Department[]
            {
                new Department{
                    Name="Terrorism",
                    Budget=13_000_000_000,
                    StartDate = DateTime.Parse("2001/9/10"),
                    InstructorId = 1
                }
            };

            foreach (Department department in departments)
            {
                context.Departments.Add(department);

            }
            context.SaveChanges();

            System.Diagnostics.Debug.WriteLine("Courses");

            var courses = new Course[]
            {
                new Course() {Title="Coding",Credits=160, DepartmentId=1}
            };

            foreach (Course course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            System.Diagnostics.Debug.WriteLine("Offices");

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment() 
                {
                    InstructorId = 1,
                    Location = "Oval Office"
                }
            };

            foreach (OfficeAssignment officeAssignment in officeAssignments) 
            {
                context.OfficeAssignments.Add(officeAssignment);  
            }
            context.SaveChanges();

            var courseAssignments = new CourseAssignment[]
            {
                new CourseAssignment()
                {
                    InstructorId = 1,
                    CourseId = 0
                }
            };

            foreach(CourseAssignment courseAssignment in courseAssignments) { 
               context.CourseAssignments.Add(courseAssignment);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment() {StudentId=3,CourseId=1050,Grade=Grade.A},
                new Enrollment() {StudentId=3,CourseId=1420,Grade=Grade.A},
                new Enrollment() {StudentId=3,CourseId=6900,Grade=Grade.D},
                new Enrollment() {StudentId=2,CourseId=1050,Grade=Grade.F},
                new Enrollment() {StudentId=1,CourseId=6666,Grade=Grade.F},
                new Enrollment() {StudentId=4,CourseId=1420,Grade=Grade.B},
                new Enrollment() {StudentId=5,CourseId=1234,Grade=Grade.A},
                new Enrollment() {StudentId=5,CourseId=1420,Grade=Grade.B},
            };

            foreach(Enrollment enrollment in enrollments)
            {
                context.Enrollments.Add(enrollment);
            }
            context.SaveChanges();



        }
    }
}
