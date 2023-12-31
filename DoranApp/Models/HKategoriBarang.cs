﻿using System;
using System.Collections.Generic;

namespace DoranApp.Models
{
    public class Hkategoribarang
    {
        //public Guid Id { get; set; }
        public int Kodeh { get; set; }
        public string Nama { get; set; }
        public bool Aktif { get; set; }
        public bool Perlusetharga { get; set; }
        public bool Cektahunan { get; set; }
        public bool Hargakhusus { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public DateTime? DeletedAt { get; set; }
    }

    public class HkategoribarangOption
    {
        public int? Kodeh { get; set; }
        public string Nama { get; set; }

        public List<DkategoribarangOption> Dkategoribarang { get; set; } = new List<DkategoribarangOption>();
    }
}