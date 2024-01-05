using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Dtos;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class LaporanTransaksiData : AbstractData<dynamic>
    {
        protected PaginationResultDto _paginationData = new PaginationResultDto();
        protected HtransTotalDto _total = new HtransTotalDto();

        public LaporanTransaksiData() : base()
        {
        }

        public LaporanTransaksiData(object query) : base(query)
        {
        }

        protected dynamic _dynamicData { get; set; }

        protected override string RelativeUrl()
        {
            return "laporan/transaksipenjualan";
        }

        private string TipeTempo(int day)
        {
            switch (day)
            {
                case 0:
                    return "Cash";
                case 1:
                    return "1 Hari";
                case 7:
                    return "1 Minggu";
                case 14:
                    return "2 Minggu";
                case 30:
                    return "1 Bulan";
                case 60:
                    return "2 Bulan";
                default:
                    return $"{day.ToString()} Hari";
            }

            return "Cash";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new ColumnSettings<dynamic>
            {
                { "Tanggal", x => x.tglTrans, typeof(DateTime) },
                { "Nama", x => x.masterpelanggan?.nama + " - " + x.masterpelanggan?.lokasiKota?.nama },
                { "Jumlah", x => x.jumlah },
                { "Lainnya", x => x.tambahanLainnya },
                { "Sales", x => x.sales?.nama },
                { "Lunas", x => Convert.ToSByte(x.lunas), typeof(Boolean) },
                { "No Nota", x => x.kodenota },
                { "Tipe", x => TipeTempo(Convert.ToInt32(x.tipetempo)) },
                { "Jatuh Tempo", x => x.tgltempo, typeof(DateTime) },
                { "Gudang", x => x.mastergudang?.nama ?? "" },
                { "DPP", x => x.dpp },
                { "Faktur PPN", x => x.ppn },
                { "PPN 100%", x => x.ppnreal },
                { "Diskon", x => x.diskon },
                { "TglPPN", x => x.tglPpn, typeof(DateTime) },
                { "Tanggal Input", x => x.insertTime, typeof(DateTime) },
                { "Keterangan", x => x.keterangan },
                { "Seri OL", x => x.noSeriOnline },
                { "Barcode OL", x => x.barcodeonline },
                { "K", x => x.jumlahkomisi },
                { "KodeP", x => x.kodepelanggan },
                { "Point Toko", x => x.poinToko },
                { "Retur", x => Convert.ToSByte(x.retur), typeof(Boolean) },
                { "Stok Nota", x => x.stoknota, typeof(Boolean) },
                { "U", x => x.untung },
                { "Untung Blm Pot OL", x => x.untungbelumpotOl },
                { "Kodeh", x => x.kodeH },
                { "Tgl Lapor PPN", x => x.tglLaporPpn },
                { "Jurnal", x => Convert.ToSByte(x.akanDiJurnalkan), typeof(Boolean) },
                { "NPWP", x => x.masterpelanggan?.npwp }
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
            ConsoleDump.Extensions.Dump(_dynamicData);

            _total.Total = response.Response?.total?.total ?? 0;
            _total.Komisi = response.Response?.total?.komisi ?? 0;
            _total.Untung = response.Response?.total?.untung ?? 0;
            _total.UntungbelumpotOl = response.Response?.total?.untungbelumpotOl ?? 0;
            _total.PpnFull = response.Response?.total?.ppnFull ?? 0;
            _total.Ppn = response.Response?.total?.ppn ?? 0;
            _total.DppFull = response.Response?.total?.dppFull ?? 0;
            _total.Dpp = response.Response?.total?.dpp ?? 0;
            _total.Jumlahbarangbiaya = response.Response?.total?.jumlahbarangbiaya ?? 0;
            _total.Diskon = response.Response?.total?.diskon ?? 0;
            _total.TotalOmzetPPN = response.Response?.total?.totalOmzetPPN ?? 0;
            _total.TambahanLainnya = response.Response?.total?.tambahanLainnya ?? 0;


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

        public async Task<List<GetLaporanTransaksiPenjualanGroupTokoDto>> GetLaporanTransaksiPenjualanGroupToko(
            dynamic query)
        {
            Rest rest = new Rest(RelativeUrl() + "/group-by-toko");
            TReturn<List<GetLaporanTransaksiPenjualanGroupTokoDto>> response =
                await rest.Get<List<GetLaporanTransaksiPenjualanGroupTokoDto>>(query);
            return response.Response;
        }

        public async Task<List<LaporanTransaksiPenjualanGroupKotaDto>> GetLaporanTransaksiPenjualanGroupKota(
            dynamic query)
        {
            Rest rest = new Rest(RelativeUrl() + "/group-by-kota");
            TReturn<List<LaporanTransaksiPenjualanGroupKotaDto>> response =
                await rest.Get<List<LaporanTransaksiPenjualanGroupKotaDto>>(query);
            return response.Response;
        }

        public async Task<List<LaporanTransaksiPenjualanGroupProvinsiDto>> GetLaporanTransaksiPenjualanGroupProvinsi(
            dynamic query)
        {
            Rest rest = new Rest(RelativeUrl() + "/group-by-provinsi");
            TReturn<List<LaporanTransaksiPenjualanGroupProvinsiDto>> response =
                await rest.Get<List<LaporanTransaksiPenjualanGroupProvinsiDto>>(query);
            return response.Response;
        }

        public dynamic GetDynamicData()
        {
            return _dynamicData;
        }

        public PaginationResultDto GetPaginationData()
        {
            return _paginationData;
        }

        public HtransTotalDto GetTotalData()
        {
            return _total;
        }
    }
}