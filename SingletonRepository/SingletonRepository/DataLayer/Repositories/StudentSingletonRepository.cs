using SingletonRepository.DataLayer.Interfaces;
using SingletonRepository.DataLayer.Model;

namespace SingletonRepository.DataLayer.Repositories
{
    /// <summary>
    /// The StudentSingletonRepository manages and delivers students inside of a singleton in-memory repository.
    /// </summary>
    public class StudentSingletonRepository : IStudentRepository
    {
        private static StudentSingletonRepository _instance;
        protected List<Student> _students;

        /// <summary>
        /// _lock object used to synchronize multiple threads accessing the singleton instance in a 
        /// multi-threaded environment. Thread-safe singleton implementation.
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// Private Constructor to prevent it from being called.
        /// </summary>
        private StudentSingletonRepository() { }

        /// <summary>
        /// Initialize a new singleton or get the current singleton object.
        /// </summary>
        /// <param name="students">List of students to initialize the data store.</param>
        /// <returns>The singleton student.</returns>
        public static StudentSingletonRepository GetSingleton(IEnumerable<Student> students = null)
        {
            if (_instance == null)
            {
                lock(_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new StudentSingletonRepository();
                        _instance._students = students?.ToList() ?? new List<Student>();
                    }
                }
            }

            return _instance;
        }

        /// <inheritdoc/>
        public Student Add(Student entity)
        {
            if (!_students.Any(x => x.Id == entity.Id))
            {
                _students = _students.Append(entity).ToList();
            }

            return entity;
        }

        /// <inheritdoc/>
        public Student Get(string id)
        {
            var result = _students.FirstOrDefault(x => x.Id == id);

            return result ?? throw new ArgumentException($"Invalid Id: {id}");
        }

        /// <inheritdoc/>
        public IEnumerable<Student> Get(Student entity)
        {
            IEnumerable<Student> result = _students;

            if (!string.IsNullOrWhiteSpace(entity?.Id))
            {
                result = result.Where(x => x.Id == entity.Id);
            }

            if (!string.IsNullOrWhiteSpace(entity?.FirstName))
            {
                result = result.Where(x => x.FirstName.StartsWith(entity.FirstName));
            }

            if (!string.IsNullOrWhiteSpace(entity?.LastName))
            {
                result = result.Where(x => x.LastName.StartsWith(entity.LastName));
            }

            if (!string.IsNullOrWhiteSpace(entity?.Email))
            {
                result = result.Where(x => x.Email.StartsWith(entity.Email));
            }

            if (entity != null && entity.Age != 0)
            {
                result = result.Where(x => x?.Age == entity?.Age);
            }

            if (entity != null && entity.Courses.Any())
            {
                result = result.Where(x => x.Courses.IsSupersetOf(entity.Courses));
            }

            return result;
        }

        /// <inheritdoc/>
        public Student GetByEmail(string email)
        {
            var result = _students.FirstOrDefault(x => x.Email == email);

            return result ?? throw new ArgumentException($"Invalid Email: {email}");
        }

        /// <inheritdoc/>
        public Student Register(string id, params string[] couseIds)
        {
            var student = Get(id);
            student.RegisterCourses(couseIds);
            return Update(id, student);
        }

        /// <inheritdoc/>
        public Student Remove(string id)
        {
            var student = Get(id);
            _students.Remove(student);
            return student;
        }

        /// <inheritdoc/>
        public Student Update(string id, Student entity)
        {
            Remove(id);
            _students = _students.Append(entity).ToList();

            return Get(id);
        }

        public Student Drop(string id, params string[] couseIds)
        {
            var student = Get(id);
            foreach(var courseId in couseIds)
            {
                student.DropCourse(courseId);
            }
            
            return Update(id, student);
        }
    }
}
