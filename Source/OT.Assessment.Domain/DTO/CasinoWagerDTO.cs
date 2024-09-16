﻿
namespace OT.Assessment.Core.Domain.DTO
{
    public class CasinoWagerDTO
    {
        public string WagerId { get; set; }
        public string Theme { get; set; }
        public string Provider { get; set; }
        public string GameName { get; set; }
        public string TransactionId { get; set; }
        public string BrandId { get; set; }
        public string AccountId { get; set; }
        public string Username { get; set; }
        public string ExternalReferenceId { get; set; }
        public string TransactionTypeId { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int NumberOfBets { get; set; }
        public string CountryCode { get; set; }
        public string SessionData { get; set; }
        public long Duration { get; set; }
    }
}
