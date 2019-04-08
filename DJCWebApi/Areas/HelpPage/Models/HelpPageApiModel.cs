namespace DJCWebApi.Areas.HelpPage.Models
{
    using DJCWebApi.Areas.HelpPage.ModelDescriptions;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Web.Http.Description;

    public class HelpPageApiModel
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private System.Web.Http.Description.ApiDescription <ApiDescription>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Collection<ParameterDescription> <UriParameters>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <RequestDocumentation>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ModelDescription <RequestModelDescription>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ModelDescription <ResourceDescription>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<MediaTypeHeaderValue, object> <SampleRequests>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<MediaTypeHeaderValue, object> <SampleResponses>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Collection<string> <ErrorMessages>k__BackingField;

        public HelpPageApiModel()
        {
            this.UriParameters = new Collection<ParameterDescription>();
            this.SampleRequests = new Dictionary<MediaTypeHeaderValue, object>();
            this.SampleResponses = new Dictionary<MediaTypeHeaderValue, object>();
            this.ErrorMessages = new Collection<string>();
        }

        private static IList<ParameterDescription> GetParameterDescriptions(ModelDescription modelDescription)
        {
            ComplexTypeModelDescription elementDescription = modelDescription as ComplexTypeModelDescription;
            if (elementDescription > null)
            {
                return elementDescription.Properties;
            }
            CollectionModelDescription description2 = modelDescription as CollectionModelDescription;
            if (description2 > null)
            {
                elementDescription = description2.ElementDescription as ComplexTypeModelDescription;
                if (elementDescription > null)
                {
                    return elementDescription.Properties;
                }
            }
            return null;
        }

        public System.Web.Http.Description.ApiDescription ApiDescription { get; set; }

        public Collection<ParameterDescription> UriParameters { get; private set; }

        public string RequestDocumentation { get; set; }

        public ModelDescription RequestModelDescription { get; set; }

        public IList<ParameterDescription> RequestBodyParameters =>
            GetParameterDescriptions(this.RequestModelDescription);

        public ModelDescription ResourceDescription { get; set; }

        public IList<ParameterDescription> ResourceProperties =>
            GetParameterDescriptions(this.ResourceDescription);

        public IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

        public IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

        public Collection<string> ErrorMessages { get; private set; }
    }
}

