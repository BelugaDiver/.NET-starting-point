using SingletonRepository.DataLayer.Model;

namespace SingletonRepository.DataLayer.Interfaces
{
    /// <summary>
    /// The IStudentRepository describes a component capable of managing and delivering students.
    /// </summary>
    public interface IStudentRepository : IObjectRepository<Student>
    {
        /// <summary>
        /// Gets a single student by their email.
        /// </summary>
        /// <param name="email">Email of the student.</param>
        /// <returns>A student that matches the email.</returns>
        Student GetByEmail(string email);

        /// <summary>
        /// Registers a student in the provided courses.
        /// </summary>
        /// <param name="id">Id of the student.</param>
        /// <param name="couseIds">Course Ids to register the student in.</param>
        /// <returns></returns>
        Student Register(string id, params string[] couseIds);

        /// <summary>
        /// Drops a student's from a list of courses.
        /// </summary>
        /// <param name="id">Id of the student.</param>
        /// <param name="couseIds">Course Ids to drop from the student.</param>
        /// <returns></returns>
        Student Drop(string id, params string[] couseIds);
    }
}
