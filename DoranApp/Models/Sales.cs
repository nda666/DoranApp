using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoranApp.Models
{
    internal class Sales
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public Guid salesTeamlId { get; set; }
        public bool isManager { get; set; }
        public Guid? managerId { get; set; }
        public bool getOmzetEmail { get; set; }
        public bool active { get; set; }
        public string createdAt { get; set; } = null;
        public string updatedAt { get; set; } = null;
        public string deletedAt { get; set; } = null;
        public virtual Sales manager { get; set; } = null;
        public virtual Masteruser user { get; set; } = null;
        public virtual SalesTeam salesTeam { get; set; } = null;
    }
}
