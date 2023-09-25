namespace DoranApp.Models;

public class Dtransit
{
    public int Id { get; set; }
    public int Kodet { get; set; }
    public short Koded { get; set; }
    public short Kodebarang { get; set; }
    public short Jumlah { get; set; }
    public bool Sudahdicek { get; set; }
    public string Namapenerima { get; set; } = null!;
    public string NmrSn { get; set; } = null!;
    public Masterbarang? Masterbarang { get; set; }
}