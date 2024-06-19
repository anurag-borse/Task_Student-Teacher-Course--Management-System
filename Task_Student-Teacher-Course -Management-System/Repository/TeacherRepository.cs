using Task_Student_Teacher_Course__Management_System.Data;
using Task_Student_Teacher_Course__Management_System.Models;
using Task_Student_Teacher_Course__Management_System.Repository.IRepository;

namespace Task_Student_Teacher_Course__Management_System.Repository
{
	public class TeacherRepository : Repository<Teacher>, ITeacherRepository
	{

		public readonly ApplicationDbContext _db;
		public TeacherRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Teacher obj)
		{
			_db.Teachers.Update(obj);
		}
	}
}
