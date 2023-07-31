using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    internal class SalesData: AbstractData<Sales>
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

        public override DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("Kode"),
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
                if (_data != null)
                {
                    _dataTable.BeginInit();
                    _dataTable.Rows.Clear();
                    foreach (Sales sales in _data)
                    {
                        DataRow r = _dataTable.NewRow();
                        r.BeginEdit();
                        r["Kode"] = sales.Kode;
                        r["Nama"] = sales.Nama;
                        r["Tim"] = sales.NamaTim;
                        r["Manager"] = sales.Manager;
                        r["Nama Manager"] = sales.NamaManager;
                        r["Sales Ol"] = sales.Salesol;
                        r["Email"] = sales.Email;
                        r["Email Jete Terdahsyat"] = sales.EmailJeteterdahsyat;
                        r["Email Omzet Terdahsyat"] = sales.EmailOmzetTerdahsyat;
                        r["Aktif"] = sales.Aktif;
                        r["Created At"] = sales.CreatedAt ?? Convert.DBNull;
                        r["Updated At"] = sales.UpdatedAt ?? Convert.DBNull;
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
