using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Dtos;
using DoranApp.Utils;

namespace DoranApp.Data
{
    enum OrderKirimMelaluiEnum
    {
        BELUM_DIISI = 0,
        DARAT = 1,
        UDARA = 2,
        LAUT = 3
    }

    internal class OrderData : AbstractData<dynamic>
    {
        protected PaginationResultDto _paginationData = new PaginationResultDto();

        public OrderData() : base()
        {
        }

        public OrderData(object query) : base(query)
        {
        }

        protected dynamic _dynamicData { get; set; }

        protected override string RelativeUrl()
        {
            return "order";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<dynamic>
            {
                { "Tanggal", x => x.tglorder, typeof(DateTime) },
                { "Nama", x => x.masterpelanggan?.nama + " - " + x.masterpelanggan?.lokasiKota?.nama },
                { "Jumlah", x => x.jumlah },
                { "Sales", x => x.sales?.nama },
                { "Penyiap", x => x.penyiaporder?.nama },
                { "PPN", x => x.ppn },
                { "Ekspedisi", x => x.ekspedisi?.nama ?? "Belum Diisi" },
                { "Info Penting", x => x.infopenting },
                { "No Seri OL", x => x.noSeriOnline },
                { "Oleh", x => x.masteruserInsert?.usernameku },
                { "Keterangan", x => x.keterangan },
                { "Kodeh", x => x.kodeh }
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            var data = new List<dynamic>();
            var responseData = response.Response?.data;

            _data = null;
            _dynamicData = null;
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

        public async Task<TReturn> UpdateHeader(string primaryKeyValue, object data)
        {
            var uri = $"{RelativeUrl()}/{primaryKeyValue}/header";

            var rest = new Rest(uri);
            try
            {
                var response = await rest.Put(data);
                return response;
            }
            catch (Exception ex)
            {
                ConsoleDump.Extensions.Dump(ex, "ERRRR");
                throw;
            }
        }

        public async Task<dynamic> SetPenyiapOrder(long kode, int kodepenyiap)
        {
            Rest rest = new Rest($"{RelativeUrl()}/{kode}/set-penyiap");
            var response = await rest.Put(new
            {
                kodepenyiap = kodepenyiap
            });
            return response.Response;
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