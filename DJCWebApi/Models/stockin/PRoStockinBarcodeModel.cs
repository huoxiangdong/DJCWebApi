namespace DJCWebApi.Models.stockin
{
    using DJCWebApi.Models;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class PRoStockinBarcodeModel : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Mcode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Custno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Mname>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <Zhoushu>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Storageno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Barcode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Inno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <Qty>k__BackingField;

        public string Mcode { get; set; }

        public string Custno { get; set; }

        public string Mname { get; set; }

        public int Zhoushu { get; set; }

        public string Storageno { get; set; }

        public string Barcode { get; set; }

        public string Inno { get; set; }

        public decimal Qty { get; set; }
    }
}

