using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoranApp.LocalModel
{
    public class NotaTransaksiDetail
    {
        public int no { get; set; }
        public int jumlah { get; set; }
        public string brgNama { get; set; }
        public long harga { get; set; }
        public long subTotal { get; set;}
    }
}
