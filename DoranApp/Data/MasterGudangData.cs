using DoranApp.Models;
using DoranApp.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class MastergudangData : AbstractData<Mastergudang>
    {
        public MastergudangData() : base() { }

        public MastergudangData(object query) : base(query) { }
        protected override string RelativeUrl()
        {
            return "mastergudang";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Mastergudang>
                {
                    { "Kode", x => x.Kode },
                    { "Nama", x => x.Nama },
                    { "Urut", x=> x.Urut, typeof(sbyte) },
                    { "Bisa Transit", x=> x.Boletransit, typeof(bool) },
                    { "Aktif", x=> x.Aktif, typeof(bool) }
                };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<Mastergudang>(_data);
        }

    }

}