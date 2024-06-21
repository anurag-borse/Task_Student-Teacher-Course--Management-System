using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }



		[Required(ErrorMessage = "Please enter Admin Name")]
		[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Admin Name can only contain letters and spaces.")]
		public string AdminName { get; set; }


		[Required(ErrorMessage = "Please Enter Email")]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
		public string AdminEmail { get; set; }


        public string AdminPassword { get; set; }






    }
}
