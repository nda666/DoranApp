using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    internal class SalesChannelData : AbstractData<SalesChannel>
    {
        public SalesChannelData() : base()
        {
        }

        public SalesChannelData(object query) : base(query)
        {
        }

        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("ID"),
                new DataColumn("Nama"),
                new DataColumn("Aktif", Type.GetType("System.Boolean")),
                new DataColumn("Created At", Type.GetType("System.DateTime")),
                new DataColumn("Updated At", Type.GetType("System.DateTime")),
                };
            return dataColumns;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest("saleschannels");
            var response = await rest.Get(_query);
            if (response.ErrorMessage != null)
            {
                MessageBox.Show(response.ErrorMessage);
            }
            else
            {
                _data = response.Response;
                if (_data != null)
                {
                    _dataTable.BeginInit();
                    _dataTable.Rows.Clear();
                    foreach (SalesChannel salesChannel in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["ID"] = salesChannel.id;
                        r["Nama"] = salesChannel.name;
                        r["Aktif"] = salesChannel.active;
                        r["Created At"] = string.IsNullOrEmpty(salesChannel.createdAt) ? DBNull.Value : (object)DateTime.Parse(salesChannel.createdAt);
                        r["Updated At"] = string.IsNullOrEmpty(salesChannel.updatedAt) ? DBNull.Value : (object)DateTime.Parse(salesChannel.updatedAt);
                        r.EndEdit();
                        _dataTable.Rows.Add(r);
                    }
                    _dataTable.EndInit();
                }
                else
                {
                    _dataTable.Rows.Clear();
                }
            }
        }
    }
}
