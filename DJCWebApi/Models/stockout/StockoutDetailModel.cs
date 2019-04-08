namespace DJCWebApi.Models.stockout
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class StockoutDetailModel
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <outno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <sourceid>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <autoid>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <mcode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <coptypeno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <sprc>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <mname>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <mnum>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <curunfqty>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <allselqty>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <curselqty>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <kcnum>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <kcdvlnum>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<StockoutListModel> <Batchcode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<StockoutStoreListModel> <Storelist>k__BackingField;

        public string outno { get; set; }

        public string sourceid { get; set; }

        public int autoid { get; set; }

        public string mcode { get; set; }

        public string coptypeno { get; set; }

        public string sprc { get; set; }

        public string mname { get; set; }

        public decimal mnum { get; set; }

        public decimal curunfqty { get; set; }

        public decimal allselqty { get; set; }

        public decimal curselqty { get; set; }

        public decimal dealnum =>
            decimal.Subtract(this.mnum, this.curunfqty);

        public decimal leftnum =>
            decimal.Subtract(this.curunfqty, this.curselqty);

        public decimal kcnum { get; set; }

        public decimal kcdvlnum { get; set; }

        public List<StockoutListModel> Batchcode { get; set; }

        public List<StockoutStoreListModel> Storelist { get; set; }
    }
}

