namespace ContoseUniversity.Models
{
    public class CourseAssignment
    {
        public int InstructorId { get; set; }
        public int CourseId { get; set; }
        public Instructor instructor { get; set; }
        public Course course { get; set; }
    }
}
