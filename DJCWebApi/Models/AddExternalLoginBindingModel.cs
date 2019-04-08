namespace DJCWebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class AddExternalLoginBindingModel
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ExternalAccessToken>k__BackingField;

        [Required, Display(Name="External access token")]
        public string ExternalAccessToken { get; set; }
    }
}

