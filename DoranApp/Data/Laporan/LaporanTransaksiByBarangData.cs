using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoranApp.Data.Laporan
{
    internal class LaporanTransaksiByBarangData : AbstractData<LaporanTransaksiByBarang>
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
            var columnSettingsList = new ColumnSettings<LaporanTransaksiByBarang> {
                  { "BrgNama", x => x.BrgNama },
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
            _dataTable = _dataTableGen.CreateDataTable<LaporanTransaksiByBarang>(_data);
        }
    }
}
