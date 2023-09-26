using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Dtos;
using DoranApp.Utils;
using HtransitResultDto = DoranApp.Utils.HtransitResultDto;

namespace DoranApp.Data
{
    public class TransitData : AbstractData<HtransitResult>
    {
        protected PaginationResultDto _paginationData = new PaginationResultDto();

        public TransitData() : base()
        {
        }

        public TransitData(object query) : base(query)
        {
        }

        protected HtransitResult _dynamicData { get; set; }

        protected override string RelativeUrl()
        {
            return "transit";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<HtransitResult>
            {
                { "Tanggal", x => x.TglTrans, typeof(DateTime) },
                { "Nama Asal", x => x.Mastergudang?.Nama },
                { "Nama Tujuan", x => x.MastergudangTujuan?.Nama },
                { "Penyiap", x => x.Penyiaporder?.Nama },
                { "Admin", x => x.MasteruserInsert?.Usernameku },
                { "Tanggal Input", x => x.InsertTime },
                { "Keterangan", x => x.Keterangan },
                { "KodeT", x => x.KodeT },
                { "History", x => x.HistoryNya },
                { "Export", x => x.Export },
                { "Kode Gudang", x => x.Kodegudang },
                { "Kode Gudang Tujuan", x => x.KodeGudangTujuan }
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get<HtransitResultDto>(_query);

            List<HtransitResult> responseData = response.Response?.Data;
            _data = null;
            _data = responseData;
            _paginationData.Page = response.Response?.Page;
            _paginationData.PageSize = response.Response?.PageSize;
            _paginationData.TotalRow = response.Response?.TotalRow;
            _paginationData.TotalPage = response.Response?.TotalPage;

            var data = new List<dynamic>();
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

        public async Task<HtransitResult> DeleteDetailByKoded(int Kodet, int[] Koded)
        {
            var client = new Client();
            var response = await client.Delete_Detail_By_KodedAsync(Kodet, new DeleteDetailByKodedDto()
            {
                Koded = Koded
            });
            return response;
        }
    }
}