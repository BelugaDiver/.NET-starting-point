using SingletonRepository.DataLayer.Model;
using System.Diagnostics.CodeAnalysis;

namespace SingletonRepository.Tests
{
    [ExcludeFromCodeCoverage]
    internal class AssertStudent
    {
        public static void AreEquivalent(Student expected, Student actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Age, actual.Age);

            var comparisonPairs = expected.Courses.Zip(actual.Courses, (expectedCourse, actualCourse) => (expectedCourse, actualCourse) );

            foreach (var pair in comparisonPairs)
            {
                Assert.AreEqual(pair.expectedCourse, pair.actualCourse);
            }
        }
    }
}
