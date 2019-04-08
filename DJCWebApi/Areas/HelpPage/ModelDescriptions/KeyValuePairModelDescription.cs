namespace DJCWebApi.Areas.HelpPage.ModelDescriptions
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class KeyValuePairModelDescription : ModelDescription
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ModelDescription <KeyModelDescription>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ModelDescription <ValueModelDescription>k__BackingField;

        public ModelDescription KeyModelDescription { get; set; }

        public ModelDescription ValueModelDescription { get; set; }
    }
}

