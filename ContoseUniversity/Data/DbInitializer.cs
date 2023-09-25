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

            var students = new Student[]
            {
                new Student() {FirstMidName="Kaarel-Martin",LastName="Noole",EnrollmentDate=DateTime.Now},
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

            var departments = new Department[]
            {
                new Department{ 
                    Name="Terrorism", 
                    Budget=13_000_000_000, 
                    StartDate = DateTime.Parse("2001/9/10"), 
                    InstructorId = instructors.Single(i => i.LastName == "Bush").Id
                },
                new Department{
                    Name="Gym",
                    Budget=5_000_000,
                    StartDate = DateTime.Parse("2003/2/23"),
                    InstructorId = instructors.Single(i => i.LastName == "Härma").Id
                },
                new Department{
                    Name="Hunting",
                    Budget=0,
                    StartDate = DateTime.Parse("20016/2/6"),
                    InstructorId = instructors.Single(i => i.LastName == "Big").Id
                },
                new Department{
                    Name="Gathering",
                    Budget=0,
                    StartDate = DateTime.Parse("2001/9/10"),
                    InstructorId = instructors.Single(i => i.LastName == "Orav").Id
                }
            };

            foreach(Department department in departments)
            {
                context.Departments.Add(department);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course() {CourseId=1050,Title="Coding",Credits=160},         
                new Course() {CourseId=6666,Title="Testing",Credits=160},
                new Course() {CourseId=1420,Title="Math",Credits=160},
                new Course() {CourseId=1420,Title="Biology",Credits=160},
                new Course() {CourseId=1234,Title="Defense",Credits=160},
                new Course() {CourseId=6900,Title="Chemistry",Credits=160},
            };

            foreach (Course course in courses)
            {
                context.Courses.Add(course);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment() 
                {
                    InstructorId = instructors.Single(i => i.LastName == "Bush").Id,
                    Location = "Oval Office"
                },
                new OfficeAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Härma").Id,
                    Location = "Backyard"
                },
                new OfficeAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Big").Id,
                    Location = "Depths Of Hell"
                },
                new OfficeAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Orav").Id,
                    Location = "Tree"
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
                    InstructorId = instructors.Single(i => i.LastName == "Bush").Id,
                    CourseId = courses.Single(i => i.Title == "Defense").CourseId
                },
                new CourseAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Bush").Id,
                    CourseId = courses.Single(i => i.Title == "Chemistry").CourseId
                },
                new CourseAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Orav").Id,
                    CourseId = courses.Single(i => i.Title == "Math").CourseId
                },
                new CourseAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Orav").Id,
                    CourseId = courses.Single(i => i.Title == "Biology").CourseId
                },
                new CourseAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Big").Id,
                    CourseId = courses.Single(i => i.Title == "Coding").CourseId
                },
                new CourseAssignment()
                {
                    InstructorId = instructors.Single(i => i.LastName == "Big").Id,
                    CourseId = courses.Single(i => i.Title == "Testing").CourseId
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
