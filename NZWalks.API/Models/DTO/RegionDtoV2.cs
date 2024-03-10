namespace NZWalks.API.Models.DTO
{
    public class RegionDtoV2
    {
        public Guid Id { get; set; }
        public required string RegionCode { get; set; }
        public required string RegionName { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
