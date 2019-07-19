using Bogus;
using ExpectedObjects;
using OnlineCourses.Domain.Courses;
using OnlineCourses.DomainTest._Builders;
using OnlineCourses.DomainTest._Util;
using System;
using Xunit;
using Xunit.Abstractions;

namespace OnlineCourses.DomainTest.Courses
{
    public class CourseTest : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _name;
        private readonly double _workload;
        private readonly TargetAudience _targetAudience;
        private readonly double _price;
        private readonly string _description;

        // The constructor is always executed before each test method being executed (SetUp);
        public CourseTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Constructor being executed");
            var faker = new Faker();

            _name = faker.Random.Word();
            _workload = faker.Random.Double(50, 1000);
            _targetAudience = TargetAudience.Student;
            _price = faker.Random.Double(100, 1000);
            _description = faker.Lorem.Paragraph();
        }

        // Dispose is always executed after each test method being executed (CleanUp);
        public void Dispose()
        {
            _output.WriteLine("Dispose being executed");
        }

        [Fact]
        public void ShouldCreateCourse()
        {
            var expectedCourse = new
            {
                Name = _name,
                Workload = _workload,
                TargetAudience = _targetAudience,
                Price = _price,
                Description = _description
            };

            var course = new Course(expectedCourse.Name,
                                    expectedCourse.Description,
                                    expectedCourse.Workload,
                                    expectedCourse.TargetAudience,
                                    expectedCourse.Price);

            expectedCourse.ToExpectedObject().ShouldMatch(course);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Course_ShouldNotHave_AnInvalidName(string invalidName)
        {
            Assert.Throws<ArgumentException>(() =>
               CourseBuilder.New().WithName(invalidName).Build())
                .WithMessage("Invalid name");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Course_ShouldNotHave_WorkloadLowerThanOne(double invalidWorkload)
        {
            Assert.Throws<ArgumentException>(() =>
                CourseBuilder.New().WithWorkload(invalidWorkload).Build())
                .WithMessage("Invalid workload");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Course_ShouldNotHave_PriceLowerThanOne(double invalidPrice)
        {
            Assert.Throws<ArgumentException>(() =>
                CourseBuilder.New().WithPrice(invalidPrice).Build())
                .WithMessage("Invalid price");
        }
    }
}