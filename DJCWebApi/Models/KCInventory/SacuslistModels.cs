namespace DJCWebApi.Models.KCInventory
{
    using DJCWebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SacuslistModels : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <value>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <label>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<Sacuslist2Models> <children>k__BackingField;

        public string value { get; set; }

        public string label { get; set; }

        public List<Sacuslist2Models> children { get; set; }
    }
}

