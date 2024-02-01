using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}
