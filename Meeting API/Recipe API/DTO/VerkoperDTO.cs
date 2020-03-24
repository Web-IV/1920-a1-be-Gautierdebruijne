using System.ComponentModel.DataAnnotations;

namespace MeetingAPI.DTO
{
    public class VerkoperDTO
    {
        [Required]
        public string Name { get; set; }
        public string Title { get; set; }
    }
}