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
                new Student() {FirstMidName="Kaarel-Martin",LastName="Noole",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Karl Umberto",LastName="Kats",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Kristjan Georg",LastName="Kessel",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Henri",LastName="Jervson",EnrollmentDate=DateTime.Now},
                new Student() {FirstMidName="Kenneth-Marcus",LastName="Aljas",EnrollmentDate=DateTime.Now},
            };

            context.Students.AddRange(students);

        }
    }
}
