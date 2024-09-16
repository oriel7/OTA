
namespace OT.Assessment.Core.Domain.Models
{
    public class PlayerCasinoWager
    {
        public string WagerId { get; set; }
        public string Game { get; set; }
        public string Provider { get; set; }
        public string AccountId { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
