using DoranApp.Models;
using DoranApp.Utils;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class MasterbarangData : AbstractData<Masterbarang>
    {
        public MasterbarangData() : base()
        {
        }

        public MasterbarangData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "masterbarang";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<Masterbarang>
                {
                    { "Kode", x => x.brgKode },
                    { "Nama", x => x.brgNama },
                };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable<Masterbarang>(_data);
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