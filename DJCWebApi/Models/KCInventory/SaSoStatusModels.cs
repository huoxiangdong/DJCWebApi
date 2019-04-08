namespace DJCWebApi.Models.KCInventory
{
    using DJCWebApi.Models;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SaSoStatusModels : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <yyyymm>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <mm>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private decimal <sorate>k__BackingField;

        public string yyyymm { get; set; }

        public string mm { get; set; }

        public decimal sorate { get; set; }
    }
}

