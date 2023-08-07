using DoranApp.Utils;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class MasteruserData : AbstractData<Models.Masteruser>
    {
        protected override string RelativeUrl()
        {
            return "masteruser";
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
