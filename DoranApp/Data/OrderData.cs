using System;
using System.Collections.Generic;
using System.Linq;
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

    public class OrderData : AbstractData<HorderResult>
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
            var columnSettingsList = new ColumnSettings<HorderResult>
            {
                { "Gudang", x => x.Mastergudang?.Nama },
                { "Tanggal", x => x.Tglorder, typeof(DateTime) },
                { "Nama", x => x.Masterpelanggan?.Nama + " - " + x.Masterpelanggan?.LokasiKota?.Nama },
                { "Jumlah", x => x.Jumlah },
                { "Sales", x => x.Sales?.Nama },
                { "Penyiap", x => x.Penyiaporder?.Nama },
                { "PPN", x => x.Ppn },
                { "Ekspedisi", x => x.Ekspedisi?.Nama ?? "Belum Diisi" },
                { "Info Penting", x => x.Infopenting },
                { "No Seri OL", x => x.NoSeriOnline },
                { "Oleh", x => x.MasteruserInsert?.Usernameku },
                { "Keterangan", x => x.Keterangan },
                { "Kodeh", x => x.Kodeh }
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            TReturn<HorderResultDto> response = await rest.Get<HorderResultDto>(_query);
            ConsoleDump.Extensions.Dump(response);
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

        public async Task<dynamic> SetPenyiapOrder(long kode, int kodepenyiap, string? passwordLimit)
        {
            Rest rest = new Rest($"{RelativeUrl()}/{kode}/set-penyiap");
            var response = await rest.Put(new
            {
                kodepenyiap,
                password = passwordLimit
            });
            return response.Response;
        }

        public async Task<HorderResult> FindUnprocesedOrderByKodeh(int kodeh)
        {
            var client = new Client();
            var result = await client.Find_OrderAsync(kodeh: kodeh, lunas: 0, checkTransaksi: true);
            return result;
        }

        public async Task<TReturn> GetNameAndKodeOnly()
        {
            Rest rest = new Rest($"{RelativeUrl()}/nama");
            return await rest.Get(new
            {
                brgAktif = true
            });
        }

        public async Task<HorderResult> FindByNoSeriOnlineNotLunas(string noSeriOnline, sbyte? historynya = null)
        {
            var resp = await _CLient.Find_OrderAsync(noSeriOnline: noSeriOnline, historynya: historynya, lunas: 0);
            return resp;
        }

        public async Task<HorderResult> FindByNoBarcodeOnlineNotLunas(string barcodeonline, sbyte? historynya = null)
        {
            var resp = await _CLient.Find_OrderAsync(barcodeonline: barcodeonline, historynya: historynya, lunas: 0);
            return resp;
        }

        public async Task TimOnlineCek(int kodeh)
        {
            await _CLient.Tim_Online_CekAsync(kodeh);
        }

        public async Task CancelOrderHeader(int kodeh)
        {
            try
            {
                await _CLient.Cancel_Order_HeaderAsync(kodeh);
            }
            catch (ApiException ex)
            {
                Helper.ShowErrorMessageFromResponse(ex);
            }
        }

        public async Task CancelOrderDetail(int kodeh, CancelOrderDetailDto dto)
        {
            await _CLient.Cancel_Order_DetailAsync(kodeh, dto);
        }
    }
}