using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    internal class UserData : AbstractData<User>
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
                    foreach (User user in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["ID"] = user.id;
                        r["Username"] = user.username;
                        r["Password"] = user.passwordText;
                        r["Role"] = user.role.name;
                        r["Aktif"] = user.active;
                        r["Created At"] = string.IsNullOrEmpty(user.createdAt) ? DBNull.Value : (object)DateTime.Parse(user.createdAt);
                        r["Updated At"] = string.IsNullOrEmpty(user.updatedAt) ? DBNull.Value : (object)DateTime.Parse(user.updatedAt);
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
