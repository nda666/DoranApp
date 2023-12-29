using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class LokasikotaData : AbstractData<LokasiKotaDataDto>
    {
        public LokasikotaData() : base()
        {
        }

        public LokasikotaData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "lokasikota";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<LokasiKotaDataDto>
            {
                { "Kode", x => x.Kode },
                { "Nama", x => x.Nama },
                { "NamaProvinsi", x => x.NamaProvinsi },
                { "Coa", x => !String.IsNullOrEmpty(x.NamaCoa) ? x.NamaCoa + " " + x.Kodecoa4.ToString() : "" },
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTable = _dataTableGen.CreateDataTable(_data);
        }
    }
}