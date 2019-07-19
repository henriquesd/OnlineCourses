using Moq;
using OnlineCourses.Domain.Courses;
using Xunit;

namespace OnlineCourses.DomainTest.Courses
{
    public class CourseStoreTest
    {
        [Fact]
        public void Should_Add_Course()
        {
            var courseDto = new CourseDto
            {
                Name = "Course A",
                Description = "Description",
                Workload = 80,
                TargetAudience = 1,
                Price = 8
            };

            var courseRepositoryMock = new Mock<ICourseRepository>();
            
            var courseStore = new CourseStore(courseRepositoryMock.Object);

            courseStore.Store(courseDto);

            courseRepositoryMock.Verify(r => r.Add(It.IsAny<Course>()));
        }
    }

    public interface ICourseRepository
    {
        void Add(Course course);
    }

    public class CourseStore
    {
        private readonly ICourseRepository _courseRepository;
        public CourseStore(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Store(CourseDto courseDto)
        {
            var course = new Course(courseDto.Name,
                                    courseDto.Description,
                                    courseDto.Workload,
                                    TargetAudience.Student,
                                    courseDto.Price);

            _courseRepository.Add(course);
        }
    }

    public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Workload { get; set; }
        public int TargetAudience { get; set; }
        public double Price { get; set; }
    }
}
