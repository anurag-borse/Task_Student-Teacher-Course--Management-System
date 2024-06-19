using Task_Student_Teacher_Course__Management_System.Repository;
using Task_Student_Teacher_Course__Management_System.Data;
using Task_Student_Teacher_Course__Management_System.Models;
using Task_Student_Teacher_Course__Management_System.Repository.IRepository;

namespace Task_Student_Teacher_Course__Management_System.Repository
{
	internal class AdminRepository : Repository<Admin>, IAdminRepository
	{
		public readonly ApplicationDbContext _db;

		public AdminRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public Admin GetByUsername(string username)
		{
			return _db.Admins.FirstOrDefault(u => u.AdminEmail == username);
			
		}

		public void Update(Admin obj)
		{
			_db.Admins.Update(obj);
		}
	}

}
