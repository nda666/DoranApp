using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    internal class UserData : AbstractData<Models.Masteruser>
    {

        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("ID"),
                new DataColumn("Username"),
                new DataColumn("Password"),
                new DataColumn("Role"),
                new DataColumn("Aktif", Type.GetType("System.Boolean")),
                new DataColumn("Created At", Type.GetType("System.DateTime")),
                new DataColumn("Updated At", Type.GetType("System.DateTime")),
                };
            return dataColumns;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest("users");
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
                    foreach (Masteruser user in _data)
                    {

                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["ID"] = user.Kodeku;
                        r["Username"] = user.Usernameku;
                        r["Password"] = user.Passwordku;
                        r["Role"] = user.Akses;
                        r["Aktif"] = user.Aktif;
                        r["Created At"] = string.IsNullOrEmpty(user.CreatedAt) ? DBNull.Value : (object)DateTime.Parse(user.CreatedAt);
                        r["Updated At"] = string.IsNullOrEmpty(user.UpdatedAt) ? DBNull.Value : (object)DateTime.Parse(user.UpdatedAt);
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
