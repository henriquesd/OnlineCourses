using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourses.Domain.Courses
{
    public class CourseStore
    {
        private readonly ICourseRepository _courseRepository;
        public CourseStore(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void Store(CourseDto courseDto)
        {
            var courseAlreadySave = _courseRepository.GetByName(courseDto.Name);

            if (courseAlreadySave != null)
                throw new ArgumentException("Course's name already exists on database");

            if (!Enum.TryParse<TargetAudience>(courseDto.TargetAudience, out var targetAudience))
                throw new ArgumentException("Invalid Target Audience");

            var course = new Course(courseDto.Name,
                                    courseDto.Description,
                                    courseDto.Workload,
                                    (TargetAudience)targetAudience,
                                    courseDto.Price);

            _courseRepository.Add(course);
        }
    }
}
