namespace DJCWebApi.Areas.HelpPage
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web.Http.Description;
    using System.Xml.Linq;

    public class HelpPageSampleGenerator
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<HelpPageSampleKey, Type> <ActualHttpMessageTypes>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<HelpPageSampleKey, object> <ActionSamples>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IDictionary<Type, object> <SampleObjects>k__BackingField;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private IList<Func<HelpPageSampleGenerator, Type, object>> <SampleObjectFactories>k__BackingField;

        public HelpPageSampleGenerator()
        {
            this.ActualHttpMessageTypes = new Dictionary<HelpPageSampleKey, Type>();
            this.ActionSamples = new Dictionary<HelpPageSampleKey, object>();
            this.SampleObjects = new Dictionary<Type, object>();
            List<Func<HelpPageSampleGenerator, Type, object>> list = new List<Func<HelpPageSampleGenerator, Type, object>> {
                new Func<HelpPageSampleGenerator, Type, object>(HelpPageSampleGenerator.DefaultSampleObjectFactory)
            };
            this.SampleObjectFactories = list;
        }

        private static object DefaultSampleObjectFactory(HelpPageSampleGenerator sampleGenerator, Type type)
        {
            ObjectGenerator generator = new ObjectGenerator();
            return generator.GenerateObject(type);
        }

        public virtual object GetActionSample(string controllerName, string actionName, IEnumerable<string> parameterNames, Type type, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, SampleDirection sampleDirection)
        {
            string[] textArray1;
            if (!this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, sampleDirection, controllerName, actionName, parameterNames), out object obj2))
            {
                textArray1 = new string[] { "*" };
            }
            if ((this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, sampleDirection, controllerName, actionName, textArray1), out obj2) || this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, type), out obj2)) || this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType), out obj2))
            {
                return obj2;
            }
            return null;
        }

        [IteratorStateMachine(typeof(<GetAllActionSamples>d__30))]
        private IEnumerable<KeyValuePair<HelpPageSampleKey, object>> GetAllActionSamples(string controllerName, string actionName, IEnumerable<string> parameterNames, SampleDirection sampleDirection)
        {
            HashSet<string> <parameterNamesSet>5__1 = new HashSet<string>(parameterNames, StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<HelpPageSampleKey, object> <sample>5__3 in this.ActionSamples)
            {
                string[] textArray1;
                SampleDirection? nullable;
                HelpPageSampleKey key = <sample>5__3.Key;
                if (string.Equals(controllerName, key.ControllerName, StringComparison.OrdinalIgnoreCase) && string.Equals(actionName, key.ActionName, StringComparison.OrdinalIgnoreCase))
                {
                    textArray1 = new string[] { "*" };
                }
                if ((key.ParameterNames.SetEquals(textArray1) || <parameterNamesSet>5__1.SetEquals(key.ParameterNames)) && ((sampleDirection == ((SampleDirection) (nullable = key.SampleDirection).GetValueOrDefault())) ? nullable.HasValue : false))
                {
                    yield return <sample>5__3;
                }
                key = null;
                <sample>5__3 = new KeyValuePair<HelpPageSampleKey, object>();
            }
        }

        public virtual IDictionary<MediaTypeHeaderValue, object> GetSample(ApiDescription api, SampleDirection sampleDirection)
        {
            if (api == null)
            {
                throw new ArgumentNullException("api");
            }
            string controllerName = api.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = api.ActionDescriptor.ActionName;
            IEnumerable<string> parameterNames = from p in api.ParameterDescriptions select p.Name;
            Type c = this.ResolveType(api, controllerName, actionName, parameterNames, sampleDirection, out Collection<MediaTypeFormatter> collection);
            Dictionary<MediaTypeHeaderValue, object> dictionary = new Dictionary<MediaTypeHeaderValue, object>();
            IEnumerable<KeyValuePair<HelpPageSampleKey, object>> enumerable2 = this.GetAllActionSamples(controllerName, actionName, parameterNames, sampleDirection);
            foreach (KeyValuePair<HelpPageSampleKey, object> pair in enumerable2)
            {
                dictionary.Add(pair.Key.MediaType, WrapSampleIfString(pair.Value));
            }
            if ((c != null) && !typeof(HttpResponseMessage).IsAssignableFrom(c))
            {
                object sampleObject = this.GetSampleObject(c);
                foreach (MediaTypeFormatter formatter in collection)
                {
                    foreach (MediaTypeHeaderValue value2 in formatter.SupportedMediaTypes)
                    {
                        if (!dictionary.ContainsKey(value2))
                        {
                            object sample = this.GetActionSample(controllerName, actionName, parameterNames, c, formatter, value2, sampleDirection);
                            if ((sample == null) && (sampleObject > null))
                            {
                                sample = this.WriteSampleObjectUsingFormatter(formatter, sampleObject, c, value2);
                            }
                            dictionary.Add(value2, WrapSampleIfString(sample));
                        }
                    }
                }
            }
            return dictionary;
        }

        public virtual object GetSampleObject(Type type)
        {
            if (!this.SampleObjects.TryGetValue(type, out object obj2))
            {
                foreach (Func<HelpPageSampleGenerator, Type, object> func in this.SampleObjectFactories)
                {
                    if (func != null)
                    {
                        try
                        {
                            obj2 = func(this, type);
                            if (obj2 > null)
                            {
                                return obj2;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return obj2;
        }

        public IDictionary<MediaTypeHeaderValue, object> GetSampleRequests(ApiDescription api) => 
            this.GetSample(api, SampleDirection.Request);

        public IDictionary<MediaTypeHeaderValue, object> GetSampleResponses(ApiDescription api) => 
            this.GetSample(api, SampleDirection.Response);

        private static bool IsFormatSupported(SampleDirection sampleDirection, MediaTypeFormatter formatter, Type type)
        {
            switch (sampleDirection)
            {
                case SampleDirection.Request:
                    return formatter.CanReadType(type);

                case SampleDirection.Response:
                    return formatter.CanWriteType(type);
            }
            return false;
        }

        public virtual Type ResolveHttpRequestMessageType(ApiDescription api)
        {
            string controllerName = api.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = api.ActionDescriptor.ActionName;
            IEnumerable<string> parameterNames = from p in api.ParameterDescriptions select p.Name;
            return this.ResolveType(api, controllerName, actionName, parameterNames, SampleDirection.Request, out _);
        }

        public virtual Type ResolveType(ApiDescription api, string controllerName, string actionName, IEnumerable<string> parameterNames, SampleDirection sampleDirection, out Collection<MediaTypeFormatter> formatters)
        {
            if (!Enum.IsDefined(typeof(SampleDirection), sampleDirection))
            {
                throw new InvalidEnumArgumentException("sampleDirection", (int) sampleDirection, typeof(SampleDirection));
            }
            if (api == null)
            {
                throw new ArgumentNullException("api");
            }
            if (this.ActualHttpMessageTypes.TryGetValue(new HelpPageSampleKey(sampleDirection, controllerName, actionName, parameterNames), out Type parameterType) || this.ActualHttpMessageTypes.TryGetValue(new HelpPageSampleKey(sampleDirection, controllerName, actionName, new string[] { "*" }), out parameterType))
            {
                Collection<MediaTypeFormatter> collection = new Collection<MediaTypeFormatter>();
                foreach (MediaTypeFormatter formatter in api.ActionDescriptor.Configuration.Formatters)
                {
                    if (IsFormatSupported(sampleDirection, formatter, parameterType))
                    {
                        collection.Add(formatter);
                    }
                }
                formatters = collection;
                return parameterType;
            }
            switch (sampleDirection)
            {
                case SampleDirection.Request:
                {
                    ApiParameterDescription description = api.ParameterDescriptions.FirstOrDefault<ApiParameterDescription>(p => p.Source == ApiParameterSource.FromBody);
                    parameterType = description?.ParameterDescriptor.ParameterType;
                    formatters = api.SupportedRequestBodyFormatters;
                    return parameterType;
                }
            }
            parameterType = api.ResponseDescription.ResponseType ?? api.ResponseDescription.DeclaredType;
            formatters = api.SupportedResponseFormatters;
            return parameterType;
        }

        private static string TryFormatJson(string str)
        {
            try
            {
                return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(str), Formatting.Indented);
            }
            catch
            {
                return str;
            }
        }

        private static string TryFormatXml(string str)
        {
            try
            {
                return XDocument.Parse(str).ToString();
            }
            catch
            {
                return str;
            }
        }

        internal static Exception UnwrapException(Exception exception)
        {
            AggregateException exception2 = exception as AggregateException;
            if (exception2 > null)
            {
                return exception2.Flatten().InnerException;
            }
            return exception;
        }

        private static object WrapSampleIfString(object sample)
        {
            string text = sample as string;
            if (text > null)
            {
                return new TextSample(text);
            }
            return sample;
        }

        public virtual object WriteSampleObjectUsingFormatter(MediaTypeFormatter formatter, object value, Type type, MediaTypeHeaderValue mediaType)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException("formatter");
            }
            if (mediaType == null)
            {
                throw new ArgumentNullException("mediaType");
            }
            object obj2 = string.Empty;
            MemoryStream writeStream = null;
            HttpContent content = null;
            try
            {
                if (formatter.CanWriteType(type))
                {
                    writeStream = new MemoryStream();
                    content = new ObjectContent(type, value, formatter, mediaType);
                    formatter.WriteToStreamAsync(type, value, writeStream, content, null).Wait();
                    writeStream.Position = 0L;
                    string str = new StreamReader(writeStream).ReadToEnd();
                    if (mediaType.MediaType.ToUpperInvariant().Contains("XML"))
                    {
                        str = TryFormatXml(str);
                    }
                    else if (mediaType.MediaType.ToUpperInvariant().Contains("JSON"))
                    {
                        str = TryFormatJson(str);
                    }
                    return new TextSample(str);
                }
                object[] args = new object[] { mediaType, formatter.GetType().Name, type.Name };
                return new InvalidSample(string.Format(CultureInfo.CurrentCulture, "Failed to generate the sample for media type '{0}'. Cannot use formatter '{1}' to write type '{2}'.", args));
            }
            catch (Exception exception)
            {
                object[] args = new object[] { formatter.GetType().Name, mediaType.MediaType, UnwrapException(exception).Message };
                obj2 = new InvalidSample(string.Format(CultureInfo.CurrentCulture, "An exception has occurred while using the formatter '{0}' to generate sample for media type '{1}'. Exception message: {2}", args));
            }
            finally
            {
                if (writeStream > null)
                {
                    writeStream.Dispose();
                }
                if (content > null)
                {
                    content.Dispose();
                }
            }
            return obj2;
        }

        public IDictionary<HelpPageSampleKey, Type> ActualHttpMessageTypes { get; internal set; }

        public IDictionary<HelpPageSampleKey, object> ActionSamples { get; internal set; }

        public IDictionary<Type, object> SampleObjects { get; internal set; }

        public IList<Func<HelpPageSampleGenerator, Type, object>> SampleObjectFactories { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HelpPageSampleGenerator.<>c <>9 = new HelpPageSampleGenerator.<>c();
            public static Func<ApiParameterDescription, string> <>9__19_0;
            public static Func<ApiParameterDescription, string> <>9__22_0;
            public static Func<ApiParameterDescription, bool> <>9__23_0;

            internal string <GetSample>b__19_0(ApiParameterDescription p) => 
                p.Name;

            internal string <ResolveHttpRequestMessageType>b__22_0(ApiParameterDescription p) => 
                p.Name;

            internal bool <ResolveType>b__23_0(ApiParameterDescription p) => 
                (p.Source == ApiParameterSource.FromBody);
        }

    }
}

