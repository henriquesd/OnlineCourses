using ExpectedObjects;
using Xunit;

namespace OnlineCourses.DomainTest.Courses
{
    public class CourseTest
    {
        [Fact]
        public void ShouldCreateCourse()
        {
            var expectedCourse = new
            {
                Name = "Basic Computing",
                Workload = (double)80,
                TargetAudience = "Students",
                Price = (double)950
            };

            var course = new Course(expectedCourse.Name,
                                    expectedCourse.Workload,
                                    expectedCourse.TargetAudience,
                                    expectedCourse.Price);

            expectedCourse.ToExpectedObject().ShouldMatch(course);
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