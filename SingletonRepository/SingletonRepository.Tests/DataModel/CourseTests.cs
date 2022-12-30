using SingletonRepository.DataLayer.Model;

namespace SingletonRepository.Tests.DataModel
{
    [TestClass]
    public class CourseTests
    {
        [TestMethod]
        public void ctor_Creates_New_Student()
        {
            var id = "course-id";
            var name = "course-name";

            var expectedCourse = new Course { Id = id, CourseName = name };

            Assert.AreEqual(expectedCourse.Id, id);
            Assert.AreEqual(expectedCourse.CourseName, name);
        }
    }
}