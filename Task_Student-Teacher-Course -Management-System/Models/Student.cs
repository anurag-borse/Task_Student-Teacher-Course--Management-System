using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required(ErrorMessage ="Please Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Date Of Birth")]
        public DateTime DateOfBirth { get; set; }


        public string StudentImageURL { get; set; }


        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Select Course")]
        public string Course { get; set; }



        [Required(ErrorMessage = "Please Select Teacher")]
        public string Teacher { get; set; }


    }
}
