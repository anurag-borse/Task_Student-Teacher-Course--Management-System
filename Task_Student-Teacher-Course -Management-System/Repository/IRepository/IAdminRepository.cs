using Task_Student_Teacher_Course__Management_System.Repository.IRepository;
using Task_Student_Teacher_Course__Management_System.Models;

namespace Task_Student_Teacher_Course__Management_System.Repository.IRepository
{
	public interface IAdminRepository : IRepository<Admin>
	{
		Admin GetByUsername(string username);
		void Update(Admin obj);

	}
}
