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
    internal class MastertimsalesData: AbstractData<Mastertimsales>
    {
        public MastertimsalesData() : base()
        {
        }

        public MastertimsalesData(object query) : base(query)
        {
        }
        protected override string RelativeUrl()
        {
            return "mastertimsales";
        }

        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("Kode"),
                new DataColumn("Nama"),
                new DataColumn("Channel"),
               new DataColumn("Target Jete", System.Type.GetType("System.Double")),
                new DataColumn("Target Omzet", System.Type.GetType("System.Double")),
                new DataColumn("Syarat Komisi", Type.GetType("System.Boolean")),
                new DataColumn("Tampil Th Lalu", Type.GetType("System.Boolean")),
                new DataColumn("Aktif", Type.GetType("System.Boolean")),
                new DataColumn("Created At", typeof(DateTime)),
                new DataColumn("Updated At", typeof(DateTime)),
                };
            return dataColumns;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
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
                    foreach (Mastertimsales salesTeam in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["Kode"] = salesTeam.Kode;
                        r["Nama"] = salesTeam.Nama;
                        r["Channel"] = salesTeam.Masterchannelsales?.Nama;
                        r["Target Jete"] = salesTeam.Targetjete;
                        r["Target Omzet"] = salesTeam.Targetomzet;
                        r["Syarat Komisi"] = salesTeam.SyaratKomisi;
                        r["Tampil Th Lalu"] = salesTeam.Tampiltahunlalu;
                        r["Aktif"] = salesTeam.Aktif;
                        r["Created At"] = salesTeam.CreatedAt ?? Convert.DBNull;
                        r["Updated At"] = salesTeam.UpdatedAt ?? Convert.DBNull;
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
