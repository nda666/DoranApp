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
    internal class SalesTeamData: AbstractData<SalesTeam>
    {
        public SalesTeamData() : base()
        {
        }

        public SalesTeamData(object query) : base(query)
        {
        }

        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("ID"),
                new DataColumn("Nama"),
                new DataColumn("Channel"),
               new DataColumn("Target Jete", System.Type.GetType("System.Double")),
                new DataColumn("Target Omzet", System.Type.GetType("System.Double")),
                new DataColumn("Syarat Komisi", Type.GetType("System.Boolean")),
                new DataColumn("Tampil Th Lalu", Type.GetType("System.Boolean")),
                new DataColumn("Aktif", Type.GetType("System.Boolean")),
                new DataColumn("Created At", Type.GetType("System.DateTime")),
                new DataColumn("Updated At", Type.GetType("System.DateTime")),
                };
            return dataColumns;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest("salesteams");
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
                    foreach (SalesTeam salesTeam in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["ID"] = salesTeam.id;
                        r["Nama"] = salesTeam.name;
                        r["Channel"] = salesTeam.salesChannel?.name;
                        r["Target Jete"] = salesTeam.jeteTarget;
                        r["Target Omzet"] = salesTeam.omzetTarget;
                        r["Syarat Komisi"] = salesTeam.commissionTerms;
                        r["Tampil Th Lalu"] = salesTeam.showLastYear;
                        r["Aktif"] = salesTeam.active;
                        r["Created At"] = string.IsNullOrEmpty(salesTeam.createdAt) ? DBNull.Value : (object)DateTime.Parse(salesTeam.createdAt);
                        r["Updated At"] = string.IsNullOrEmpty(salesTeam.updatedAt) ? DBNull.Value : (object)DateTime.Parse(salesTeam.updatedAt);
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
