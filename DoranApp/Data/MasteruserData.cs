using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class MasteruserData : AbstractData<Masteruser>
    {
        protected override string RelativeUrl()
        {
            return "masteruser";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Masteruser>
            {
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

        public async Task UpdateUserSession()
        {
            var response = await _CLient.Get_User_By_TokenAsync();
            Session.SetUser(response);
        }
    }
}