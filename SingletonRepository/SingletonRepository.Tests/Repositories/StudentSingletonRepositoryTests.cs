using SingletonRepository.DataLayer.Repositories;
using SingletonRepository.DataLayer.Model;

namespace SingletonRepository.Tests.Repositories
{
    [TestClass]
    public class StudentSingleRepositortyTests
    {
        [TestMethod]
        public void GetSingleton_ReturnsEmptyListOfStudents()
        {
            var repo = StudentSingletonRepository.GetSingleton(null);

            Assert.AreEqual(0, repo.Get(new Student()).Count());
        }

        [TestMethod]
        public void GetSingleton_ReturnsExpectedListOfStudents()
        {
            var expectedListOfStudents = new[]
            {
                new Student { Id = "student-id1" },
                new Student { Id = "student-id2" },
                new Student { Id = "student-id3" }
            };

            var repo = StudentSingletonRepository.GetSingleton(expectedListOfStudents);

            var actualListOfStudents = repo.Get(new Student());
            Assert.AreEqual(3, actualListOfStudents.Count());

            var comparisonPairs = actualListOfStudents.Zip(expectedListOfStudents);
            foreach (var (expected, actual) in comparisonPairs)
            {
                AssertStudent.AreEquivalent(expected, actual);
            }
        }

        [TestMethod]
        public void GetSingleton_WhenCalledTwice_ReturnsSameListOfStudents()
        {
            var expectedListOfStudents = new[]
            {
                new Student { Id = "student-id1" },
                new Student { Id = "student-id2" },
                new Student { Id = "student-id3" }
            };

            StudentSingletonRepository.GetSingleton(expectedListOfStudents);
            var repo = StudentSingletonRepository.GetSingleton(null);

            var actualListOfStudents = repo.Get(new Student());
            Assert.AreEqual(3, actualListOfStudents.Count());

            var comparisonPairs = actualListOfStudents.Zip(expectedListOfStudents);
            foreach (var pair in comparisonPairs)
            {
                AssertStudent.AreEquivalent(pair.First, pair.Second);
            }
        }

        [TestMethod]
        public void Add_Adds_New_Student()
        {
            var student = new Student
            {
                Id = "student-id",
                FirstName = "first-name",
                LastName = "last-name",
                Email = "email",
                Age = 20
            };

            var repo = StudentSingletonRepository.GetSingleton();
            var actualStudent = repo.Add(student);

            AssertStudent.AreEquivalent(student, actualStudent);
        }

        [TestMethod]
        public void Get_ById_GetsAStudent()
        {
            var expectedStudent = new Student
            {
                Id = "student-id",
                FirstName = "first-name",
                LastName = "last-name",
                Email = "email",
                Age = 20
            };

            var repo = StudentSingletonRepository.GetSingleton();
            repo.Add(expectedStudent);
            var actualStudent = repo.Get("student-id");

            AssertStudent.AreEquivalent(expectedStudent, actualStudent);
        }

        [TestMethod]
        public void Get_ById_WhenNotFound_ThrowsException()
        {
            var repo = StudentSingletonRepository.GetSingleton();
            Assert.ThrowsException<ArgumentException>(() => repo.Get("invalid-id"));
        }

        [TestMethod]
        public void Get_GetsAListOfStudents()
        {
            var expectedListOfStudents = new[]
            {
                new Student { Id = "student-id1" },
                new Student { Id = "student-id2" },
                new Student { Id = "student-id3" }
            };

            var repo = StudentSingletonRepository.GetSingleton(expectedListOfStudents);
            var actualListOfStudents = repo.Get(null as Student);
            Assert.AreEqual(3, actualListOfStudents.Count());

            var comparisonPairs = actualListOfStudents.Zip(expectedListOfStudents);
            foreach (var pair in comparisonPairs)
            {
                AssertStudent.AreEquivalent(pair.First, pair.Second);
            }
        }

        [TestMethod]
        public void Get_GetsAFilteredOfStudents()
        {
            var expectedListOfStudents = new[]
            {
                new Student { Id = "student-id1", FirstName = "First" },
                new Student { Id = "student-id2", FirstName = "First" },
                new Student { Id = "student-id3", FirstName = "NotFirst" }
            };

            var repo = StudentSingletonRepository.GetSingleton(expectedListOfStudents);
            var actualListOfStudents = repo.Get(new Student { FirstName = "Fir" });
            Assert.AreEqual(2, actualListOfStudents.Count());

            var comparisonPairs = actualListOfStudents.Zip(expectedListOfStudents.Take(2));
            foreach (var pair in comparisonPairs)
            {
                AssertStudent.AreEquivalent(pair.First, pair.Second);
            }
        }

        [TestMethod]
        public void GetByEmail_GetsAStudent()
        {
            var expectedStudent = new Student
            {
                Id = "student-id",
                FirstName = "first-name",
                LastName = "last-name",
                Email = "email",
                Age = 20
            };

            var repo = StudentSingletonRepository.GetSingleton();
            repo.Add(expectedStudent);
            var actualStudent = repo.GetByEmail("email");

            AssertStudent.AreEquivalent(expectedStudent, actualStudent);
        }

        [TestMethod]
        public void GetByEmail_WhenNotFound_ThrowsException()
        {
            var repo = StudentSingletonRepository.GetSingleton();
            Assert.ThrowsException<ArgumentException>(() => repo.GetByEmail("invalid-email"));
        }

        [TestMethod]
        public void Update_Updates_Existing_Student()
        {
            var student = new Student
            {
                Id = "student-id",
                FirstName = "first-name",
                LastName = "last-name",
                Email = "email",
                Age = 20
            };

            var repo = StudentSingletonRepository.GetSingleton();
            repo.Add(student);

            student.FirstName = "updated-first-name";

            var actualStudent = repo.Update("student-id", student);

            AssertStudent.AreEquivalent(student, actualStudent);
        }

        [TestMethod]
        public void Remove_Removes_Existing_Student()
        {
            var student = new Student
            {
                Id = "student-id",
                FirstName = "first-name",
                LastName = "last-name",
                Email = "email",
                Age = 20
            };

            var repo = StudentSingletonRepository.GetSingleton();
            repo.Add(student);

            var actualStudent = repo.Remove("student-id");

            Assert.ThrowsException<ArgumentException>(() => repo.Get("student-id"));
        }

        [TestMethod]
        public void Register_Registers_A_Student_ForCourses()
        {
            var student = new Student
            {
                Id = "register-student-id",
                FirstName = "first-name",
                LastName = "last-name",
                Email = "email",
                Age = 20
            };

            var repo = StudentSingletonRepository.GetSingleton();
            repo.Add(student);

            var expectedCourse = "course-id";

            var actualStudent = repo.Register("register-student-id", expectedCourse);

            Assert.IsTrue(actualStudent.Courses.Contains(expectedCourse));
        }

        [TestMethod]
        public void Drop_Drops_A_Student_FromCourses()
        {
            var student = new Student
            {
                Id = "drop-student-id",
                FirstName = "first-name",
                LastName = "last-name",
                Email = "email",
                Age = 20
            };

            var repo = StudentSingletonRepository.GetSingleton();
            repo.Add(student);

            var expectedCourse = "course-id";

            repo.Register("drop-student-id", expectedCourse);
            var actualStudent = repo.Drop("drop-student-id", expectedCourse);

            Assert.IsFalse(actualStudent.Courses.Contains(expectedCourse));
        }
    }
}