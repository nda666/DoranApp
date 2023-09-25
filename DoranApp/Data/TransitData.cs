using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using DoranApp.Dtos;
using DoranApp.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var data = new List<dynamic>();
            List<HtransitResult> responseData = response.Response?.Data;
            _data = null;
            _data = responseData;
            _paginationData.Page = response.Response?.Page;
            _paginationData.PageSize = response.Response?.PageSize;
            _paginationData.TotalRow = response.Response?.TotalRow;
            _paginationData.TotalPage = response.Response?.TotalPage;
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

        public ExpandoObject ConvertToExpando(object anonymousObject)
        {
            var json = JsonConvert.SerializeObject(anonymousObject);

            ConsoleDump.Extensions.Dump(anonymousObject);
            var jObject = JObject.Parse(json);
            return jObject.ToObject<ExpandoObject>();
        }
    }
}