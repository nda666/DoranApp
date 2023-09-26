using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data.Laporan
{
    public class LaporanTransaksiBySales
    {
        public short Kode { get; set; }
        public string Nama { get; set; }
        public long SumTotal { get; set; }
        public double Persen { get; set; }
    }

    internal class LaporanTransaksiBySalesData : AbstractData<TransaksiBySalesResultDto>
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
            var columnSettingsList = new ColumnSettings<TransaksiBySalesResultDto>
            {
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
            _dataTable = _dataTableGen.CreateDataTable<TransaksiBySalesResultDto>(_data);
        }
    }
}