using SingletonRepository.DataLayer.Model;

namespace SingletonRepository
{
    /// <summary>
    /// The SampleData.cs class contains sample data to be used to initialize the student and course repositories.
    /// </summary>
    public static class SampleData
    {
        public static List<Student> GetStudents()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = "student-1",
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "John.Smith@university.edu",
                    Age = 20,
                    Courses = new HashSet<string>
                    {
                        "EN 102",
                        "CS 400",
                        "BS 322"
                    }
                },
                new Student
                {
                    Id = "student-2",
                    FirstName = "Anna",
                    LastName = "Smith",
                    Email = "Anna.Smith@university.edu",
                    Age = 20,
                    Courses = new HashSet<string>
                    {
                        "EN 102",
                        "CS 400",
                        "BS 450"
                    }
                },
                new Student
                {
                    Id = "student-3",
                    FirstName = "Charles",
                    LastName = "Xavier",
                    Email = "Charles.Xavier@university.edu",
                    Age = 20,
                    Courses = new HashSet<string>
                    {
                        "MA 200",
                        "PS 230",
                    }
                }
            };
        }

        public static List<Course> GetCourses()
        {
            return new List<Course>
            {
                new Course
                {
                    Id = "EN 102",
                    CourseName = "Introduction to English"
                },
                new Course
                {
                    Id = "CS 400",
                    CourseName = "Computer Architecture"
                },
                new Course
                {
                    Id = "BS 322",
                    CourseName = "Introduction to Cost Accounting"
                }
            };
        }
    }
}
