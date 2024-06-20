using Task_Student_Teacher_Course__Management_System.Repository.IRepository;
using Task_Student_Teacher_Course__Management_System.Models;

namespace Task_Student_Teacher_Course__Management_System.Repository.IRepository
{
	public interface ICourseRepository : IRepository<Course>
	{
        Course GetById(int courseId);
        void Update(Course course);
	}
}
