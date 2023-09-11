using System.Collections.Generic;

namespace DoranApp.Models
{
    public partial class LokasiProvinsi
    {
        public int? Kode { get; set; }
        public string Nama { get; set; } = null!;
    }

    public partial class LokasiProvinsiOption
    {
        public int? Kode { get; set; }
        public string Nama { get; set; } = null!;

        public List<LokasiKotaOption> LokasiKota { get; set; } = new List<LokasiKotaOption>();
    }
}