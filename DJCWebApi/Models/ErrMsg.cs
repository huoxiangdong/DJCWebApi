namespace DJCWebApi.Models
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ErrMsg : Model
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int <HResult>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Source>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Message>k__BackingField;

        public int HResult { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }
    }
}

