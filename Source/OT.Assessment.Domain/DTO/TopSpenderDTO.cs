
namespace OT.Assessment.Core.Domain.DTO
{
    public class TopSpenderDTO
    {
        public Guid AccountId { get; set; }
        public string UserName { get; set; }
        public double TotalAmountSpend { get; set; }
    }
}
