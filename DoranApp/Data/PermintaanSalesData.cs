using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoranApp.Dtos;
using DoranApp.Utils;

namespace DoranApp.Data
{
    public class PermintaanSalesData : AbstractData<PermintaanSalesResult>
    {
        protected PaginationResultDto _paginationData = new PaginationResultDto();

        public PermintaanSalesData() : base()
        {
        }

        public PermintaanSalesData(object query) : base(query)
        {
        }

        protected dynamic _dynamicData { get; set; }

        protected override string RelativeUrl()
        {
            return "permintaan-sales";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<PermintaanSalesResult>
            {
                { "Gudang", x => x.Mastergudang?.Nama },
                { "Tanggal", x => x.Tglorder, typeof(DateTime) },
                { "Tujuan", x => x.MastergudangTujuan?.Nama },
                { "Sales", x => x.Sales?.Nama },
                { "Keterangan", x => x.Keterangan },
                { "Penyiap", x => x.Penyiaporder?.Nama },
                { "Oleh", x => x.MasteruserInsert?.Usernameku },
                { "Level", x => (x.Historynya == 3) ? "BELUM" : (x.Historynya == 2) ? "BISA DISIAPKAN" : "SELESAI" },
                { "Kodeh", x => x.Kodeh },
                { "Lunas", x => x.Lunas, typeof(Boolean) }
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            TReturn<PermintaanSalesResultDto> response = await rest.Get<PermintaanSalesResultDto>(_query);

            var data = new List<dynamic>();
            var responseData = response.Response.Data.ToList();

            _data = null;
            _dynamicData = null;
            _dynamicData = responseData;
            _data = responseData;
            _paginationData.Page = response.Response.Page;
            _paginationData.PageSize = response.Response.PageSize;
            _paginationData.TotalRow = response.Response.TotalRow;
            _paginationData.TotalPage = response.Response.TotalPage;
            foreach (var x in responseData)
            {
                data.Add(x);
            }

            _dataTable = null;
            _dataTable = _dataTableGen.CreateDataTable<dynamic>(data);
        }

        public dynamic GetDynamicData()
        {
            return _dynamicData;
        }

        public PaginationResultDto GetPaginationData()
        {
            return _paginationData;
        }

        public async Task CreatePermintaanSales(SavePermintaanSalesDto dto)
        {
            await _CLient.Save_Permintaan_SalesAsync(dto);
        }

        public async Task<PermintaanSalesResult> InsertDetail(int Kodeh, InsertDetailPermintaanSalesDto dto)
        {
            var result = await _CLient.Insert_Detail_Permintaan_SalesAsync(Kodeh, dto);
            return result;
        }

        public async Task<PermintaanSalesResult> UpdateDetail(int Kodeh, int Koded, InsertDetailPermintaanSalesDto dto)
        {
            var result = await _CLient.Update_Detail_Permintaan_SalesAsync(Kodeh, Koded, dto);
            return result;
        }

        public async Task<PermintaanSalesResult> DeleteDetail(int Kodeh, int[] Koded)
        {
            var response = await _CLient.Delete_Detail_Permintaan_SalesAsync(Kodeh, new DeletePermintaanSalesDetailDto
            {
                Koded = Koded
            });
            return response;
        }
    }
}