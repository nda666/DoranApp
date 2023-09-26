using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data.Laporan
{
    public class LaporanTransaksiByToko
    {
        public short Kode { get; set; }
        public string Nama { get; set; }
        public long Jumlah { get; set; }
        public long SumTotal { get; set; }
    }

    internal class LaporanTransaksiByTokoData : AbstractData<TransaksiByTokoResultDto>
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
            var columnSettingsList = new ColumnSettings<TransaksiByTokoResultDto>
            {
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
            _dataTable = _dataTableGen.CreateDataTable<TransaksiByTokoResultDto>(_data);
        }
    }
}