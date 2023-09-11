using System.Collections.Generic;

namespace DoranApp.Models
{
    public class Dkategoribarang
    {
        public int Koded { get; set; }
        public string Nama { get; set; } = null!;
        public int Kodeh { get; set; }
        public sbyte Munculdimasterbarangapps { get; set; }
        public sbyte Cnp { get; set; }
        public bool Sn { get; set; }
        public sbyte Perlusetharga { get; set; }
        public virtual ICollection<Masterbarang> Masterbarang { get; set; }

        public virtual Hkategoribarang? Hkategoribarang { get; set; }
    }

    public partial class DkategoribarangOption
    {
        public int? Koded { get; set; }
        public string Nama { get; set; } = null!;
        public int? Kodeh { get; set; }
    }
}