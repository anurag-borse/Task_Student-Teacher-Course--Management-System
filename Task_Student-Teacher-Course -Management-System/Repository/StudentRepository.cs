using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task_Student_Teacher_Course__Management_System.Data;
using Task_Student_Teacher_Course__Management_System.Models;
using Task_Student_Teacher_Course__Management_System.Repository.IRepository;

namespace Task_Student_Teacher_Course__Management_System.Repository
{
	public class StudentRepository : Repository<Student>, IStudentRepository
	{
		public readonly ApplicationDbContext _db;

		public StudentRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

        public Student GetById(int studentid)
        {
			var student = _db.Students.Find(studentid);
			return student;
        }

        public void Update(Student obj)
		{
			_db.Students.Update(obj);
		}
        public IQueryable<Student> GetAllWithIncludes(params Expression<Func<Student, object>>[] includeProperties)
        {
            IQueryable<Student> query = _db.Students;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }
    }
	
}
