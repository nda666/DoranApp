using DoranApp.Models;
using DoranApp.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoranApp.Data
{
    class MasterBarangData
    {
        private dynamic Query;
        public DataTable DataTable { get; private set; }
        private BindingSource BindingSource;

        public MasterBarangData(dynamic query)
        {
            Query = query;
            DataTable = new DataTable();
            DataTable.Columns.AddRange(MasterBarangData.GetColumn());
            BindingSource = new BindingSource();
        }

        public static DataColumn[] GetColumn()
        {

            DataColumn[] dataColumns = new DataColumn[] {
                new DataColumn("Nama Barang"),
                new DataColumn("Rak Gudang"),
                new DataColumn("Kategori Barang"),
                new DataColumn("Stok", System.Type.GetType("System.Int32")),
                new DataColumn("Harga"),
                new DataColumn("Harga Toko"),
                new DataColumn("Harga SRP"),
                new DataColumn("Kode", System.Type.GetType("System.Int32"))
                };
            return dataColumns;
        }

        public async Task<BindingSource> GetBindingSource()
        {
            Rest rest = new Rest("master-barang");
            BindingSource = new BindingSource();

            var response = await rest.Get(Query);
            if (response.ErrorMessage != null)
            {
                MessageBox.Show(response.ErrorMessage);
            }
            else
            {
                Pagination pagination = response.Response;

                List<MasterBarang> masterbarang = new List<MasterBarang>();
                if (pagination.data != null)
                {
                    masterbarang = pagination.data.Select(
                       x => ((JObject)x).ToObject<MasterBarang>()
                       ).ToList<MasterBarang>();

                    foreach (MasterBarang mb in masterbarang)
                    {
                        DataRow r = DataTable.NewRow();
                        r.BeginEdit();
                        r["Nama Barang"] = mb.brgNama;
                        r["Rak Gudang"] = "-";
                        r["Kategori Barang"] = mb.dKategoriBarang.nama;
                        r["Stok"] = 0;
                        r["Harga"] = mb.hargaol;
                        r["Harga Toko"] = mb.hargatoko;
                        r["Harga SRP"] = mb.hargaSRP;
                        r["Kode"] = mb.brgKode;
                        r.EndEdit();
                        DataTable.Rows.Add(r);
                    }
                }
                else
                {
                    DataTable.Rows.Clear();
                    Console.WriteLine("Barang Null");
                }


                BindingSource.DataSource = DataTable;

            }
            return BindingSource;
        }
    }
}
