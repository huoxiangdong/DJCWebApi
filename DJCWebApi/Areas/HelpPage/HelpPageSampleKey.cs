namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;

    public class HelpPageSampleKey
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ControllerName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ActionName>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MediaTypeHeaderValue <MediaType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private HashSet<string> <ParameterNames>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Type <ParameterType>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private DJCWebApi.Areas.HelpPage.SampleDirection? <SampleDirection>k__BackingField;

        public HelpPageSampleKey(MediaTypeHeaderValue mediaType)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }
            this.ActionName = string.Empty;
            this.ControllerName = string.Empty;
            this.MediaType = mediaType;
            this.ParameterNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, Type type) : this(mediaType)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            this.ParameterType = type;
        }

        public HelpPageSampleKey(DJCWebApi.Areas.HelpPage.SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames)
        {
            if (!Enum.IsDefined(typeof(DJCWebApi.Areas.HelpPage.SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int) sampleDirection, typeof(DJCWebApi.Areas.HelpPage.SampleDirection));
            }
            if (controllerName == null)
            {
                throw new ArgumentNullException("controllerName");
            }
            if (actionName == null)
            {
                throw new ArgumentNullException("actionName");
            }
            if (parameterNames == null)
            {
                throw new ArgumentNullException("parameterNames");
            }
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.ParameterNames = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            this.SampleDirection = new DJCWebApi.Areas.HelpPage.SampleDirection?(sampleDirection);
        }

        public HelpPageSampleKey(MediaTypeHeaderValue mediaType, DJCWebApi.Areas.HelpPage.SampleDirection sampleDirection, string controllerName, string actionName, IEnumerable<string> parameterNames) : this(sampleDirection, controllerName, actionName, parameterNames)
        {
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }
            this.MediaType = mediaType;
        }

        public override bool Equals(object obj)
        {
            DJCWebApi.Areas.HelpPage.SampleDirection? sampleDirection;
            DJCWebApi.Areas.HelpPage.SampleDirection? nullable2;
            HelpPageSampleKey key = obj as HelpPageSampleKey;
            if (key == null)
            {
                return false;
            }
            if (((string.Equals(this.ControllerName, key.ControllerName, StringComparison.OrdinalIgnoreCase) && string.Equals(this.ActionName, key.ActionName, StringComparison.OrdinalIgnoreCase)) && ((this.MediaType == key.MediaType) || ((this.MediaType != null) && this.MediaType.Equals(key.MediaType)))) && (this.ParameterType == key.ParameterType))
            {
                sampleDirection = this.SampleDirection;
                nullable2 = key.SampleDirection;
            }
            return (((sampleDirection.GetValueOrDefault() == nullable2.GetValueOrDefault()) ? (sampleDirection.HasValue == nullable2.HasValue) : false) && this.ParameterNames.SetEquals(key.ParameterNames));
        }

        public override int GetHashCode()
        {
            int num = this.ControllerName.ToUpperInvariant().GetHashCode() ^ this.ActionName.ToUpperInvariant().GetHashCode();
            if (this.MediaType > null)
            {
                num ^= this.MediaType.GetHashCode();
            }
            if (this.SampleDirection.HasValue)
            {
                num ^= this.SampleDirection.GetHashCode();
            }
            if (this.ParameterType != null)
            {
                num ^= this.ParameterType.GetHashCode();
            }
            foreach (string str in this.ParameterNames)
            {
                num ^= str.ToUpperInvariant().GetHashCode();
            }
            return num;
        }

        public string ControllerName { get; private set; }

        public string ActionName { get; private set; }

        public MediaTypeHeaderValue MediaType { get; private set; }

        public HashSet<string> ParameterNames { get; private set; }

        public Type ParameterType { get; private set; }

        public DJCWebApi.Areas.HelpPage.SampleDirection? SampleDirection { get; private set; }
    }
}

