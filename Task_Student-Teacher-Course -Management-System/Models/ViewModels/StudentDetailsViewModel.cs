using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models.View_Models
{
    public class StudentDetailsViewModel
    {
        [Key]
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string StudentImageURL { get; set; }
        public List<string> Courses { get; set; } = new List<string>();

    }
}
