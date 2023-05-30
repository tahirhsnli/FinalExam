using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Fullname { get; set; } = null!;
        public string? Image { get; set; }
        public int ProfessionId { get; set; }
        public Profession? Profession { get; set; }
        public bool Isdeleted { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
