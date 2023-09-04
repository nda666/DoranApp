using DoranApp.Models;
using DoranApp.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class MasterpengeluaranData : AbstractData<Masterpengeluaran>
    {
        public MasterpengeluaranData() : base()
        {
        }

        public MasterpengeluaranData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "masterpengeluaran";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Masterpengeluaran> {
                  { "Kode", x => x.Kode },
                  { "Nama", x => x.Nama },
                  { "Aktif", x => x.Aktif, typeof(bool) },
            };
            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);

            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<Masterpengeluaran>(_data);
        }

        public async Task<TReturn> GetEkspedisiOnly()
        {
            Rest rest = new Rest($"{RelativeUrl()}/ekspedisi");

            return await rest.Get();
        }
    }
}