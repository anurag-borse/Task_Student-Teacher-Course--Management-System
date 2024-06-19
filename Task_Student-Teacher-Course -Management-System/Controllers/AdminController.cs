using Microsoft.AspNetCore.Mvc;
using Task_Student_Teacher_Course__Management_System.Models;
using Task_Student_Teacher_Course__Management_System.Repository.IRepository;

namespace Task_Student_Teacher_Course__Management_System.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            
        }



        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = unitOfWork.Student.GetAll();



            return View(students);
        }

        public IActionResult AddStudent()
        {



            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student,IFormFile file)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Student.Add(student);
                unitOfWork.Save();
                return RedirectToAction("GetStudents");
            }
            return View(student);
        }



    }
}
