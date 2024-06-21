using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please Enter Course Name")]
        [RegularExpression(@"^[a-zA-Z0-9#\-\+\s]+$", ErrorMessage = "Course Name can only contain letters, numbers, spaces, #, -, and +.")]
        public string CourseName { get; set; }




        [Required(ErrorMessage = "Please Enter Course Fee")]
        [Range(1, int.MaxValue, ErrorMessage = "Course Fee must be a positive number")]
        public int CourseFee { get; set; }



        public string? TeacherName { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
       

    }
}
