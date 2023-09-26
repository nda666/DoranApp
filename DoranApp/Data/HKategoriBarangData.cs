using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class HKategoriBarangData : AbstractData<Hkategoribarang>
    {
        public HKategoriBarangData() : base()
        {
        }

        public HKategoriBarangData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "hkategoribarang";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Hkategoribarang>
            {
                { "Kode", x => x.Kodeh },
                { "Nama", x => x.Nama },
                { "Harga khusus", x => x.Hargakhusus, typeof(bool) },
                { "Perlu set harga", (x) => x.Perlusetharga, typeof(bool) },
                { "Cek tahunan", (x) => x.Cektahunan, typeof(bool) },
            };
            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable(_data);
        }
    }
}