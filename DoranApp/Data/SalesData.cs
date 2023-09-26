using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class SalesData : AbstractData<SalesDto>
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

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<SalesDto>
            {
                { "Kode", x => x.Kode },
                { "Nama", x => x.Nama },
                { "Tim", x => x.NamaTim },
                { "Manager", (x) => x.Manager },
                { "Nama Manager", (x) => x.NamaManager },
                { "Email", (x) => x.Email },
                { "Sales Ol", (x) => x.Salesol, typeof(bool) },
                { "Email Omzet", (x) => x.Jenis, typeof(bool) },
                { "Email Jete Terdahsyat", (x) => x.EmailJeteterdahsyat, typeof(bool) },
                { "Email Omzet Terdahsyat", (x) => x.EmailOmzetTerdahsyat, typeof(bool) },
                { "Email Resi Kiriman", (x) => x.Emailresikiriman, typeof(bool) },
                { "Email Target Tahunan", (x) => x.EmailTargetTahunan, typeof(bool) },
                { "Lihat Omzet Tahunan", (x) => x.Bisalihatomzettahunantim, typeof(bool) },
                { "Aktif", x => x.Aktif, typeof(bool) },
                { "Urutan", x => x.Urutan },
            };
            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);

            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<SalesDto>(_data);
        }

        public async Task<TReturn> GetNameAndKodeOnly()
        {
            Rest rest = new Rest($"{RelativeUrl()}/nama");

            return await rest.Get(new
            {
                aktif = true
            });
        }
    }
}