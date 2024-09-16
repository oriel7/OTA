
namespace OT.Assessment.Core.Domain.DTO
{
    public class CasinoWagerResponseDTO
    {
        public Guid WagerId { get; set; }
        public string GameName { get; set; }
        public string Provider { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
