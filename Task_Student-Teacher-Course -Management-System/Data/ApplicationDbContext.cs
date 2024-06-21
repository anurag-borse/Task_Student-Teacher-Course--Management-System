


using Microsoft.EntityFrameworkCore;
using Task_Student_Teacher_Course__Management_System.Models;

namespace Task_Student_Teacher_Course__Management_System.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Admin> Admins { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Admin>().HasData(
				new Admin
				{
					AdminId = 1,
					AdminName = "Admin",
					AdminEmail = "admin@gmail.com",
					AdminPassword = "admin"

				}
			);



			modelBuilder.Entity<Course>().HasData(
				new Course
				{
					CourseId = 1,
					CourseName = "C#",
					TeacherName = "Rahul"
				}


			);


			modelBuilder.Entity<Teacher>().HasData(
				new Teacher
				{
					TeacherId = 1,
					FirstName = "Rahul",
					LastName = "Jadhav",
					Course = "C#",
					Salary = 50000

				}
			);


            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();



        }



	}
}
