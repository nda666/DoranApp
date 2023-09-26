using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data.Laporan
{
    internal class LaporanTransaksiByBarangData : AbstractData<TransaksiByBarangResultDto>
    {
        public LaporanTransaksiByBarangData() : base()
        {
        }

        public LaporanTransaksiByBarangData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "laporan/transaksibybarang";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<TransaksiByBarangResultDto>
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
            _dataTable = _dataTableGen.CreateDataTable<TransaksiByBarangResultDto>(_data);
        }
    }
}