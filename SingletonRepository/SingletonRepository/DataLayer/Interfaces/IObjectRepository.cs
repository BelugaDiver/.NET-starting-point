namespace SingletonRepository.DataLayer
{
    /// <summary>
    /// The IObjectRepository interface describes a base component capable of managing
    /// and delivering objects.
    /// </summary>
    /// <typeparam name="T">Type of object to be managed.</typeparam>
    public interface IObjectRepository<T>
    {
        /// <summary>
        /// Add a new entity to the data store.
        /// </summary>
        /// <typeparam name="T">Type of entity to be added.</typeparam>
        /// <param name="entity">Entity to be added.</param>
        /// <returns>Returns the entity added to the data store.</returns>
        T Add(T entity);

        /// <summary>
        /// Gets a single entity.
        /// </summary>
        /// <typeparam name="T">Type of entity to get.</typeparam>
        /// <param name="id">Id of the entity to get.</param>
        /// <returns>A single entity.</returns>
        T Get(string id);

        /// <summary>
        /// Gets a list of entities that satisfies all conditions presented
        /// by the current entity.
        /// </summary>
        /// <param name="entity">Entity properties that should be queried.</param>
        /// <returns>A list of entities that match the passed-in entity.</returns>
        IEnumerable<T> Get(T entity);

        /// <summary>
        /// Updates an entity in the data store.
        /// </summary>
        /// <typeparam name="T">Type of entity to be updated.</typeparam>
        /// <param name="id">Id of the entity to be updated.</param>
        /// <param name="entity">Updated entity.</param>
        /// <returns>The updated entity in the data store.</returns>
        T Update(string id, T entity);

        /// <summary>
        /// Removes an entity from the data store.
        /// </summary>
        /// <typeparam name="T">Type of entity to be removed.</typeparam>
        /// <param name="id">Id of the entity to be removed.</param>
        /// <returns>The removed entity</returns>
        T Remove(string id);
    }
}
