namespace DJCWebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RemoveLoginBindingModel
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <LoginProvider>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ProviderKey>k__BackingField;

        [Required, Display(Name="Login provider")]
        public string LoginProvider { get; set; }

        [Required, Display(Name="Provider key")]
        public string ProviderKey { get; set; }
    }
}

