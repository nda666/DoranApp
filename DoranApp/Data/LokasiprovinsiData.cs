using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class LokasiprovinsiData : AbstractData<LokasiProvinsi>
    {
        public LokasiprovinsiData() : base()
        {
        }

        public LokasiprovinsiData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "lokasiprovinsi";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<LokasiProvinsi>
            {
                { "Kode", x => x.Kode },
                { "Nama", x => x.Nama }
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<LokasiProvinsi>(_data);
        }
    }
}