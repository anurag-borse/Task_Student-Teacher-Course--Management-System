using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        //-------------------------------For Students--------------------------------

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = unitOfWork.Student.GetAll();

            return View(students);
        }

        public IActionResult AddStudent()
        {

            var courses = unitOfWork.Course.GetAll();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName", "TeacherName");

            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student, IFormFile file)
        {

            if (ModelState.IsValid)
            {

                if (file != null)
                {

                    var Image = file;
                    var fileName = Guid.NewGuid().ToString() + Image.FileName;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(fileStream);
                    }
                    student.StudentImageURL = Path.Combine("/Image", fileName).Replace("\\", "/"); 

                }


                int x = Int32.Parse(student.Course);
                var course = unitOfWork.Course.GetById(x);
                student.Course = course.CourseName;
                student.Teacher = course.TeacherName;


                unitOfWork.Student.Add(student);
                unitOfWork.Save();
                return RedirectToAction("GetStudents");
            }
            return View();

        }


        [HttpGet]
        public IActionResult EditStudent(int id)
        {
            var courses = unitOfWork.Course.GetAll();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName", "TeacherName");

            var student = unitOfWork.Student.GetById(id);
            return View(student);
        }


        [HttpPost]
        public IActionResult EditStudent(Student student, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                var studentinDb = unitOfWork.Student.GetById(student.StudentId);

                if (file != null)
                {

                    if (!string.IsNullOrEmpty(studentinDb.StudentImageURL))
                    {
                        var existingFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", studentinDb.StudentImageURL.TrimStart('/'));
                        if (System.IO.File.Exists(existingFilePath))
                        {
                            System.IO.File.Delete(existingFilePath);
                        }

                        var Image = file;
                        var fileName = Guid.NewGuid().ToString() + Image.FileName;
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            Image.CopyTo(fileStream);
                        }
                        studentinDb.StudentImageURL = Path.Combine("/Image", fileName).Replace("\\", "/");

                    }
                }
                else
                {
                    studentinDb.StudentImageURL = studentinDb.StudentImageURL;
                }

                studentinDb.FirstName = student.FirstName;
                studentinDb.LastName = student.LastName;
                studentinDb.DateOfBirth = student.DateOfBirth;
                studentinDb.Email = student.Email;

                int x = Int32.Parse(student.Course);
                var course = unitOfWork.Course.GetById(x);
                

                studentinDb.Course = course.CourseName;
                studentinDb.Teacher = course.TeacherName;


                unitOfWork.Student.Update(studentinDb);
                unitOfWork.Save();
                return RedirectToAction("GetStudents");


            }
            return View();
        }



        [HttpGet]
        public IActionResult DeleteStudent(int id)
        {

            var student = unitOfWork.Student.GetById(id);
            if (student != null)
            {
                unitOfWork.Student.Remove(student);
                unitOfWork.Save();
                return RedirectToAction("GetStudents");
            }
            return View();
        }


        //-------------------------------For Courses--------------------------------


        [HttpGet]
        public IActionResult GetCourses()
        {
            var courses = unitOfWork.Course.GetAll();

            return View(courses);
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            var teacher = unitOfWork.Teacher.GetAll();
            ViewBag.Teachers = new SelectList(teacher, "TeacherId", "FirstName");

            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Course.Add(course);
                unitOfWork.Save();
                return RedirectToAction("GetCourses", "Admin");

            }
            return View();
        }





        //-------------------------------For Teachers--------------------------------


        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teachers = unitOfWork.Teacher.GetAll();

            return View(teachers);
        }


        [HttpGet]
        public IActionResult AddTeacher()
        {
            var courses = unitOfWork.Course.GetAll();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName", "TeacherName");

            return View();
        }

        [HttpPost]
        public IActionResult AddTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Teacher.Add(teacher);
                unitOfWork.Save();
                return RedirectToAction("GetTeachers", "Admin");

            }
            return View();
        }





    }
}
