using System;
using System.Collections.Generic;

namespace DoranApp.Models
{
    public class Mastertimsales
    {
        //public Guid Id { get; set; }
        public int? Kode { get; set; }
        public string Nama { get; set; }
        public long Targetjete { get; set; }
        public long Targetomzet { get; set; }
        public bool Tampiltahunlalu { get; set; }
        public bool Aktif { get; set; }
        public bool SyaratKomisi { get; set; }
        public int Kodechannel { get; set; }
        //public DateTime? CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        //public DateTime? DeletedAt { get; set; }

        public virtual List<Sales> Sales { get; set; }
        public virtual Masterchannelsales Masterchannelsales { get; set; } = null;
    }

    public class MastertimsalesOption
    {
        //public Guid Id { get; set; }
        public int? Kode { get; set; }
        public string Nama { get; set; }
        public int Kodechannel { get; set; }
        public virtual List<SalesOption> Sales { get; set; } = new List<SalesOption>();
    }

}