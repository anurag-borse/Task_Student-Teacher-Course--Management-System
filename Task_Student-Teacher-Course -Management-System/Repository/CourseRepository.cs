using Task_Student_Teacher_Course__Management_System.Data;
using Task_Student_Teacher_Course__Management_System.Models;
using Task_Student_Teacher_Course__Management_System.Repository.IRepository;

namespace Task_Student_Teacher_Course__Management_System.Repository
{
	public class CourseRepository : Repository<Course>, ICourseRepository
	{
		public readonly ApplicationDbContext _db;

		public CourseRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

        public Course GetById(int courseId)
        {
			return _db.Courses.Find(courseId);
        }

        public void Update(Course course)
		{
			_db.Courses.Update(course);
		}
	}
	
}
