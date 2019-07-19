using ExpectedObjects;
using System;
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
                TargetAudience = TargetAudience.Student,
                Price = (double)950
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
            var expectedCourse = new
            {
                Name = "Basic Computing",
                Workload = (double)80,
                TargetAudience = TargetAudience.Student,
                Price = (double)950
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Course(invalidName,
                            expectedCourse.Workload,
                            expectedCourse.TargetAudience,
                            expectedCourse.Price))
                .Message;

            Assert.Equal("Invalid name", message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void Course_ShouldNotHave_WorkloadLowerThanOne(double invalidWorkload)
        {
            var expectedCourse = new
            {
                Name = "Basic Computing",
                Workload = (double)80,
                TargetAudience = TargetAudience.Student,
                Price = (double)950
            };

            var message = Assert.Throws<ArgumentException>(() =>
                new Course(expectedCourse.Name,
                            invalidWorkload,
                            expectedCourse.TargetAudience,
                            expectedCourse.Price))
                .Message;

            Assert.Equal("Invalid workload", message);
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

            var message = Assert.Throws<ArgumentException>(() =>
                new Course(expectedCourse.Name,
                            expectedCourse.Workload,
                            expectedCourse.TargetAudience,
                            invalidPrice))
                .Message;

            Assert.Equal("Invalid price", message);
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