using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    internal class RolesData : AbstractData<Role>
    {
        public RolesData() : base()
        {
        }

        public RolesData(object query) : base(query)
        {
        }


        protected override string RelativeUrl()
        {
            return "role";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Role> {
                  { "Kode", x => x.id },
                  { "Nama", x => x.name },
                  { "aktif", x => x.active, typeof(bool) },
                  { "Created at", (x) => x.createdAt, typeof(bool) },
                 

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