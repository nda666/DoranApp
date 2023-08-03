using DoranApp.Utils;
using System;

namespace DoranApp.Models
{
    class HKategoribarang
    {
        [Column(hide:true)]
        public Guid Id { get; set; }
        public int Kodeh { get; set; }
        public string Nama { get; set; }
        public bool Aktif { get; set; }

        [Column(name: "Perlu set harga")]
        public bool Perlusetharga { get; set; }

        [Column(name: "Cek tahunan")]
        public bool Cektahunan { get; set; }

        [Column(name: "Harga khusus")]
        public bool Hargakhusus { get; set; }

        [Column(name: "Created At")]
        public DateTime? CreatedAt { get; set; }

        [Column(name: "Updated At")]
        public DateTime? UpdatedAt { get; set; }

        [Column(hide: true)]
        public DateTime? DeletedAt { get; set; }
    }
}
