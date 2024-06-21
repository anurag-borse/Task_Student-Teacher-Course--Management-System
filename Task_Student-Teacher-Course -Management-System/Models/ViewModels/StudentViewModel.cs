using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models.ViewModels
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Date Of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public string? StudentImageURL { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public List<int> SelectedCourseIds { get; set; } = new List<int>();

        public IEnumerable<SelectListItem>? AvailableCourses { get; set; }
    }
}
