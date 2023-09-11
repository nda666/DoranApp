using DoranApp.Models;
using DoranApp.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class MasterdivisiData : AbstractData<Masterdivisi>
    {
        public MasterdivisiData() : base()
        {
        }

        public MasterdivisiData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "masterdivisi";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new List<ColumnSettings> {
                new ColumnSettings("Kode", item => ((Masterdivisi) item).Kode),
                new ColumnSettings("Nama", item => ((Masterdivisi) item).Nama)
            };
            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<Masterdivisi>(_data);
        }
    }
}