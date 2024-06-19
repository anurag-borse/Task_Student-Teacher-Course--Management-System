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



		public void Update(Student obj)
		{
			_db.Students.Update(obj);
		}
	}
	
}
