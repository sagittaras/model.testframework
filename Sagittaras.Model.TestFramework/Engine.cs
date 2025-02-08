namespace Sagittaras.Model.TestFramework
{
    public enum Engine
    {
        /// <summary>
        ///     Used database is only in-memory.
        /// </summary>
        InMemory,
        
        /// <summary>
        ///     Used database is engine like sqlserver or npgsql.
        /// </summary>
        DbEngine
    }
}