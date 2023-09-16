using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoranApp.Data.Laporan
{
    public class LaporanTransaksiBySales
    {
        public short Kode { get; set; }
        public string Nama { get; set; }
        public long SumTotal { get; set; }
        public double Persen { get; set; }
    }
    internal class LaporanTransaksiBySalesData : AbstractData<LaporanTransaksiBySales>
    {
        public LaporanTransaksiBySalesData() : base()
        {
        }

        public LaporanTransaksiBySalesData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "laporan/transaksibysales";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<LaporanTransaksiBySales> {
                  { "Nama", x => x.Nama },
                  { "Jumlah", x => x.SumTotal },
                  { "Persen", x => x.Persen },
            };
            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());

            ConsoleDump.Extensions.Dump(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<LaporanTransaksiBySales>(_data);
        }
    }
}
