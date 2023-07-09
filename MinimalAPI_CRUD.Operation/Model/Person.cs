using System.ComponentModel.DataAnnotations;

namespace MinimalAPI_CRUD.Operation.Model
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailId { get; set; } 

        [Required]
        public string MobileNo { get; set; }

    }
}
