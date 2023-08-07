using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoranApp.Models
{
    internal class Masterdivisi
    {
        public Guid Id { get; set; }
        public int Kode { get; set; }
        public string Nama { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
