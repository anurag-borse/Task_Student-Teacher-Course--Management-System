using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class Teacher
    {

        [Key]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters.")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters.")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Please Enter Salary")]
        [Range(1, int.MaxValue, ErrorMessage = "Salary be a positive number")]
        public int Salary { get; set; }


        
        public string? Course { get; set; }


    }
}
