namespace DJCWebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RegisterExternalBindingModel
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Email>k__BackingField;

        [Required, Display(Name="Email")]
        public string Email { get; set; }
    }
}

