using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public required RegionDto Region { get; set; }
        public required DifficultyDto Difficulty { get; set; }
    }
}
