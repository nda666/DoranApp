using DoranApp.Models;
using DoranApp.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class MasterjabatanData : AbstractData<Masterjabatan>
    {
        public MasterjabatanData() : base() { }

        public MasterjabatanData(object query) : base(query) { }
        protected override string RelativeUrl()
        {
            return "masterjabatan";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Masterjabatan>
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
            _dataTable = _dataTableGen.CreateDataTable<Masterjabatan>(_data);
        }

    }

}