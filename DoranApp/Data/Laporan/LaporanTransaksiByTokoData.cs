using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoranApp.Data.Laporan
{
    public class LaporanTransaksiByToko
    {
        public short Kode { get; set; }
        public string Nama { get; set; }
        public long Jumlah { get; set; }
        public long SumTotal { get; set; }
    }
    internal class LaporanTransaksiByTokoData : AbstractData<LaporanTransaksiByToko>
    {
        public LaporanTransaksiByTokoData() : base()
        {
        }

        public LaporanTransaksiByTokoData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "laporan/transaksibytoko";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<LaporanTransaksiByToko> {
                  { "Nama", x => x.Nama },
                  { "Jumlah", x => x.Jumlah },
                  { "Total", x => x.SumTotal },
            };
            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());

            ConsoleDump.Extensions.Dump(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<LaporanTransaksiByToko>(_data);
        }
    }
}
