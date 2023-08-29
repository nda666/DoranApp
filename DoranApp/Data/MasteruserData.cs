using DoranApp.Models;
using DoranApp.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class MasteruserData : AbstractData<Models.Masteruser>
    {
        protected override string RelativeUrl()
        {
            return "masteruser";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Masteruser> {
                  { "Kode", x => x.Kodeku },
                  { "Username", x => x.Usernameku },
                  { "Akses", x => x.Akses },
                  { "Aktif", x => x.Aktif, typeof(bool) },
                 

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