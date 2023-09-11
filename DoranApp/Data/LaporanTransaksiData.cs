using DoranApp.Models;
using DoranApp.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoranApp.Data
{
    internal class LaporanTransaksiData : AbstractData<dynamic>
    {
        protected dynamic _dynamicData { get; set; }
        protected PaginationData _paginationData = new PaginationData();

        public LaporanTransaksiData() : base()
        {
        }

        public LaporanTransaksiData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "transaksi";
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
                    { "Nama", x => x.masterpelanggan?.nama + " - " +  x.masterpelanggan?.lokasiKota?.nama },
                    { "Jumlah", x => x.jumlah },
                    { "Lainnya", x => x.tambahanLainnya },
                    { "Sales", x => x.sales?.nama },
                    { "Lunas", x => Convert.ToSByte(x.lunas), typeof(Boolean) },
                    { "No Nota", x => x.kodenota },
                    { "Tipe", x =>  TipeTempo( Convert.ToInt32(x.tipetempo))},
                    { "Gudang", x => x.mastergudang?.nama ?? ""},
                    {"DPP", x => x.dpp },
                     {"Faktur PPN", x => x.ppn },
                     {"PPN 100%", x => x.ppnreal },
                {"Tanggal Input", x => x.insertTime,  typeof(DateTime) },
                    { "Keterangan", x => x.keterangan },
                     { "Seri OL", x => x.noSeriOnline },
                     { "Barcode OL", x => x.barcodeonline },
                { "Retur", x => Convert.ToSByte(x.retur), typeof(Boolean) },
                    { "Kodeh", x=>x.kodeH },
                { "Jurnal", x => Convert.ToSByte(x.akanDiJurnalkan), typeof(Boolean) },
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


}