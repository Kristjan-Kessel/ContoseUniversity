using ContoseUniversity.Models;

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
                new Student() {Id=1,FirstMidName="Kaarel-Martin",LastName="Noole",EnrollmentDate=DateTime.Now},
                new Student() {Id=2,FirstMidName="Karl Umberto",LastName="Kats",EnrollmentDate=DateTime.Now},
                new Student() {Id=3,FirstMidName="Kristjan Georg",LastName="Kessel",EnrollmentDate=DateTime.Now},
                new Student() {Id=4,FirstMidName="Henri",LastName="Jervson",EnrollmentDate=DateTime.Now},
                new Student() {Id=5,FirstMidName="Kenneth-Marcus",LastName="Aljas",EnrollmentDate=DateTime.Now},
            };

            foreach (Student student in students)
            {
                context.Students.Add(student);
            }

            var courses = new Course[]
            {
                new Course() {CourseId=1050,Title="Programmeerimine",Credits=160},
                new Course() {CourseId=6900,Title="Keemia",Credits=160},
                new Course() {CourseId=1420,Title="Matemaatika",Credits=160},
                new Course() {CourseId=6666,Title="Testimine",Credits=160},
                new Course() {CourseId=1234,Title="Riigikaitse",Credits=160},
            };

            foreach (Course course in courses)
            {
                context.Courses.Add(course);
            }
            

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
