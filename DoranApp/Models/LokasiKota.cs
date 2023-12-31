﻿using System.Collections.Generic;

namespace DoranApp.Models
{
    public partial class LokasiKota
    {
        public int? Kode { get; set; }
        public string Nama { get; set; } = null!;
        public sbyte Provinsi { get; set; }
        public sbyte Kodeareapengiriman { get; set; }
        public int Kodecoa4 { get; set; }
        public sbyte AdaKertasOrder { get; set; }

        public virtual ICollection<Masterpelanggan>? Masterpelanggans { get; set; }
    }

    public partial class LokasiKotaOption
    {
        public int? Kode { get; set; }
        public string Nama { get; set; } = null!;
        public sbyte Provinsi { get; set; }

    }
    }