namespace DoranApp.Models
{
    public class LaporanTransaksiByBarang
    {
        public short BrgKode { get; set; }
        public string BrgNama { get; set; }
        public long Jumlah { get; set; }
        public long SumTotal { get; set; }
    }
}