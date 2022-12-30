namespace SingletonRepository.DataLayer.Model
{
    /// <summary>
    /// The Student class represents a student that is registered for courses.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Id of the student.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// First name of the student.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the student.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email of the student.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Age of the student.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// List of courses the student is currently enrolled in.
        /// </summary>
        public ISet<string> Courses { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Student()
        {
            Courses = new HashSet<string>();
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="id">Id of the student.</param>
        /// <param name="firstName">First name of the student.</param>
        /// <param name="lastName">Last name of the student.</param>
        /// <param name="email">Email of the student.</param>
        /// <param name="age">Age of the student.</param>
        /// <param name="courses">List of courses the student is registered in.</param>
        public Student(string id, string firstName, string lastName, string email, int age, IEnumerable<string> courses)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Age = age;
            Courses = courses.ToHashSet();
        }

        /// <summary>
        /// Registers the student to attend courses.
        /// </summary>
        /// <param name="courseIds">Comma-separated list of course ids the student will be registered for.</param>
        public Student RegisterCourses(params string[] courseIds)
        {
            Courses.UnionWith(courseIds);
            return this;
        }

        /// <summary>
        /// Drops a course the student is registered in.
        /// </summary>
        /// <param name="courseId">Course id to be dropped.</param>
        public Student DropCourse(string courseId)
        {
            if (Courses.Contains(courseId))
            {
                Courses.Remove(courseId);
            }

            return this;
        }
    }
}
