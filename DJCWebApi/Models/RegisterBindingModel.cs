namespace DJCWebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class RegisterBindingModel
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Email>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Password>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ConfirmPassword>k__BackingField;

        [Required, Display(Name="Email")]
        public string Email { get; set; }

        [Required, StringLength(100, ErrorMessage="The {0} must be at least {2} characters long.", MinimumLength=6), DataType((DataType) DataType.Password), Display(Name="Password")]
        public string Password { get; set; }

        [DataType((DataType) DataType.Password), Display(Name="Confirm password"), Compare("Password", ErrorMessage="The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

