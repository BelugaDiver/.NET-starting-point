using SingletonRepository.DataLayer.Model;
using System.Diagnostics.CodeAnalysis;

namespace SingletonRepository.Tests
{
    [ExcludeFromCodeCoverage]
    internal class AssertCourse
    {
        public static void AreEquivalent(Course expected, Course actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.CourseName, actual.CourseName);
        }
    }
}
