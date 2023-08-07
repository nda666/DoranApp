using System;

namespace DoranApp.Models
{
    internal class SalesTeam
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public Guid salesChannelId { get; set; }
        public ulong jeteTarget { get; set; }
        public ulong omzetTarget { get; set; }
        public bool showLastYear { get; set; }
        public bool commissionTerms { get; set; }
        public bool active { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string deletedAt { get; set; }

        public SalesChannel salesChannel { get; set; }
    }
}