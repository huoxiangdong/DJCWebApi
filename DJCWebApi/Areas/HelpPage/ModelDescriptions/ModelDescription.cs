namespace DJCWebApi.Areas.HelpPage.ModelDescriptions
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public abstract class ModelDescription
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Documentation>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Type <ModelType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Name>k__BackingField;

        protected ModelDescription()
        {
        }

        public string Documentation { get; set; }

        public Type ModelType { get; set; }

        public string Name { get; set; }
    }
}

