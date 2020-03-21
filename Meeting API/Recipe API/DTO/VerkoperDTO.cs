using System.ComponentModel.DataAnnotations;

namespace Recipe_API.DTO
{
    public class VerkoperDTO
    {
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
    }
}