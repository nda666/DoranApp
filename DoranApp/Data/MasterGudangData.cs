using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    class MasterGudangData
    {
        private string URL = "master-gudang";
        private dynamic Query;
        public DataTable DataTable { get; private set; }
        private BindingSource BindingSource;

        public MasterGudangData(dynamic query)
        {
            Query = query;
            DataTable = new DataTable();
            DataTable.Columns.AddRange(MasterGudangData.GetColumn().ToArray());
            BindingSource = new BindingSource();
        }

        public static List<DataColumn> GetColumn()
        {
            var model = new MasterGudang();
            var columnLength = model.GetType().GetProperties().Length;

            List<DataColumn> dataColumns = new List<DataColumn>();
            foreach (var property in model.GetType().GetProperties())
            {
                DataColumn column = new DataColumn(property.Name, property.PropertyType);
                column.AllowDBNull = true;
                dataColumns.Add(column);
            }

            return dataColumns;
        }

        public async Task<BindingSource> GetBindingSource(bool forComboBox = false)
        {
            Rest rest = new Rest(URL);
            BindingSource = new BindingSource();
            if (forComboBox)
            {
                DataRow r = DataTable.NewRow();
                r.BeginEdit();
                r["kode"] = DBNull.Value;
                r["nama"] = "SEMUA";
                r["aktif"] = 1;
                r["urut"] = 1;
                r["boletransit"] = 1;
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

                List<MasterGudang> masterGudangs = response.Response;
                foreach (MasterGudang masterGudang in masterGudangs)
                {
                    DataRow r = DataTable.NewRow();
                    r.BeginEdit();
                    r["kode"] = masterGudang.kode;
                    r["nama"] = masterGudang.nama;
                    r["aktif"] = masterGudang.aktif;
                    r["aktif"] = masterGudang.urut;
                    r["urut"] = masterGudang.boletransit;
                    r["boletransit"] = 1;
                    r.EndEdit();
                    DataTable.Rows.Add(r);
                }

            }
            BindingSource.DataSource = DataTable;
            return BindingSource;
        }
    }
}
