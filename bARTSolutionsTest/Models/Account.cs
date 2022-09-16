using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace bARTSolutionsTest.Models
{
    public class Account
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public ICollection<Contact> Contacts { get; set; }

    }
}
