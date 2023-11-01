using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class MasterpelangganData : AbstractData<Masterpelanggan>
    {
        public MasterpelangganData() : base()
        {
        }

        public MasterpelangganData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "masterpelanggan";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Masterpelanggan>
            {
                { "Kode", x => x.Kode },
                { "Nama", x => x.Nama },
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<Masterpelanggan>(_data);
        }

        public async Task<List<MasterpelangganNamaResultDto>> GetOptions()
        {
            var pelanggan = await _CLient.Get_Masterpelanggan_NamaAsync(aktif: true);
            return pelanggan.ToList();
        }

        public async Task<TReturn> GetNameAndKodeOnly()
        {
            Rest rest = new Rest($"{RelativeUrl()}/nama");
            return await rest.Get(new
            {
                brgAktif = true
            });
        }
    }
}