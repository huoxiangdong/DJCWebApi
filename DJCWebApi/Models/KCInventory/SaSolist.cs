namespace DJCWebApi.Models.KCInventory
{
    using DJCWebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SaSolist : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <orderno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <orderdt>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <custno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <pfix>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <cusname>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <corderno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <autoid>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <mcode>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <qty>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <ckqty>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <sprc>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <sofinrate>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<SaSoProdStatus> <prodstatus>k__BackingField;

        public string orderno { get; set; }

        public string orderdt { get; set; }

        public string custno { get; set; }

        public string pfix { get; set; }

        public string cusname { get; set; }

        public string corderno { get; set; }

        public string autoid { get; set; }

        public string mcode { get; set; }

        public decimal qty { get; set; }

        public decimal ckqty { get; set; }

        public string sprc { get; set; }

        public decimal sofinrate { get; set; }

        public List<SaSoProdStatus> prodstatus { get; set; }
    }
}

