using System;
using System.Collections.Generic;

namespace DoranApp.Models
{
    internal class Masterdivisi
    {
        //public Guid Id { get; set; }
        public int Kode { get; set; }
        public string Nama { get; set; } = null!;
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public DateTime? DeletedAt { get; set; }
        public ICollection<Masterpegawai> Masterpegawais { get; set; }
    }
}