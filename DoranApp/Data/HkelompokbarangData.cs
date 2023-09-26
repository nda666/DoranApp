using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class HkelompokbarangData : AbstractData<Hkelompokbarang>
    {
        public HkelompokbarangData() : base()
        {
        }

        public HkelompokbarangData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "mastergudang";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Hkelompokbarang>
            {
                { "Kode", x => x.Kode },
                { "Nama", x => x.Nama },
                { "Aktif", x => x.Aktif, typeof(bool) }
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<Hkelompokbarang>(_data);
        }

        public async Task SetActive(int kode, bool aktif)
        {
            Rest rest = new Rest($"{RelativeUrl()}/{kode}/aktif");
            var response = await rest.Post(new { aktif = aktif });
            await RunRefresh();
        }

        public async Task SetBolehTransit(int kode, bool transit)
        {
            Rest rest = new Rest($"{RelativeUrl()}/{kode}/transit");
            var response = await rest.Post(new { boletransit = transit });
            await RunRefresh();
        }
    }
}