namespace DJCWebApi.Models
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class UserLoginInfoViewModel
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <LoginProvider>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ProviderKey>k__BackingField;

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}

