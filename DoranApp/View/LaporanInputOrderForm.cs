using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoranApp.DataGlobal;
using DoranApp.Utils;

namespace DoranApp.View;

public partial class LaporanInputOrderForm : Form
{
    private List<MastergudangOptionDto> _MastergudangOptions = new List<MastergudangOptionDto>();

    public LaporanInputOrderForm()
    {
        InitializeComponent();
    }

    private async Task SubscribeMastergudang()
    {
        FetchMastergudangOption.Subscribe(data =>
        {
            _MastergudangOptions = data.Prepend(new MastergudangOptionDto()
            {
                Kode = null,
                Nama = "Semua Gudang"
            }).ToList();
            comboFilterGudang.DataSource = _MastergudangOptions;
        });
        FetchMastergudangOption.Run();
    }

    private void LaporanInputOrderForm_Load(object sender, EventArgs e)
    {
        SubscribeMastergudang();
    }
}