using Task_Student_Teacher_Course__Management_System.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Task_Student_Teacher_Course__Management_System.Data;
using Task_Student_Teacher_Course__Management_System.Models;

namespace Task_Student_Teacher_Course__Management_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }


        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = unitOfWork.Admin.GetByUsername(username);

            if (admin != null)
            {
                if (admin.AdminPassword == password)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }



            return View();
        }



    }
}
