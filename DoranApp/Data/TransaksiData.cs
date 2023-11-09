using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    public class TransaksiData : AbstractData<dynamic>
    {
        // protected PaginationResultDto _paginationData = new PaginationResultDto();

        public TransaksiData() : base()
        {
        }

        public TransaksiData(object query) : base(query)
        {
        }

        protected dynamic _dynamicData { get; set; }

        protected override string RelativeUrl()
        {
            return "transaksi";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<dynamic>
            {
                { "Gudang", x => x.mastergudang?.nama },
                { "No Nota", x => x.kodenota },
                { "Tanggal", x => x.tglTrans, typeof(DateTime) },
                { "Nama", x => x.masterpelanggan?.nama + " - " + x.masterpelanggan?.lokasiKota?.nama },
                { "Jumlah", x => x.jumlah },
                { "Sales", x => x.sales?.nama },
                { "Tipe", x => x.tipetempo == 0 ? "CASH!!!" : "TEMPO" },
                { "Keterangan", x => x.keterangan },
                { "Kodeh", x => x.kodeH }
            };

            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            var data = new List<dynamic>();
            // var responseData = response.Response?.data;
            var responseData = response.Response;
            _data = null;
            _dynamicData = null;
            _dynamicData = responseData;
            _data = responseData;
            // _paginationData.Page = response.Response?.page;
            // _paginationData.PageSize = response.Response?.pageSize;
            // _paginationData.TotalRow = response.Response?.totalRow;
            // _paginationData.TotalPage = response.Response?.totalPage;
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

        // public PaginationResultDto GetPaginationData()
        // {
        //     return _paginationData;
        // }

        public async Task<HtransResult> GetByKodeh(int kodeh)
        {
            var res = await _CLient.Get_Transaksi_By_KodehAsync(kodeh);
            return res;
        }

        public async Task<NotaTransaksiPpnResultDto> GetNotaPpn(int kodeh)
        {
            var res = await _CLient.Get_Nota_Transaksi_PpnAsync(kodeh);
            return res;
        }


        public async Task<TReturn> GetNameAndKodeOnly()
        {
            Rest rest = new Rest($"{RelativeUrl()}/nama");
            return await rest.Get(new
            {
                brgAktif = true
            });
        }

        public async Task<Htrans> CancelOrder(int kodeh)
        {
            var response = await _CLient.Cancel_TransaksiAsync(kodeh);
            return response;
        }
    }
}