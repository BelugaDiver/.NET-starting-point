using SingletonRepository.DataLayer.Model;

namespace SingletonRepository.DataLayer.Interfaces
{
    /// <summary>
    /// The ICourseRepository describes a component capable of managing and delivering courses.
    /// </summary>
    public interface ICourseRepository : IObjectRepository<Course>
    {
    }
}
