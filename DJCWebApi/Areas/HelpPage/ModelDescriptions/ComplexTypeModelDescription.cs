namespace DJCWebApi.Areas.HelpPage.ModelDescriptions
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ComplexTypeModelDescription : ModelDescription
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Collection<ParameterDescription> <Properties>k__BackingField;

        public ComplexTypeModelDescription()
        {
            this.Properties = new Collection<ParameterDescription>();
        }

        public Collection<ParameterDescription> Properties { get; private set; }
    }
}

