namespace Task_Student_Teacher_Course__Management_System.Repository.IRepository
{
    public interface IUnitOfWork
    {


        IAdminRepository Admin { get; set; }


        ITeacherRepository Teacher { get; set; }

        IStudentRepository Student { get; set; }

        ICourseRepository Course { get; set; }

        

        

        void Save();
    }
}
