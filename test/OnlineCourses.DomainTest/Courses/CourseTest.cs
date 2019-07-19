using ExpectedObjects;
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

            _name = "Basic Computing";
            _workload = 80;
            _targetAudience = TargetAudience.Student;
            _price = 950;
            _description = "A description";
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
                Name = "Basic Computing",
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

    public enum TargetAudience
    {
        Student,
        CollegeStudent,
        Employee,
        Employer
    }

    public class Course
    {
        public Course(string name, string description, double workload, TargetAudience targetAudience, double price)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid name");

            if (workload < 1)
                throw new ArgumentException("Invalid workload");

            if (price < 1)
                throw new ArgumentException("Invalid price");

            Name = name;
            Description = description;
            Workload = workload;
            TargetAudience = targetAudience;
            Price = price;
        }

        public string Name { get; private set; }
        public string Description { get; set; }
        public double Workload { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public double Price { get; private set; }
    }
}