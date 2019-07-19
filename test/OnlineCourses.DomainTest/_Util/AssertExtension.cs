using System;
using Xunit;

namespace OnlineCourses.DomainTest._Util
{
    public static class AssertExtension
    {
        public static void WithMessage(this ArgumentException exception, string message)
        {
            if (exception.Message == message)
                Assert.True(true);
            else
                Assert.False(true, $"Expected the message '{message}'");
        }
    }
}
