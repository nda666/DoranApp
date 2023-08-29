using DoranApp.Models;
using DoranApp.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class SetlevelhargaData : AbstractData<Setlevelharga>
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
            var columnSettingsList = new ColumnSettings<Setlevelharga>
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
            _dataTable = _dataTableGen.CreateDataTable<Setlevelharga>(_data);
        }

        public async Task<dynamic> GetHargaByLevel(int[] kodeBarang, int kodeLevel, int kodePelanggan)
        {
            Rest rest = new Rest("harga/bylevel");
            var response = await rest.Get(new { 
                kodepelanggan = kodePelanggan,
                kodeLevel = kodeLevel,
                kodeBarang = kodeBarang
            });
            return response.Response;
        }
    }
}