using System;

namespace DoranApp.Models
{
    internal class Setlevelharga
    {
        public int Kode { get; set; }
        public string Nama { get; set; } = null!;
        public float AcuanTambah { get; set; }
        public float AcuanPotong { get; set; }
        public int Modal { get; set; }
        public bool Online { get; set; }
        public sbyte Urutan { get; set; }
    }
}