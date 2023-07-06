using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    class RolesData : AbstractData<Role>
    {

        public RolesData() : base()
        {
        }

        public RolesData(object query) : base(query)
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
            Rest rest = new Rest("roles");
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
                    foreach (Role role in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["ID"] = role.id;
                        r["Nama"] = role.name;
                        r["Aktif"] = role.active;
                        r["Created At"] = string.IsNullOrEmpty(role.createdAt) ? DBNull.Value : (object)DateTime.Parse(role.createdAt);
                        r["Updated At"] = string.IsNullOrEmpty(role.updatedAt) ? DBNull.Value : (object)DateTime.Parse(role.updatedAt);
                        r.EndEdit();
                        _dataTable.Rows.Add(r);
                    }
                    _dataTable.EndInit();
                }
                else
                {
                    _dataTable.Rows.Clear();
                    Console.WriteLine("Role Null");
                }
            }
        }
    }
}
