using Xunit;

namespace OnlineCourses.DomainTest.Courses
{
    public class CourseTest
    {
        [Fact]
        public void ShouldCreateCourse()
        {
            const string name = "Basic Computing";
            const double workload = 80;
            const string targetAudience = "Students";
            const double price = 950;

            var course = new Course(name, workload, targetAudience, price);

            Assert.Equal(name, course.Name);
            Assert.Equal(workload, course.Workload);
            Assert.Equal(targetAudience, course.TargetAudience);
            Assert.Equal(price, course.Price);
        }
    }

    public class Course
    {
        public Course(string name, double workload, string targetAudience, double price)
        {
            Name = name;
            Workload = workload;
            TargetAudience = targetAudience;
            Price = price;
        }

        public string Name { get; private set; }
        public double Workload { get; private set; }
        public string TargetAudience { get; private set; }
        public double Price { get; private set; }
    }
}