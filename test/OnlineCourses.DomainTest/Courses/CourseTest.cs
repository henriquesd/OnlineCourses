using ExpectedObjects;
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

        // The constructor is always executed before each test method being executed (SetUp);
        public CourseTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Constructor being executed");

            _name = "Basic Computing";
            _workload = 80;
            _targetAudience = TargetAudience.Student;
            _price = 950;
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
                Price = _price
            };

            var course = new Course(expectedCourse.Name,
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
                new Course(invalidName,
                            _workload,
                            _targetAudience,
                            _price))
                .WithMessage("Invalid name");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Course_ShouldNotHave_WorkloadLowerThanOne(double invalidWorkload)
        {
            Assert.Throws<ArgumentException>(() =>
                new Course(_name,
                            invalidWorkload,
                            _targetAudience,
                            _price))
                .WithMessage("Invalid workload");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Course_ShouldNotHave_PriceLowerThanOne(double invalidPrice)
        {
            var expectedCourse = new
            {
                Name = "Basic Computing",
                Workload = (double)80,
                TargetAudience = TargetAudience.Student,
                Price = (double)950
            };

            Assert.Throws<ArgumentException>(() =>
                new Course(_name,
                            _workload,
                            _targetAudience,
                            invalidPrice))
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
        public Course(string name, double workload, TargetAudience targetAudience, double price)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Invalid name");

            if (workload < 1)
                throw new ArgumentException("Invalid workload");

            if (price < 1)
                throw new ArgumentException("Invalid price");

            Name = name;
            Workload = workload;
            TargetAudience = targetAudience;
            Price = price;
        }

        public string Name { get; private set; }
        public double Workload { get; private set; }
        public TargetAudience TargetAudience { get; private set; }
        public double Price { get; private set; }
    }
}