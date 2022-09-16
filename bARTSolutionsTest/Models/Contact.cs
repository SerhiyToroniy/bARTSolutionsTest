using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace bARTSolutionsTest.Models
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Key]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
