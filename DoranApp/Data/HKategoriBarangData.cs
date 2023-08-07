using DoranApp.Models;
using DoranApp.Utils;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    class HKategoriBarangData : AbstractData<HKategoribarang>
    {
        public HKategoriBarangData() : base()
        {
        }
        public HKategoriBarangData(object query) : base(query)
        {
        }
        protected override string RelativeUrl()
        {
            return "hkategoribarang";
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
