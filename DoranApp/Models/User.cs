using System;

namespace DoranApp.Models
{
    class User
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string passwordText { get; set; }
        public string api_token { get; set; }
        public bool active { get; set; }
        public Guid roleId { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public Role role { get; set; }
    }
}
