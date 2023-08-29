using System;

namespace DoranApp.Models
{
    internal class Role
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}