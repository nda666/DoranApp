namespace DoranApp.Models
{
    internal class MasterbarangOption
    {
        public int? brgKode { get; set; }
        public string brgNama { get; set; }
    }

    public class Masterbarang
    {
        public int brgKode { get; set; }
        public string brgNama { get; set; }
        public int brgAktif { get; set; }
        public int insertName { get; set; }
        public string insertTime { get; set; }
        public int updateName { get; set; }
        public string updateTime { get; set; }
        public int brgHabis { get; set; }
        public int modal { get; set; }
        public int hargatoko { get; set; }
        public int hargaSRP { get; set; }
        public int minStokHabis { get; set; }
        public int maksstok { get; set; }
        public int poinToko { get; set; }
        public int supplierkode { get; set; }
        public int tipebarang { get; set; }
        public int hargaol { get; set; }
        public string namaol { get; set; }
        public int brgspesial { get; set; }
        public int komisi { get; set; }
        public int kategoriBrg { get; set; }
        public int favorit { get; set; }
        public int groupbaranghabis { get; set; }
        public int diskontinu { get; set; }
        public int statusKirimanCina { get; set; }
        public string ketKirimanCina { get; set; }

        public Dkategoribarang Dkategoribarang { get; set; }
    }
}