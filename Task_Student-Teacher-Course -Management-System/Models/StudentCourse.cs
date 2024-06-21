using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class StudentCourse
    {

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
