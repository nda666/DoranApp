namespace DoranApp.Models
{
    internal class Dtrans
    {
        public int Id { get; set; }
        public int Kodeh { get; set; }
        public short Koded { get; set; }
        public short Kodebarang { get; set; }
        public short Jumlah { get; set; }
        public long? Harga { get; set; }
        public int Komisi { get; set; }
        public int Untung { get; set; }
        public sbyte PoinToko { get; set; }
        public bool? KuranginStok { get; set; }
        public sbyte Tukartipe { get; set; }
        public sbyte HargaOk { get; set; }
        public string Nmrsn { get; set; } = null!;

        public virtual Htrans? Htrans { get; set; }
        public virtual Masterbarang? Masterbarang { get; set; }
    }
}