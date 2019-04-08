namespace DJCWebApi.Models.workfloor
{
    using DJCWebApi.Models;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class QCBadItemModel : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <itemno>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <itemname>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <datachecked>k__BackingField = false;

        public string itemno { get; set; }

        public string itemname { get; set; }

        public bool datachecked { get; set; }
    }
}

