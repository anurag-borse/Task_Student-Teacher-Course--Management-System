
using Task_Student_Teacher_Course__Management_System.Repository.IRepository;
using System.Reflection;
using Task_Student_Teacher_Course__Management_System.Data;
using Task_Student_Teacher_Course__Management_System.Repository;

namespace HRMS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IAdminRepository Admin { get; set; }

        public ITeacherRepository Teacher { get; set; }

        public ICourseRepository Course { get; set; }   

        public IStudentRepository Student { get; set; }
       

        public UnitOfWork(ApplicationDbContext _db)
        {
        

            Admin = new AdminRepository(_db);

            Teacher = new TeacherRepository(_db);

            Course = new CourseRepository(_db);

            Student = new StudentRepository(_db);




            this._db = _db;

        }

       

        public void Save()
        {
            _db.SaveChanges();

        }
    }
}
