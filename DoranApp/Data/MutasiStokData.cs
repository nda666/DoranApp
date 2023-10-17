using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleDump;
using DoranApp.Utils;

namespace DoranApp.Data;

public class MutasiStokData : AbstractData<GetMutasiResultDto>
{
    protected Client client = new Client();

    public MutasiStokData() : base()
    {
    }

    public MutasiStokData(object query) : base(query)
    {
    }

    protected GetMutasiResultDto _dynamicData { get; set; }

    protected override string RelativeUrl()
    {
        return "mutasi";
    }

    protected override List<ColumnSettings> ColumnSettings()
    {
        var columnSettingsList = new ColumnSettings<GetMutasiResultDto>
        {
            { "Tanggal", x => x.Tanggal, typeof(DateTime) },
            { "Keterangan", x => x.Keterangan },
            { "Nama Toko", x => x.Oleh },
            { "Harga", x => x.Harga },
            { "Jumlah", x => x.Jumlah },
            { "Saldo", x => x.Saldo },
            { "Kode BM", x => x.KodEnya },
            { "Kodes", x => x.KodeSupplier },
            { "Kode Barang", x => x.KodeBarang },
            { "Historinya", x => x.History },
            { "Lunas", x => x.Lunas },
            { "Indexnya", x => x.Indexnya }
        };

        return columnSettingsList;
    }

    public async Task<GetStokResponseDto> GetStokByBarangAndGudang(int kodebarang, int kodegudang)
    {
        var response = await _CLient.Get_Stok_By_Barang_And_GudangAsync(kodebarang, kodegudang);
        return response;
    }

    protected override async Task RunRefresh()
    {
        List<GetMutasiResultDto> response = await _CLient.Get_MutasiAsync(_query.KodeBarang, _query.Kodegudang);
        _data = null;
        _data = response;
        var data = new List<GetMutasiResultDto>();
        var oldSaldo = 0;
        foreach (var x in response)
        {
            oldSaldo += x.Jumlah;
            x.Saldo = oldSaldo;
            x.Saldo.Dump();
            data.Add(x);
        }

        _dataTable = null;
        _dataTable = _dataTableGen.CreateDataTable(data);
    }

    public dynamic GetDynamicData()
    {
        return _dynamicData;
    }
}