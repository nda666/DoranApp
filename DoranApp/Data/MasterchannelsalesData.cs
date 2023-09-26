using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class MasterchannelsalesData : AbstractData<Masterchannelsales>
    {
        public MasterchannelsalesData() : base()
        {
        }

        public MasterchannelsalesData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "masterchannelsales";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Masterchannelsales>
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
            _dataTable = _dataTableGen.CreateDataTable(_data);
        }
    }
}