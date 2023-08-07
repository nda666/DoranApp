using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class SalesData : AbstractData<Sales>
    {
        public SalesData() : base()
        {
        }

        public SalesData(object query) : base(query)
        {
        }
        protected override string RelativeUrl()
        {
            return "sales";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Sales> {
                  { "Kode", x => x.Kode },
                  { "Nama", x => x.Nama },
                  { "Tim", x => x.NamaTim },
                    { "Manager", (x) => x.Manager },
                  { "Nama Manager", (x) => x.NamaManager },
                  { "Aktif", x => x.Aktif },
                  { "Created At", x => x.CreatedAt },
                  { "Updated At", x => x.UpdatedAt },
            };
            return columnSettingsList;
        }

        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("Kode", typeof(Int32)),
                new DataColumn("Nama"),
                new DataColumn("Tim"),
                new DataColumn("Manager", Type.GetType("System.Boolean")),
                new DataColumn("Nama Manager"),
                new DataColumn("Sales Ol", Type.GetType("System.Boolean")),
                new DataColumn("Email"),
            new DataColumn("Email Jete Terdahsyat", Type.GetType("System.Boolean")),
                   new DataColumn("Email Omzet Terdahsyat", Type.GetType("System.Boolean")),
                new DataColumn("Aktif", Type.GetType("System.Boolean")),
                new DataColumn("Created At", Type.GetType("System.DateTime")),
                new DataColumn("Updated At", Type.GetType("System.DateTime")),
                };
            return dataColumns;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);

            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<Sales>(_data);

        }
    }

}
