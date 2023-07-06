using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    internal class SalesData: AbstractData<Sales>
    {
        protected string _restUri = "sales";
        public SalesData() : base()
        {
        }

        public SalesData(object query) : base(query)
        {
        }

        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("ID"),
                new DataColumn("Nama"),
                new DataColumn("Team"),
                new DataColumn("Manager", Type.GetType("System.Boolean")),
                new DataColumn("Nama Manager"),
                new DataColumn("Omzet Email", Type.GetType("System.Boolean")),
                new DataColumn("Aktif", Type.GetType("System.Boolean")),
                new DataColumn("Created At", Type.GetType("System.DateTime")),
                new DataColumn("Updated At", Type.GetType("System.DateTime")),
                };
            return dataColumns;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(_restUri);
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
                    foreach (Sales sales in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["ID"] = sales.id;
                        r["Nama"] = sales.name;
                        r["Team"] = sales.salesTeam?.name;
                        r["Manager"] = sales.isManager;
                        r["Nama Manager"] = sales.manager?.name;
                        r["Omzet Email"] = sales.getOmzetEmail;
                        r["Aktif"] = sales.active;
                        r["Created At"] = string.IsNullOrEmpty(sales.createdAt) ? DBNull.Value : (object)DateTime.Parse(sales.createdAt);
                        r["Updated At"] = string.IsNullOrEmpty(sales.updatedAt) ? DBNull.Value : (object)DateTime.Parse(sales.updatedAt);
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

        static void FormatColumnToCurrency(DataTable dataTable, string columnName)
        {
            // Create a new computed column for the formatted price
            DataColumn formattedPriceColumn = new DataColumn("FormattedPrice", typeof(string));
            formattedPriceColumn.Expression = $"FORMAT({columnName}, 'C')";
            dataTable.Columns.Add(formattedPriceColumn);
        }
    }
    
}
