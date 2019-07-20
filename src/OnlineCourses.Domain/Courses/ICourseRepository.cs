namespace OnlineCourses.Domain.Courses
{
    public interface ICourseRepository
    {
        void Add(Course course);
        Course GetByName(string name);
    }
}
