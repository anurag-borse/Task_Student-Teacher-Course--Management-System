using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please Enter Course Name")]
        public string CourseName { get; set; }


        [Required(ErrorMessage = "Please Select Teacher Name")]
        public string TeacherName { get; set; }



    }
}
