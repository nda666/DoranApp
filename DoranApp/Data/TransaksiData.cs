using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class TransaksiData : AbstractData<dynamic>
    {
        protected dynamic _dynamicData { get; set; }
        protected PaginationData _paginationData = new PaginationData();

        public TransaksiData() : base()
        {
        }

        public TransaksiData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "transaksi";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<dynamic>
                {
                    { "No Nota", x => x.kodenota },
                    { "Tanggal", x => x.tglTrans, typeof(DateTime) },
                    { "Nama", x => x.masterpelanggan?.nama + " - " +  x.masterpelanggan?.lokasiKota?.nama },
                    { "Jumlah", x => x.jumlah },
                    { "Sales", x => x.kodeSales },
                    { "Tipe", x => x.tipetempo, typeof(sbyte) },
                    { "Keterangan", x => x.keterangan },
                    { "Kodeh", x=>x.kodeH }
                };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            var data = new List<dynamic>();
            var responseData = response.Response?.data;
            _dynamicData = responseData;
            _data = responseData;
            _paginationData.Page = response.Response?.page;
            _paginationData.PageSize = response.Response?.pageSize;
            _paginationData.TotalRow = response.Response?.totalRow;
            _paginationData.TotalPage = response.Response?.totalPage;
            foreach (var x in responseData)
            {
                data.Add(x);
            }
            _dataTable = _dataTableGen.CreateDataTable<dynamic>(data);
        }

        public dynamic GetDynamicData()
        {
            return _dynamicData;
        }

        public PaginationData GetPaginationData()
        {
            return _paginationData;
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

    internal class PaginationData
    {
        public long TotalPage { get; set; } = 0;
        public long TotalRow { get; set; } = 0;
        public long Page { get; set; } = 1;
        public long PageSize { get; set; } = 20;
    }
}