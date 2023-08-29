using System;

namespace DoranApp.Models
{
    internal class Hkelompokbarang
    {
        public int Id { get; set; }
        public int Kode { get; set; }
        public string Nama { get; set; }
        public bool Aktif { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}