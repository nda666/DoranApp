using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class SetlevelhargaData : AbstractData<CommonResultDto>
    {
        public SetlevelhargaData() : base()
        {
        }

        public SetlevelhargaData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "sethargalevel";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<CommonResultDto>
            {
                { "Kode", x => x.Kode },
                { "Nama", x => x.Nama },
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<CommonResultDto>(_data);
        }

        public async Task<dynamic> GetHargaByLevel(int[] kodeBarang, int kodeLevel, int kodePelanggan)
        {
            Rest rest = new Rest("harga/bylevel");
            var response = await rest.Get(new
            {
                kodepelanggan = kodePelanggan,
                kodeLevel = kodeLevel,
                kodeBarang = kodeBarang
            });
            return response.Response;
        }
    }
}