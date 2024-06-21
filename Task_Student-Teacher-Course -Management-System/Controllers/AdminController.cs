using HRMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Task_Student_Teacher_Course__Management_System.Models;
using Task_Student_Teacher_Course__Management_System.Repository.IRepository;
using System.Linq;
using Task_Student_Teacher_Course__Management_System.Models.View_Models;
using Microsoft.EntityFrameworkCore;
using Task_Student_Teacher_Course__Management_System.Models.ViewModels;

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
        public IActionResult GetStudents(string searchString)
        {
            var students = unitOfWork.Student.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.FirstName.Contains(searchString) ||
                                               s.LastName.Contains(searchString) ||
                                               s.StudentCourses.Any(sc => sc.Course.CourseName.Contains(searchString)) ||
                                               s.StudentId.ToString().Contains(searchString) ||
                                               s.StudentCourses.Any(sc => sc.Course.TeacherName.Contains(searchString))).ToList();
            }

            return View(students);
        }

        public IActionResult AddStudent()
        {
            var viewModel = new StudentViewModel
            {
                AvailableCourses = unitOfWork.Course.GetAll().Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.CourseName
                }).ToList()
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    Email = model.Email,
                    StudentImageURL = UploadFile(file)

                };

                foreach (var courseId in model.SelectedCourseIds)
                {
                    student.StudentCourses.Add(new StudentCourse { CourseId = courseId });
                }

                unitOfWork.Student.Add(student);
                unitOfWork.Save();
                return RedirectToAction("GetStudents");
            }

            model.AvailableCourses = unitOfWork.Course.GetAll().Select(c => new SelectListItem
            {
                Value = c.CourseId.ToString(),
                Text = c.CourseName
            }).ToList();

            return View(model);
        }





        private string UploadFile(IFormFile file, string existingFilePath = null)
        {
            if (file == null || file.Length == 0)
            {
                return existingFilePath;
            }

            if (!string.IsNullOrEmpty(existingFilePath))
            {
                var existingPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingFilePath.TrimStart('/'));
                if (System.IO.File.Exists(existingPath))
                {
                    System.IO.File.Delete(existingPath);
                }
            }

            var newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Image", file.FileName);

            using (var stream = new FileStream(newPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Image/" + file.FileName;
        }

        public IActionResult EditStudent(int id)
        {
            var student = unitOfWork.Student.GetAllWithIncludes(s => s.StudentCourses)
                .FirstOrDefault(s => s.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                StudentImageURL = student.StudentImageURL,
                SelectedCourseIds = student.StudentCourses.Select(sc => sc.CourseId).ToList(),
                AvailableCourses = unitOfWork.Course.GetAll().Select(c => new SelectListItem
                {
                    Value = c.CourseId.ToString(),
                    Text = c.CourseName
                }).ToList()
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentViewModel model, IFormFile file)
        {
            if (model.StudentId != null)
            {


                var student = unitOfWork.Student.GetAllWithIncludes(s => s.StudentCourses)
                    .FirstOrDefault(s => s.StudentId == model.StudentId);

                if (student == null)
                {
                    return NotFound();
                }

                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.DateOfBirth = model.DateOfBirth;
                student.Email = model.Email;

                if (file != null && file.Length > 0)
                {
                    student.StudentImageURL = UploadFile(file, student.StudentImageURL);
                }
                else
                {
                    student.StudentImageURL = student.StudentImageURL;
                }

                student.StudentCourses.Clear();
                foreach (var courseId in model.SelectedCourseIds)
                {
                    student.StudentCourses.Add(new StudentCourse { CourseId = courseId });
                }

                unitOfWork.Student.Update(student);
                unitOfWork.Save();
                return RedirectToAction("GetStudents");
            }

            return View();
        }
        [HttpGet]
        public IActionResult DetailsStudent(int id)
        {
            var student = unitOfWork.Student.Get(s => s.StudentId == id, includeProperties: "StudentCourses.Course");
            if (student == null)
            {
                return NotFound();
            }

            var viewModel = new StudentDetailsViewModel
            {
                StudentId = student.StudentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                StudentImageURL = student.StudentImageURL,
                Courses = student.StudentCourses.Select(sc => sc.Course.CourseName).ToList()
            };

            return View(viewModel);
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

        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course course = unitOfWork.Course.GetById(id);
            var teacher = unitOfWork.Teacher.GetAll();
            ViewBag.Teachers = new SelectList(teacher, "TeacherId", "FirstName");
            return View(course);
        }

        [HttpPost]
        public IActionResult EditCourse(Course course)
        {
            Course courseInDb = unitOfWork.Course.GetById(course.CourseId);
            if (courseInDb != null)
            {
                courseInDb.CourseName = course.CourseName;
                courseInDb.CourseFee = course.CourseFee;
                courseInDb.TeacherName = course.TeacherName;
                unitOfWork.Course.Update(courseInDb);
                unitOfWork.Save();
                return RedirectToAction("GetCourses", "Admin");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteCourse(int id)
        {
            Course course = unitOfWork.Course.GetById(id);
            if (course != null)
            {
                unitOfWork.Course.Remove(course);
                unitOfWork.Save();
                return RedirectToAction("GetCourses");
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
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");
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

        [HttpGet]
        public IActionResult EditTeacher(int id)
        {
            Teacher teacher = unitOfWork.Teacher.GetById(id);
            var courses = unitOfWork.Course.GetAll();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");
            return View(teacher);
        }

        [HttpPost]
        public IActionResult EditTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                Teacher teacherInDb = unitOfWork.Teacher.GetById(teacher.TeacherId);
                if (teacherInDb != null)
                {
                    teacherInDb.FirstName = teacher.FirstName;
                    teacherInDb.LastName = teacher.LastName;
                    teacherInDb.Salary = teacher.Salary;
                    teacherInDb.Course = teacher.Course;
                    unitOfWork.Teacher.Update(teacherInDb);
                    unitOfWork.Save();
                    return RedirectToAction("GetTeachers", "Admin");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteTeacher(int id)
        {
            Teacher teacher = unitOfWork.Teacher.GetById(id);
            if (teacher != null)
            {
                unitOfWork.Teacher.Remove(teacher);
                unitOfWork.Save();
                return RedirectToAction("GetTeachers");
            }
            return View();
        }
    }
}
