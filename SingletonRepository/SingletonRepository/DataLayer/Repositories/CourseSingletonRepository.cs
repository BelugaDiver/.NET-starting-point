using SingletonRepository.DataLayer.Model;
using SingletonRepository.DataLayer.Interfaces;

namespace SingletonRepository.DataLayer.Repositories
{
    /// <summary>
    /// The CourseSingletonRepository manages and delivers courses inside of a singleton in-memory repository.
    /// </summary>
    public class CourseSingletonRepository : ICourseRepository
    {
        private static CourseSingletonRepository _instance;
        protected List<Course> _courses;

        /// <summary>
        /// _lock object used to synchronize multiple threads accessing the singleton instance in a 
        /// multi-threaded environment. Thread-safe singleton implementation.
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// Private Constructor to prevent it from being called.
        /// </summary>
        private CourseSingletonRepository() { }

        /// <summary>
        /// Initialize a new singleton or get the current singleton object.
        /// </summary>
        /// <param name="courses">List of students to initialize the data store.</param>
        /// <returns>The singleton student.</returns>
        public static CourseSingletonRepository GetSingleton(IEnumerable<Course> courses = null)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CourseSingletonRepository();
                        _instance._courses = courses?.ToList() ?? new List<Course>();
                    }
                }
            }

            return _instance;
        }

        public Course Add(Course entity)
        {
            if (!_courses.Any(x => x.Id == entity.Id))
            {
                _courses = _courses.Append(entity).ToList();
            }

            return entity;
        }

        public Course Get(string id)
        {
            var result = _courses.FirstOrDefault(x => x.Id == id);

            return result ?? throw new ArgumentException($"Invalid Id: {id}");
        }

        public IEnumerable<Course> Get(Course entity)
        {
            IEnumerable<Course> result = _courses;

            if (!string.IsNullOrWhiteSpace(entity?.Id))
            {
                result = result.Where(x => x.Id == entity.Id);
            }

            if (!string.IsNullOrWhiteSpace(entity?.CourseName))
            {
                result = result.Where(x => x.CourseName.StartsWith(entity.CourseName));
            }

            return result;
        }

        public Course Update(string id, Course entity)
        {
            Remove(id);
            _courses = _courses.Append(entity).ToList();

            return Get(id);
        }

        public Course Remove(string id)
        {
            var student = Get(id);
            _courses.Remove(student);
            return student;
        }
    }
}
