namespace Sagittaras.Model.TestFramework
{
    public enum Engine
    {
        /// <summary>
        /// Used database is only in-memory.
        /// </summary>
        InMemory,
        
        /// <summary>
        /// Used databse is some database engine like sqlserver or npgsql.
        /// </summary>
        DbEngine
    }
}