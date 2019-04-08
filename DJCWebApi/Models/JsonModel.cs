namespace DJCWebApi.Models
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class JsonModel : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <msg>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <success>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool <userchoice>k__BackingField;

        public string msg { get; set; }

        public bool success { get; set; }

        public bool userchoice { get; set; }
    }
}

