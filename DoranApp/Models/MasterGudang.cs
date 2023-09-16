using System;

namespace DoranApp.Models
{
    internal class Mastergudang
    {
        public int Kode { get; set; }
        public string Nama { get; set; }
        public bool Aktif { get; set; }
        public sbyte Urut { get; set; }
        public bool Boletransit { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public DateTime? DeletedAt { get; set; }
    }

    public class MastergudangOption
    {
        public int? Kode { get; set; }
        public string Nama { get; set; }
    }
}