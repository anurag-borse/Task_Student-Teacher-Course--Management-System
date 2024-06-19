using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class Teacher
    {

        [Key]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string LastName { get; set; }



        [Required(ErrorMessage = "Please Enter Salary")]
        public int Salary { get; set; }





        [Required(ErrorMessage = "Please Select Course")]
        public string Course { get; set; }


    }
}
