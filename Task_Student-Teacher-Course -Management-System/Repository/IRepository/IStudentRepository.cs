using Task_Student_Teacher_Course__Management_System.Repository.IRepository;
using Task_Student_Teacher_Course__Management_System.Models;
using System.Linq.Expressions;

namespace Task_Student_Teacher_Course__Management_System.Repository.IRepository
{
	public interface IStudentRepository : IRepository<Student>
	{
        Student GetById(int studentid);
        void Update(Student student);

        IQueryable<Student> GetAllWithIncludes(params Expression<Func<Student, object>>[] includeProperties);

    }
}
