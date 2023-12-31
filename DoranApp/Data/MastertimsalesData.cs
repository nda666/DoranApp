﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DoranApp.Utils;

namespace DoranApp.Data
{
    internal class MastertimsalesData : AbstractData<Mastertimsales>
    {
        public MastertimsalesData() : base()
        {
        }

        public MastertimsalesData(object query) : base(query)
        {
        }

        protected override string RelativeUrl()
        {
            return "mastertimsales";
        }

        protected override List<ColumnSettings> ColumnSettings()
        {
            var columnSettingsList = new List<ColumnSettings>
            {
                new ColumnSettings("Kode", item => ((Mastertimsales)item).Kode),
                new ColumnSettings("Nama", item => ((Mastertimsales)item).Nama),
                new ColumnSettings("Nama Channel", item => ((Mastertimsales)item).Masterchannelsales?.Nama),
                new ColumnSettings("Targetjete", item => ((Mastertimsales)item).Targetjete),
                new ColumnSettings("Targetomzet", item => ((Mastertimsales)item).Targetomzet),
                new ColumnSettings("Tampiltahunlalu", item => ((Mastertimsales)item).Tampiltahunlalu),
                new ColumnSettings("SyaratKomisi", item => ((Mastertimsales)item).SyaratKomisi),
                new ColumnSettings("Kodechannel", item => ((Mastertimsales)item).SyaratKomisi)
            };
            return columnSettingsList;
        }

        protected override async Task RunRefresh()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(_query);
            _data = response.Response;
            _dataTableGen.CreateDataTable<Mastertimsales>(_data);
        }

        public async Task<List<Mastertimsales>> GetWithSales()
        {
            Rest rest = new Rest(RelativeUrl());
            var response = await rest.Get(new
            {
                active = true,
                withSales = true
            });
            return (List<Mastertimsales>)response.Response;
        }
    }
}