using SingletonRepository.DataLayer.Model;

namespace SingletonRepository.Tests.DataModel
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void ctor_Creates_New_Student()
        {
            var id = "student-id";
            var firstName = "first-name";
            var lastName = "last-name";
            var email = "email";
            var age = 20;

            var expectedStudent = new Student
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Age = age
            };

            Assert.AreEqual(expectedStudent.Id, id);
            Assert.AreEqual(expectedStudent.FirstName, firstName);
            Assert.AreEqual(expectedStudent.LastName, lastName);
            Assert.AreEqual(expectedStudent.Email, email);
            Assert.AreEqual(expectedStudent.Age, age);
        }

        [TestMethod]
        public void ctor_Creates_New_Student_WithCourses()
        {
            var id = "student-id";
            var firstName = "first-name";
            var lastName = "last-name";
            var email = "email";
            var age = 20;
            var courses = new List<string> { "course-1", "course-2", "course-3" };

            var expectedStudent = new Student(id, firstName, lastName, email, age, courses);

            Assert.AreEqual(expectedStudent.Id, id);
            Assert.AreEqual(expectedStudent.FirstName, firstName);
            Assert.AreEqual(expectedStudent.LastName, lastName);
            Assert.AreEqual(expectedStudent.Email, email);
            Assert.AreEqual(expectedStudent.Age, age);
            
            var comparisonPairs = expectedStudent.Courses.Zip(courses);

            foreach (var pair in comparisonPairs)
            {
                Assert.AreEqual(pair.First, pair.Second);
            }
        }

        [TestMethod]
        public void RegisterCourses_RegistersAStudent()
        {
            var expectedCourse = "expected-course";
            var student = new Student { Id = "id", FirstName = "John", LastName = "Doe", Email = "JohnDoe@institution.edu", Age = 20 };

            student.RegisterCourses(expectedCourse);

            Assert.IsTrue(student.Courses.Contains(expectedCourse));
        }

        [TestMethod]
        public void RegisterCourses_WhenStudentIsAlreadyRegistered_DoesNothing()
        {
            var expectedCourse = "expected-course";
            var student = new Student
            {
                Id = "id",
                FirstName = "John",
                LastName = "Doe",
                Email = "JohnDoe@institution.edu",
                Age = 20,
                Courses = new HashSet<string> { expectedCourse, "course-2" }
            };

            student.RegisterCourses(expectedCourse);

            Assert.AreEqual(2, student.Courses.Count);
            Assert.IsTrue(student.Courses.Contains(expectedCourse));
        }

        [TestMethod]
        public void RegisterCourses_WithMultipleCourses_RegistersAStudent()
        {
            var expectedCourse1 = "expected-course-1";
            var expectedCourse2 = "expected-course-2";
            var expectedCourse3 = "expected-course-3";

            var student = new Student { Id = "id", FirstName = "John", LastName = "Doe", Email = "JohnDoe@institution.edu", Age = 20 };

            student.RegisterCourses(expectedCourse1, expectedCourse2, expectedCourse3);

            Assert.AreEqual(3, student.Courses.Count);
            Assert.IsTrue(student.Courses.Contains(expectedCourse1));
            Assert.IsTrue(student.Courses.Contains(expectedCourse2));
            Assert.IsTrue(student.Courses.Contains(expectedCourse3));
        }

        [TestMethod]
        public void DropCourse_DropsACourse()
        {
            var expectedCourse = "expected-course";
            var student = new Student
            {
                Id = "id",
                FirstName = "John",
                LastName = "Doe",
                Email = "JohnDoe@institution.edu",
                Age = 20,
                Courses = new HashSet<string> { expectedCourse, "course-2" }
            };

            student.DropCourse(expectedCourse);

            Assert.AreEqual(1, student.Courses.Count);
            Assert.IsFalse(student.Courses.Contains(expectedCourse));
        }

        [TestMethod]
        public void DropCourse_WhenStudentIsNotRegistered_DoesNothing()
        {
            var expectedCourse = "expected-course";
            var student = new Student
            {
                Id = "id",
                FirstName = "John",
                LastName = "Doe",
                Email = "JohnDoe@institution.edu",
                Age = 20,
                Courses = new HashSet<string> { "course-2" }
            };

            student.DropCourse(expectedCourse);

            Assert.AreEqual(1, student.Courses.Count);
            Assert.IsFalse(student.Courses.Contains(expectedCourse));
        }
    }
}
