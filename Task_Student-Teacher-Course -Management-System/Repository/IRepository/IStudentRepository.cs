using Task_Student_Teacher_Course__Management_System.Repository.IRepository;
using Task_Student_Teacher_Course__Management_System.Models;

namespace Task_Student_Teacher_Course__Management_System.Repository.IRepository
{
	public interface IStudentRepository : IRepository<Student>
	{
		void Update(Student student);
	}
}
