using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    class HKategoriBarangData
    {
        private dynamic Query;
        public DataTable DataTable { get; private set; }
        private BindingSource BindingSource;

        public HKategoriBarangData(dynamic query)
        {
            Query = query;
            DataTable = new DataTable();
            DataTable.Columns.AddRange(HKategoriBarangData.GetColumn());
            BindingSource = new BindingSource();
        }

        public static DataColumn[] GetColumn()
        {
            DataColumn kodehCol = new DataColumn("kodeh", System.Type.GetType("System.Int32"));
            kodehCol.AllowDBNull = true;

            DataColumn[] dataColumns = new DataColumn[] {

            kodehCol,
                new DataColumn("nama"),
                new DataColumn("aktif", System.Type.GetType("System.Int32"))
                };
            return dataColumns;
        }

        public async Task<BindingSource> GetBindingSource(bool forComboBox = false)
        {
            Rest rest = new Rest("hkategoribarang");
            BindingSource = new BindingSource();
            if (forComboBox)
            {
                DataRow r = DataTable.NewRow();
                r.BeginEdit();
                r["kodeh"] = DBNull.Value;
                r["nama"] = "SEMUA";
                r["aktif"] = 1;
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

                List<HKategoriBarang> kategoriBarangs = response.Response;
                foreach (HKategoriBarang dKategori in kategoriBarangs)
                {
                    DataRow r = DataTable.NewRow();
                    r.BeginEdit();
                    r["kodeh"] = dKategori.kodeh;
                    r["nama"] = dKategori.nama;
                    r["aktif"] = dKategori.aktif;
                    r.EndEdit();
                    DataTable.Rows.Add(r);
                }

            }
            BindingSource.DataSource = DataTable;
            return BindingSource;
        }
    }
}
