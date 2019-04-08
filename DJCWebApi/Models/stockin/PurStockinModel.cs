namespace DJCWebApi.Models.stockin
{
    using DJCWebApi.Models;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PurStockinModel : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <storageno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <mcode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <custno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <mname>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <barcode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <zhoubatch>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <coptypeno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <zhoushu>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <qty>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <purqty>k__BackingField;

        public string storageno { get; set; }

        public string mcode { get; set; }

        public string custno { get; set; }

        public string mname { get; set; }

        public string barcode { get; set; }

        public string zhoubatch { get; set; }

        public string coptypeno { get; set; }

        public int zhoushu { get; set; }

        public decimal qty { get; set; }

        public decimal purqty { get; set; }
    }
}

