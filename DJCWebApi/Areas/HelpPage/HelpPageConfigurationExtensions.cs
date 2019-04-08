namespace DJCWebApi.Areas.HelpPage
{
    using DJCWebApi.Areas.HelpPage.ModelDescriptions;
    using DJCWebApi.Areas.HelpPage.Models;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;

    public static class HelpPageConfigurationExtensions
    {
        private const string ApiModelPrefix = "MS_HelpPageApiModel_";

        private static ParameterDescription AddParameterDescription(HelpPageApiModel apiModel, ApiParameterDescription apiParameter, ModelDescription typeDescription)
        {
            ParameterDescription item = new ParameterDescription {
                Name = apiParameter.Name,
                Documentation = apiParameter.Documentation,
                TypeDescription = typeDescription
            };
            apiModel.UriParameters.Add(item);
            return item;
        }

        private static HelpPageApiModel GenerateApiModel(ApiDescription apiDescription, HttpConfiguration config)
        {
            HelpPageApiModel apiModel = new HelpPageApiModel {
                ApiDescription = apiDescription
            };
            ModelDescriptionGenerator modelDescriptionGenerator = config.GetModelDescriptionGenerator();
            HelpPageSampleGenerator helpPageSampleGenerator = config.GetHelpPageSampleGenerator();
            GenerateUriParameters(apiModel, modelDescriptionGenerator);
            GenerateRequestModelDescription(apiModel, modelDescriptionGenerator, helpPageSampleGenerator);
            GenerateResourceDescription(apiModel, modelDescriptionGenerator);
            GenerateSamples(apiModel, helpPageSampleGenerator);
            return apiModel;
        }

        private static void GenerateRequestModelDescription(HelpPageApiModel apiModel, ModelDescriptionGenerator modelGenerator, HelpPageSampleGenerator sampleGenerator)
        {
            ApiDescription apiDescription = apiModel.ApiDescription;
            foreach (ApiParameterDescription description2 in apiDescription.ParameterDescriptions)
            {
                if (description2.Source == ApiParameterSource.FromBody)
                {
                    Type parameterType = description2.ParameterDescriptor.ParameterType;
                    apiModel.RequestModelDescription = modelGenerator.GetOrCreateModelDescription(parameterType);
                    apiModel.RequestDocumentation = description2.Documentation;
                }
                else if ((description2.ParameterDescriptor != null) && (description2.ParameterDescriptor.ParameterType == typeof(HttpRequestMessage)))
                {
                    Type modelType = sampleGenerator.ResolveHttpRequestMessageType(apiDescription);
                    if (modelType != null)
                    {
                        apiModel.RequestModelDescription = modelGenerator.GetOrCreateModelDescription(modelType);
                    }
                }
            }
        }

        private static void GenerateResourceDescription(HelpPageApiModel apiModel, ModelDescriptionGenerator modelGenerator)
        {
            ResponseDescription responseDescription = apiModel.ApiDescription.ResponseDescription;
            Type modelType = responseDescription.ResponseType ?? responseDescription.DeclaredType;
            if ((modelType != null) && (modelType != typeof(void)))
            {
                apiModel.ResourceDescription = modelGenerator.GetOrCreateModelDescription(modelType);
            }
        }

        private static void GenerateSamples(HelpPageApiModel apiModel, HelpPageSampleGenerator sampleGenerator)
        {
            try
            {
                foreach (KeyValuePair<MediaTypeHeaderValue, object> pair in sampleGenerator.GetSampleRequests(apiModel.ApiDescription))
                {
                    apiModel.SampleRequests.Add(pair.Key, pair.Value);
                    LogInvalidSampleAsError(apiModel, pair.Value);
                }
                foreach (KeyValuePair<MediaTypeHeaderValue, object> pair2 in sampleGenerator.GetSampleResponses(apiModel.ApiDescription))
                {
                    apiModel.SampleResponses.Add(pair2.Key, pair2.Value);
                    LogInvalidSampleAsError(apiModel, pair2.Value);
                }
            }
            catch (Exception exception)
            {
                object[] args = new object[] { HelpPageSampleGenerator.UnwrapException(exception).Message };
                apiModel.ErrorMessages.Add(string.Format(CultureInfo.CurrentCulture, "An exception has occurred while generating the sample. Exception message: {0}", args));
            }
        }

        private static void GenerateUriParameters(HelpPageApiModel apiModel, ModelDescriptionGenerator modelGenerator)
        {
            ApiDescription apiDescription = apiModel.ApiDescription;
            foreach (ApiParameterDescription description2 in apiDescription.ParameterDescriptions)
            {
                if (description2.Source == ApiParameterSource.FromUri)
                {
                    HttpParameterDescriptor parameterDescriptor = description2.ParameterDescriptor;
                    Type modelType = null;
                    ModelDescription typeDescription = null;
                    ComplexTypeModelDescription description4 = null;
                    if (parameterDescriptor > null)
                    {
                        modelType = parameterDescriptor.ParameterType;
                        typeDescription = modelGenerator.GetOrCreateModelDescription(modelType);
                        description4 = typeDescription as ComplexTypeModelDescription;
                    }
                    if ((description4 != null) && !IsBindableWithTypeConverter(modelType))
                    {
                        foreach (ParameterDescription description5 in description4.Properties)
                        {
                            apiModel.UriParameters.Add(description5);
                        }
                    }
                    else if (parameterDescriptor > null)
                    {
                        ParameterAnnotation annotation;
                        ParameterDescription description6 = AddParameterDescription(apiModel, description2, typeDescription);
                        if (!parameterDescriptor.IsOptional)
                        {
                            annotation = new ParameterAnnotation {
                                Documentation = "Required"
                            };
                            description6.Annotations.Add(annotation);
                        }
                        object defaultValue = parameterDescriptor.DefaultValue;
                        if (defaultValue > null)
                        {
                            annotation = new ParameterAnnotation {
                                Documentation = "Default value is " + Convert.ToString(defaultValue, CultureInfo.InvariantCulture)
                            };
                            description6.Annotations.Add(annotation);
                        }
                    }
                    else
                    {
                        Debug.Assert(parameterDescriptor == null);
                        ModelDescription orCreateModelDescription = modelGenerator.GetOrCreateModelDescription(typeof(string));
                        AddParameterDescription(apiModel, description2, orCreateModelDescription);
                    }
                }
            }
        }

        public static HelpPageApiModel GetHelpPageApiModel(this HttpConfiguration config, string apiDescriptionId)
        {
            string key = "MS_HelpPageApiModel_" + apiDescriptionId;
            if (!config.Properties.TryGetValue(key, out object obj2))
            {
                ApiDescription apiDescription = config.Services.GetApiExplorer().ApiDescriptions.FirstOrDefault<ApiDescription>(api => string.Equals(api.GetFriendlyId(), apiDescriptionId, StringComparison.OrdinalIgnoreCase));
                if (apiDescription > null)
                {
                    obj2 = GenerateApiModel(apiDescription, config);
                    config.Properties.TryAdd(key, obj2);
                }
            }
            return (HelpPageApiModel) obj2;
        }

        public static HelpPageSampleGenerator GetHelpPageSampleGenerator(this HttpConfiguration config) => 
            ((HelpPageSampleGenerator) config.Properties.GetOrAdd(typeof(HelpPageSampleGenerator), k => new HelpPageSampleGenerator()));

        public static ModelDescriptionGenerator GetModelDescriptionGenerator(this HttpConfiguration config) => 
            ((ModelDescriptionGenerator) config.Properties.GetOrAdd(typeof(ModelDescriptionGenerator), k => InitializeModelDescriptionGenerator(config)));

        private static ModelDescriptionGenerator InitializeModelDescriptionGenerator(HttpConfiguration config)
        {
            ModelDescriptionGenerator generator = new ModelDescriptionGenerator(config);
            Collection<ApiDescription> apiDescriptions = config.Services.GetApiExplorer().ApiDescriptions;
            foreach (ApiDescription description in apiDescriptions)
            {
                if (TryGetResourceParameter(description, config, out _, out Type type))
                {
                    generator.GetOrCreateModelDescription(type);
                }
            }
            return generator;
        }

        private static bool IsBindableWithTypeConverter(Type parameterType)
        {
            if (parameterType == null)
            {
                return false;
            }
            return TypeDescriptor.GetConverter(parameterType).CanConvertFrom(typeof(string));
        }

        private static void LogInvalidSampleAsError(HelpPageApiModel apiModel, object sample)
        {
            InvalidSample sample2 = sample as InvalidSample;
            if (sample2 > null)
            {
                apiModel.ErrorMessages.Add(sample2.ErrorMessage);
            }
        }

        public static void SetActualRequestType(this HttpConfiguration config, Type type, string controllerName, string actionName)
        {
            string[] parameterNames = new string[] { "*" };
            config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Request, controllerName, actionName, parameterNames), type);
        }

        public static void SetActualRequestType(this HttpConfiguration config, Type type, string controllerName, string actionName, params string[] parameterNames)
        {
            config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Request, controllerName, actionName, parameterNames), type);
        }

        public static void SetActualResponseType(this HttpConfiguration config, Type type, string controllerName, string actionName)
        {
            string[] parameterNames = new string[] { "*" };
            config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Response, controllerName, actionName, parameterNames), type);
        }

        public static void SetActualResponseType(this HttpConfiguration config, Type type, string controllerName, string actionName, params string[] parameterNames)
        {
            config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Response, controllerName, actionName, parameterNames), type);
        }

        public static void SetDocumentationProvider(this HttpConfiguration config, IDocumentationProvider documentationProvider)
        {
            config.Services.Replace(typeof(IDocumentationProvider), documentationProvider);
        }

        public static void SetHelpPageSampleGenerator(this HttpConfiguration config, HelpPageSampleGenerator sampleGenerator)
        {
            config.Properties.AddOrUpdate(typeof(HelpPageSampleGenerator), k => sampleGenerator, (k, o) => sampleGenerator);
        }

        public static void SetSampleForMediaType(this HttpConfiguration config, object sample, MediaTypeHeaderValue mediaType)
        {
            config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType), sample);
        }

        public static void SetSampleForType(this HttpConfiguration config, object sample, MediaTypeHeaderValue mediaType, Type type)
        {
            config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, type), sample);
        }

        public static void SetSampleObjects(this HttpConfiguration config, IDictionary<Type, object> sampleObjects)
        {
            config.GetHelpPageSampleGenerator().SampleObjects = sampleObjects;
        }

        public static void SetSampleRequest(this HttpConfiguration config, object sample, MediaTypeHeaderValue mediaType, string controllerName, string actionName)
        {
            string[] parameterNames = new string[] { "*" };
            config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Request, controllerName, actionName, parameterNames), sample);
        }

        public static void SetSampleRequest(this HttpConfiguration config, object sample, MediaTypeHeaderValue mediaType, string controllerName, string actionName, params string[] parameterNames)
        {
            config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Request, controllerName, actionName, parameterNames), sample);
        }

        public static void SetSampleResponse(this HttpConfiguration config, object sample, MediaTypeHeaderValue mediaType, string controllerName, string actionName)
        {
            string[] parameterNames = new string[] { "*" };
            config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Response, controllerName, actionName, parameterNames), sample);
        }

        public static void SetSampleResponse(this HttpConfiguration config, object sample, MediaTypeHeaderValue mediaType, string controllerName, string actionName, params string[] parameterNames)
        {
            config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Response, controllerName, actionName, parameterNames), sample);
        }

        private static bool TryGetResourceParameter(ApiDescription apiDescription, HttpConfiguration config, out ApiParameterDescription parameterDescription, out Type resourceType)
        {
            parameterDescription = apiDescription.ParameterDescriptions.FirstOrDefault<ApiParameterDescription>(p => (p.Source == ApiParameterSource.FromBody) || ((p.ParameterDescriptor != null) && (p.ParameterDescriptor.ParameterType == typeof(HttpRequestMessage))));
            if (parameterDescription == null)
            {
                resourceType = null;
                return false;
            }
            resourceType = parameterDescription.ParameterDescriptor.ParameterType;
            if (resourceType == typeof(HttpRequestMessage))
            {
                resourceType = config.GetHelpPageSampleGenerator().ResolveHttpRequestMessageType(apiDescription);
            }
            if (resourceType == null)
            {
                parameterDescription = null;
                return false;
            }
            return true;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HelpPageConfigurationExtensions.<>c <>9 = new HelpPageConfigurationExtensions.<>c();
            public static Func<object, object> <>9__13_0;
            public static Func<ApiParameterDescription, bool> <>9__24_0;

            internal object <GetHelpPageSampleGenerator>b__13_0(object k) => 
                new HelpPageSampleGenerator();

            internal bool <TryGetResourceParameter>b__24_0(ApiParameterDescription p) => 
                ((p.Source == ApiParameterSource.FromBody) || ((p.ParameterDescriptor != null) && (p.ParameterDescriptor.ParameterType == typeof(HttpRequestMessage))));
        }
    }
}

