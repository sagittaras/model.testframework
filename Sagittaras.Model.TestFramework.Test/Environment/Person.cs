using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sagittaras.Model.TestFramework.Test.Environment
{
    /// <summary>
    /// Testing entity
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Person's identification
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        /// <summary>
        /// Person's first name
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Peron's last name
        /// </summary>
        public string LastName { get; set; }
    }
}