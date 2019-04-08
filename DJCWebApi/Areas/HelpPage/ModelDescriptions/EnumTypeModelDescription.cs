namespace DJCWebApi.Areas.HelpPage.ModelDescriptions
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class EnumTypeModelDescription : ModelDescription
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Collection<EnumValueDescription> <Values>k__BackingField;

        public EnumTypeModelDescription()
        {
            this.Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}

