using SingletonRepository.DataLayer.Model;
using SingletonRepository.DataLayer.Repositories;

namespace SingletonRepository.Tests.Repositories
{
    [TestClass]
    public class CourseSingletonRepositoryTests
    {
        [TestMethod]
        public void GetSingleton_ReturnsEmptyListOfCourses()
        {
            var repo = CourseSingletonRepository.GetSingleton(null);

            Assert.AreEqual(0, repo.Get(new Course()).Count());
        }

        [TestMethod]
        public void GetSingleton_ReturnsExpectedListOfStudents()
        {
            var expectedListOfCourses = new[]
            {
                new Course { Id = "course-id1" },
                new Course { Id = "course-id2" },
                new Course { Id = "course-id3" }
            };

            var repo = CourseSingletonRepository.GetSingleton(expectedListOfCourses);

            var actualListOfCourses = repo.Get(null as Course);
            Assert.AreEqual(3, actualListOfCourses.Count());

            var comparisonPairs = actualListOfCourses.Zip(expectedListOfCourses);
            foreach (var (expected, actual) in comparisonPairs)
            {
                AssertCourse.AreEquivalent(expected, actual);
            }
        }

        [TestMethod]
        public void GetSingleton_ReturnsSameListOfStudents()
        {
            var expectedListOfCourses = new[]
            {
                new Course { Id = "course-id1" },
                new Course { Id = "course-id2" },
                new Course { Id = "course-id3" }
            };

            CourseSingletonRepository.GetSingleton(expectedListOfCourses);
            var repo = CourseSingletonRepository.GetSingleton(null);

            var actualListOfCourses = repo.Get(new Course());
            Assert.AreEqual(3, actualListOfCourses.Count());

            var comparisonPairs = actualListOfCourses.Zip(expectedListOfCourses);
            foreach (var (expected, actual) in comparisonPairs)
            {
                AssertCourse.AreEquivalent(expected, actual);
            }
        }

        [TestMethod]
        public void Add_Adds_New_Course()
        {
            var expectedCourse = new Course
            {
                Id = "course-id",
                CourseName = "course-name"
            };

            var repo = CourseSingletonRepository.GetSingleton();
            var actualCourse = repo.Add(expectedCourse);

            AssertCourse.AreEquivalent(expectedCourse, actualCourse);
        }

        [TestMethod]
        public void Get_GetsSingleCourse()
        {
            var expectedCourse = new Course
            {
                Id = "course-id",
                CourseName = "course-name"
            };

            var repo = CourseSingletonRepository.GetSingleton();
            repo.Add(expectedCourse);

            var actualCourse = repo.Get(expectedCourse.Id);

            AssertCourse.AreEquivalent(expectedCourse, actualCourse);
        }

        [TestMethod]
        public void Get_ById_WhenNotFound_ThrowsException()
        {
            var repo = CourseSingletonRepository.GetSingleton();
            Assert.ThrowsException<ArgumentException>(() => repo.Get("invalid-id"));
        }

        [TestMethod]
        public void Get_GetsFilteredListOfCourses()
        {
            var expectedListOfCourses = new[]
            {
                new Course { Id = "course-id1", CourseName = "First" },
                new Course { Id = "course-id2", CourseName = "First" },
                new Course { Id = "course-id3", CourseName = "NotFirst" }
            };

            var repo = CourseSingletonRepository.GetSingleton(expectedListOfCourses);
            var actualListOfCourses = repo.Get(new Course { CourseName = "Fir" });
            Assert.AreEqual(2, actualListOfCourses.Count());

            var comparisonPairs = actualListOfCourses.Zip(expectedListOfCourses.Take(2));
            foreach (var pair in comparisonPairs)
            {
                AssertCourse.AreEquivalent(pair.First, pair.Second);
            }
        }

        [TestMethod]
        public void Update_Updates_Existing_Student()
        {
            var course = new Course
            {
                Id = "course-id-update",
                CourseName = "course-name"
            };

            var repo = CourseSingletonRepository.GetSingleton();
            repo.Add(course);

            course.CourseName = "updated-course-name";

            var actualCourse = repo.Update(course.Id, course);

            AssertCourse.AreEquivalent(course, actualCourse);
        }

        [TestMethod]
        public void Remove_Removes_Existing_Student()
        {
            var course = new Course
            {
                Id = "course-id-remove",
                CourseName = "course-name"
            };

            var repo = CourseSingletonRepository.GetSingleton();
            repo.Add(course);

            var actualStudent = repo.Remove(course.Id);

            Assert.ThrowsException<ArgumentException>(() => repo.Get(course.Id));
        }
    }
}
