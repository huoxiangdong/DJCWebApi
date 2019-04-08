namespace DJCWebApi.Models.stockout
{
    using DJCWebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class StockoutHeadModel : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <outno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <sourceid>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ddate>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <custno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <shortname>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<StockoutDetailModel> <Detail>k__BackingField;

        public string outno { get; set; }

        public string sourceid { get; set; }

        public string ddate { get; set; }

        public string custno { get; set; }

        public string shortname { get; set; }

        public List<StockoutDetailModel> Detail { get; set; }
    }
}

