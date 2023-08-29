using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    internal class DKategoriBarangData
    {
        private dynamic Query;
        public DataTable DataTable { get; private set; }
        private BindingSource BindingSource;

        public DKategoriBarangData(dynamic query)
        {
            Query = query;
            DataTable = new DataTable();
            DataTable.Columns.AddRange(DKategoriBarangData.GetColumn());
            BindingSource = new BindingSource();
        }

        public static DataColumn[] GetColumn()
        {
            DataColumn kodedCol = new DataColumn("koded", System.Type.GetType("System.Int32"));
            kodedCol.AllowDBNull = true;
            DataColumn[] dataColumns = new DataColumn[] {
                kodedCol,
                new DataColumn("nama"),
                new DataColumn("kodeh", System.Type.GetType("System.Int32")),
                new DataColumn("munculdimasterbarangapps", System.Type.GetType("System.Int32")),
                new DataColumn("cnp"),
                new DataColumn("sn")
                };
            return dataColumns;
        }

        public async Task<BindingSource> GetBindingSource(bool forComboBox = false)
        {
            Rest rest = new Rest("dkategoribarang");
            BindingSource = new BindingSource();
            if (forComboBox)
            {
                DataRow r = DataTable.NewRow();
                r.BeginEdit();
                r["koded"] = DBNull.Value;
                r["nama"] = "SEMUA";
                r["kodeh"] = DBNull.Value;
                r["munculdimasterbarangapps"] = DBNull.Value;
                r["cnp"] = "";
                r["sn"] = "";
                r.EndEdit();
                DataTable.Rows.Add(r);
            }
            var response = await rest.Get(Query);
            if (response.ErrorMessage != null)
            {
                MessageBox.Show(response.ErrorMessage);
            }
            else
            {
                List<DKategoriBarang> kategoriBarangs = response.Response;
                foreach (DKategoriBarang dKategori in kategoriBarangs)
                {
                    DataRow r = DataTable.NewRow();
                    r.BeginEdit();
                    r["koded"] = dKategori.koded;
                    r["nama"] = dKategori.nama;
                    r["kodeh"] = dKategori.kodeh;
                    r["munculdimasterbarangapps"] = dKategori.munculdimasterbarangapps;
                    r["cnp"] = dKategori.cnp;
                    r["sn"] = dKategori.sn;
                    r.EndEdit();
                    DataTable.Rows.Add(r);
                }
            }
            BindingSource.DataSource = DataTable;
            return BindingSource;
        }
    }
}