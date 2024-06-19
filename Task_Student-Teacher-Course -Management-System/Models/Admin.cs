using System.ComponentModel.DataAnnotations;

namespace Task_Student_Teacher_Course__Management_System.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }


        public string AdminName { get; set; }

        public string AdminEmail { get; set; }


        public string AdminPassword { get; set; }






    }
}
