namespace DJCWebApi.Models.stockout
{
    using DJCWebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class StockoutModel : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <custno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <shortname>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<StockoutHeadModel> <Stockout>k__BackingField;

        public string custno { get; set; }

        public string shortname { get; set; }

        public List<StockoutHeadModel> Stockout { get; set; }
    }
}

