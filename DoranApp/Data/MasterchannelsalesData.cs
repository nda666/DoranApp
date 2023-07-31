using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("ID"),
                new DataColumn("Nama"),
                new DataColumn("Created At", typeof(DateTime)),
                new DataColumn("Updated At", typeof(DateTime)),
                new DataColumn("Aktif", typeof(bool)),
                };
            return dataColumns;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());

            var response = await rest.Get(_query);
            if (response.ErrorMessage != null)
            {
                MessageBox.Show("123123");
            }
            else
            {
                _data = response.Response;
                if (_data != null)
                {
                    _dataTable.BeginInit();
                    _dataTable.Rows.Clear();
                    foreach (Masterchannelsales salesChannel in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["ID"] = salesChannel.Kode;
                        r["Nama"] = salesChannel.Nama;
                        r["Created At"] = salesChannel.CreatedAt ?? Convert.DBNull;
                        r["Updated At"] = salesChannel.UpdatedAt ?? Convert.DBNull;
                        r["Aktif"] = salesChannel.Aktif;
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
