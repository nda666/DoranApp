using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class DKategoriBarangData
    {
        private BindingSource BindingSource;
        private dynamic Query;

        public DKategoriBarangData(dynamic query)
        {
            Query = query;
            DataTable = new DataTable();
            DataTable.Columns.AddRange(DKategoriBarangData.GetColumn());
            BindingSource = new BindingSource();
        }

        public DataTable DataTable { get; private set; }

        public static DataColumn[] GetColumn()
        {
            DataColumn kodedCol = new DataColumn("koded", System.Type.GetType("System.Int32"));
            kodedCol.AllowDBNull = true;
            DataColumn[] dataColumns = new DataColumn[]
            {
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
                List<Dkategoribarang> kategoriBarangs = response.Response;
                foreach (Dkategoribarang dKategori in kategoriBarangs)
                {
                    DataRow r = DataTable.NewRow();
                    r.BeginEdit();
                    r["koded"] = dKategori.Koded;
                    r["nama"] = dKategori.Nama;
                    r["kodeh"] = dKategori.Kodeh;
                    r["munculdimasterbarangapps"] = dKategori.Munculdimasterbarangapps;
                    r["cnp"] = dKategori.Cnp;
                    r["sn"] = dKategori.Sn;
                    r.EndEdit();
                    DataTable.Rows.Add(r);
                }
            }

            BindingSource.DataSource = DataTable;
            return BindingSource;
        }
    }
}