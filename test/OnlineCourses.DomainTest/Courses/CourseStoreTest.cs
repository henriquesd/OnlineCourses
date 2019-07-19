using Bogus;
using Moq;
using OnlineCourses.Domain.Courses;
using OnlineCourses.DomainTest._Util;
using System;
using Xunit;

namespace OnlineCourses.DomainTest.Courses
{
    public class CourseStoreTest
    {
        private readonly CourseDto _courseDto;
        private readonly Mock<ICourseRepository> _courseRepositoryMock;
        private readonly CourseStore _courseStore;

        public CourseStoreTest()
        {
            var fake = new Faker();
            _courseDto = new CourseDto
            {
                Name = fake.Random.Words(),
                Description = fake.Lorem.Paragraph(),
                Workload = fake.Random.Double(50, 1000),
                TargetAudience = "Student",
                Price = fake.Random.Double(1000, 2000)
            };

            _courseRepositoryMock = new Mock<ICourseRepository>();
            _courseStore = new CourseStore(_courseRepositoryMock.Object);
        }

        [Fact]
        public void Should_Add_Course()
        {
            _courseStore.Store(_courseDto);

            _courseRepositoryMock.Verify(r => r.Add(
                It.Is<Course>(
                    c => c.Name == _courseDto.Name &&
                    c.Description == _courseDto.Description
                )
            ));
        }

        [Fact]
        public void ShouldNot_Inform_AnInvalidTargetAudience()
        {
            var invalidTargetAudience = "Doctor";
            _courseDto.TargetAudience = invalidTargetAudience;

            Assert.Throws<ArgumentException>(() => _courseStore.Store(_courseDto))
                .WithMessage("Invalid Target Audience");
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
            Enum.TryParse(typeof(TargetAudience), courseDto.TargetAudience, out var targetAudience);

            if (targetAudience == null)
                throw new ArgumentException("Invalid Target Audience");

            var course = new Course(courseDto.Name,
                                    courseDto.Description,
                                    courseDto.Workload,
                                    (TargetAudience)targetAudience,
                                    courseDto.Price);

            _courseRepository.Add(course);
        }
    }

    public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Workload { get; set; }
        public string TargetAudience { get; set; }
        public double Price { get; set; }
    }
}
