namespace VimExercise.Logic
{
    /// <summary>
    /// represents a collection of entities.
    /// </summary>
    /// <typeparam name="T">the entity type</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// adds an entity to the repository.
        /// </summary>
        /// <param name="entity">retrieves an entity from the repository</param>
        public void Add(T entity);
        /// <summary>
        /// get an entity from the repository by its id
        /// </summary>
        /// <param name="entityIdentifier">the id of the entity</param>
        /// <returns>null if no entity with specified id was found. otherwise, the entity</returns>
        public Doctor TryGetById(string entityIdentifier);
    }
}
