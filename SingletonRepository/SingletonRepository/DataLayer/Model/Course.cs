namespace SingletonRepository.DataLayer.Model
{
    /// <summary>
    /// The Course class represents a course a student is registered in.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Id for the course.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the course.
        /// </summary>
        public string CourseName { get; set; }
    }
}
