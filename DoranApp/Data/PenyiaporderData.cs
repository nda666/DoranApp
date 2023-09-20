using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Models;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class PenyiaporderData : AbstractData<Penyiaporder>
    {
        public PenyiaporderData() : base()
        {
        }

        public PenyiaporderData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "penyiaporder";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Penyiaporder>
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
            _dataTable = _dataTableGen.CreateDataTable<Penyiaporder>(_data);
        }

        public async Task<List<Penyiaporder>> GetActivePenyiapOrder()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(new
            {
                aktif = true
            });

            return (List<Penyiaporder>)response.Response;
        }

        public async Task SetActive(int kode, bool aktif)
        {
            Rest rest = new Rest($"{RelativeUrl()}/{kode}/aktif");
            var response = await rest.Post(new { aktif = aktif });
            await RunRefresh();
        }
    }
}