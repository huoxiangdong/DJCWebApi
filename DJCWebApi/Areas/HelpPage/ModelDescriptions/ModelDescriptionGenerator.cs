namespace DJCWebApi.Areas.HelpPage.ModelDescriptions
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Xml.Serialization;

    public class ModelDescriptionGenerator
    {
        private readonly IDictionary<Type, Func<object, string>> AnnotationTextGenerator;
        private readonly IDictionary<Type, string> DefaultTypeDocumentation;
        private Lazy<IModelDocumentationProvider> _documentationProvider;
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Dictionary<string, ModelDescription> <GeneratedModels>k__BackingField;

        public ModelDescriptionGenerator(HttpConfiguration config)
        {
            Dictionary<Type, Func<object, string>> dictionary = new Dictionary<Type, Func<object, string>> {
                { 
                    typeof(RequiredAttribute),
                    a => "Required"
                },
                { 
                    typeof(RangeAttribute),
                    delegate (object a) {
                        RangeAttribute attribute = (RangeAttribute) a;
                        object[] args = new object[] { 
                            attribute.Minimum,
                            attribute.Maximum
                        };
                        return string.Format(CultureInfo.CurrentCulture, "Range: inclusive between {0} and {1}", args);
                    }
                },
                { 
                    typeof(MaxLengthAttribute),
                    delegate (object a) {
                        MaxLengthAttribute attribute = (MaxLengthAttribute) a;
                        object[] args = new object[] { attribute.Length };
                        return string.Format(CultureInfo.CurrentCulture, "Max length: {0}", args);
                    }
                },
                { 
                    typeof(MinLengthAttribute),
                    delegate (object a) {
                        MinLengthAttribute attribute = (MinLengthAttribute) a;
                        object[] args = new object[] { attribute.Length };
                        return string.Format(CultureInfo.CurrentCulture, "Min length: {0}", args);
                    }
                },
                { 
                    typeof(StringLengthAttribute),
                    delegate (object a) {
                        StringLengthAttribute attribute = (StringLengthAttribute) a;
                        object[] args = new object[] { 
                            attribute.MinimumLength,
                            attribute.MaximumLength
                        };
                        return string.Format(CultureInfo.CurrentCulture, "String length: inclusive between {0} and {1}", args);
                    }
                },
                { 
                    typeof(DataTypeAttribute),
                    delegate (object a) {
                        DataTypeAttribute attribute = (DataTypeAttribute) a;
                        object[] args = new object[] { attribute.CustomDataType ?? attribute.get_DataType().ToString() };
                        return string.Format(CultureInfo.CurrentCulture, "Data type: {0}", args);
                    }
                },
                { 
                    typeof(RegularExpressionAttribute),
                    delegate (object a) {
                        RegularExpressionAttribute attribute = (RegularExpressionAttribute) a;
                        object[] args = new object[] { attribute.Pattern };
                        return string.Format(CultureInfo.CurrentCulture, "Matching regular expression pattern: {0}", args);
                    }
                }
            };
            this.AnnotationTextGenerator = dictionary;
            Dictionary<Type, string> dictionary2 = new Dictionary<Type, string> {
                { 
                    typeof(short),
                    "integer"
                },
                { 
                    typeof(int),
                    "integer"
                },
                { 
                    typeof(long),
                    "integer"
                },
                { 
                    typeof(ushort),
                    "unsigned integer"
                },
                { 
                    typeof(uint),
                    "unsigned integer"
                },
                { 
                    typeof(ulong),
                    "unsigned integer"
                },
                { 
                    typeof(byte),
                    "byte"
                },
                { 
                    typeof(char),
                    "character"
                },
                { 
                    typeof(sbyte),
                    "signed byte"
                },
                { 
                    typeof(Uri),
                    "URI"
                },
                { 
                    typeof(float),
                    "decimal number"
                },
                { 
                    typeof(double),
                    "decimal number"
                },
                { 
                    typeof(decimal),
                    "decimal number"
                },
                { 
                    typeof(string),
                    "string"
                },
                { 
                    typeof(Guid),
                    "globally unique identifier"
                },
                { 
                    typeof(TimeSpan),
                    "time interval"
                },
                { 
                    typeof(DateTime),
                    "date"
                },
                { 
                    typeof(DateTimeOffset),
                    "date"
                },
                { 
                    typeof(bool),
                    "boolean"
                }
            };
            this.DefaultTypeDocumentation = dictionary2;
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            this._documentationProvider = new Lazy<IModelDocumentationProvider>(() => config.Services.GetDocumentationProvider() as IModelDocumentationProvider);
            this.GeneratedModels = new Dictionary<string, ModelDescription>(StringComparer.OrdinalIgnoreCase);
        }

        private string CreateDefaultDocumentation(Type type)
        {
            if (!this.DefaultTypeDocumentation.TryGetValue(type, out string documentation) && (this.DocumentationProvider > null))
            {
                documentation = this.DocumentationProvider.GetDocumentation(type);
            }
            return documentation;
        }

        private void GenerateAnnotations(MemberInfo property, ParameterDescription propertyModel)
        {
            List<ParameterAnnotation> list = new List<ParameterAnnotation>();
            IEnumerable<Attribute> customAttributes = property.GetCustomAttributes();
            foreach (Attribute attribute in customAttributes)
            {
                if (this.AnnotationTextGenerator.TryGetValue(attribute.GetType(), out Func<object, string> func))
                {
                    ParameterAnnotation item = new ParameterAnnotation {
                        AnnotationAttribute = attribute,
                        Documentation = func(attribute)
                    };
                    list.Add(item);
                }
            }
            list.Sort(delegate (ParameterAnnotation x, ParameterAnnotation y) {
                if (x.AnnotationAttribute is RequiredAttribute)
                {
                    return -1;
                }
                if (y.AnnotationAttribute is RequiredAttribute)
                {
                    return 1;
                }
                return string.Compare(x.Documentation, y.Documentation, StringComparison.OrdinalIgnoreCase);
            });
            foreach (ParameterAnnotation annotation2 in list)
            {
                propertyModel.Annotations.Add(annotation2);
            }
        }

        private CollectionModelDescription GenerateCollectionModelDescription(Type modelType, Type elementType)
        {
            ModelDescription orCreateModelDescription = this.GetOrCreateModelDescription(elementType);
            if (orCreateModelDescription > null)
            {
                return new CollectionModelDescription { 
                    Name = ModelNameHelper.GetModelName(modelType),
                    ModelType = modelType,
                    ElementDescription = orCreateModelDescription
                };
            }
            return null;
        }

        private ModelDescription GenerateComplexTypeModelDescription(Type modelType)
        {
            ParameterDescription description4;
            ComplexTypeModelDescription description = new ComplexTypeModelDescription {
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                Documentation = this.CreateDefaultDocumentation(modelType)
            };
            this.GeneratedModels.Add(description.Name, description);
            bool hasDataContractAttribute = modelType.GetCustomAttribute<DataContractAttribute>() > null;
            PropertyInfo[] properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo info in properties)
            {
                if (ShouldDisplayMember(info, hasDataContractAttribute))
                {
                    description4 = new ParameterDescription {
                        Name = GetMemberName(info, hasDataContractAttribute)
                    };
                    ParameterDescription propertyModel = description4;
                    if (this.DocumentationProvider > null)
                    {
                        propertyModel.Documentation = this.DocumentationProvider.GetDocumentation(info);
                    }
                    this.GenerateAnnotations(info, propertyModel);
                    description.Properties.Add(propertyModel);
                    propertyModel.TypeDescription = this.GetOrCreateModelDescription(info.PropertyType);
                }
            }
            FieldInfo[] fields = modelType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo info2 in fields)
            {
                if (ShouldDisplayMember(info2, hasDataContractAttribute))
                {
                    description4 = new ParameterDescription {
                        Name = GetMemberName(info2, hasDataContractAttribute)
                    };
                    ParameterDescription item = description4;
                    if (this.DocumentationProvider > null)
                    {
                        item.Documentation = this.DocumentationProvider.GetDocumentation(info2);
                    }
                    description.Properties.Add(item);
                    item.TypeDescription = this.GetOrCreateModelDescription(info2.FieldType);
                }
            }
            return description;
        }

        private DictionaryModelDescription GenerateDictionaryModelDescription(Type modelType, Type keyType, Type valueType)
        {
            ModelDescription orCreateModelDescription = this.GetOrCreateModelDescription(keyType);
            ModelDescription description2 = this.GetOrCreateModelDescription(valueType);
            return new DictionaryModelDescription { 
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                KeyModelDescription = orCreateModelDescription,
                ValueModelDescription = description2
            };
        }

        private EnumTypeModelDescription GenerateEnumTypeModelDescription(Type modelType)
        {
            EnumTypeModelDescription description = new EnumTypeModelDescription {
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                Documentation = this.CreateDefaultDocumentation(modelType)
            };
            bool hasDataContractAttribute = modelType.GetCustomAttribute<DataContractAttribute>() > null;
            foreach (FieldInfo info in modelType.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (ShouldDisplayMember(info, hasDataContractAttribute))
                {
                    EnumValueDescription item = new EnumValueDescription {
                        Name = info.Name,
                        Value = info.GetRawConstantValue().ToString()
                    };
                    if (this.DocumentationProvider > null)
                    {
                        item.Documentation = this.DocumentationProvider.GetDocumentation(info);
                    }
                    description.Values.Add(item);
                }
            }
            this.GeneratedModels.Add(description.Name, description);
            return description;
        }

        private KeyValuePairModelDescription GenerateKeyValuePairModelDescription(Type modelType, Type keyType, Type valueType)
        {
            ModelDescription orCreateModelDescription = this.GetOrCreateModelDescription(keyType);
            ModelDescription description2 = this.GetOrCreateModelDescription(valueType);
            return new KeyValuePairModelDescription { 
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                KeyModelDescription = orCreateModelDescription,
                ValueModelDescription = description2
            };
        }

        private ModelDescription GenerateSimpleTypeModelDescription(Type modelType)
        {
            SimpleTypeModelDescription description = new SimpleTypeModelDescription {
                Name = ModelNameHelper.GetModelName(modelType),
                ModelType = modelType,
                Documentation = this.CreateDefaultDocumentation(modelType)
            };
            this.GeneratedModels.Add(description.Name, description);
            return description;
        }

        private static string GetMemberName(MemberInfo member, bool hasDataContractAttribute)
        {
            JsonPropertyAttribute customAttribute = member.GetCustomAttribute<JsonPropertyAttribute>();
            if ((customAttribute != null) && !string.IsNullOrEmpty(customAttribute.PropertyName))
            {
                return customAttribute.PropertyName;
            }
            if (hasDataContractAttribute)
            {
                DataMemberAttribute attribute2 = member.GetCustomAttribute<DataMemberAttribute>();
                if ((attribute2 != null) && !string.IsNullOrEmpty(attribute2.Name))
                {
                    return attribute2.Name;
                }
            }
            return member.Name;
        }

        public ModelDescription GetOrCreateModelDescription(Type modelType)
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }
            Type underlyingType = Nullable.GetUnderlyingType(modelType);
            if (underlyingType != null)
            {
                modelType = underlyingType;
            }
            string modelName = ModelNameHelper.GetModelName(modelType);
            if (this.GeneratedModels.TryGetValue(modelName, out ModelDescription description))
            {
                if (modelType != description.ModelType)
                {
                    object[] args = new object[] { modelName, description.ModelType.FullName, modelType.FullName };
                    throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "A model description could not be created. Duplicate model name '{0}' was found for types '{1}' and '{2}'. Use the [ModelName] attribute to change the model name for at least one of the types so that it has a unique name.", args));
                }
                return description;
            }
            if (this.DefaultTypeDocumentation.ContainsKey(modelType))
            {
                return this.GenerateSimpleTypeModelDescription(modelType);
            }
            if (modelType.IsEnum)
            {
                return this.GenerateEnumTypeModelDescription(modelType);
            }
            if (modelType.IsGenericType)
            {
                Type[] genericArguments = modelType.GetGenericArguments();
                if ((genericArguments.Length == 1) && typeof(IEnumerable<>).MakeGenericType(genericArguments).IsAssignableFrom(modelType))
                {
                    return this.GenerateCollectionModelDescription(modelType, genericArguments[0]);
                }
                if (genericArguments.Length == 2)
                {
                    if (typeof(IDictionary<,>).MakeGenericType(genericArguments).IsAssignableFrom(modelType))
                    {
                        return this.GenerateDictionaryModelDescription(modelType, genericArguments[0], genericArguments[1]);
                    }
                    if (typeof(KeyValuePair<,>).MakeGenericType(genericArguments).IsAssignableFrom(modelType))
                    {
                        return this.GenerateKeyValuePairModelDescription(modelType, genericArguments[0], genericArguments[1]);
                    }
                }
            }
            if (modelType.IsArray)
            {
                Type elementType = modelType.GetElementType();
                return this.GenerateCollectionModelDescription(modelType, elementType);
            }
            if (modelType == typeof(NameValueCollection))
            {
                return this.GenerateDictionaryModelDescription(modelType, typeof(string), typeof(string));
            }
            if (typeof(IDictionary).IsAssignableFrom(modelType))
            {
                return this.GenerateDictionaryModelDescription(modelType, typeof(object), typeof(object));
            }
            if (typeof(IEnumerable).IsAssignableFrom(modelType))
            {
                return this.GenerateCollectionModelDescription(modelType, typeof(object));
            }
            return this.GenerateComplexTypeModelDescription(modelType);
        }

        private static bool ShouldDisplayMember(MemberInfo member, bool hasDataContractAttribute)
        {
            JsonIgnoreAttribute customAttribute = member.GetCustomAttribute<JsonIgnoreAttribute>();
            XmlIgnoreAttribute attribute2 = member.GetCustomAttribute<XmlIgnoreAttribute>();
            IgnoreDataMemberAttribute attribute3 = member.GetCustomAttribute<IgnoreDataMemberAttribute>();
            NonSerializedAttribute attribute4 = member.GetCustomAttribute<NonSerializedAttribute>();
            ApiExplorerSettingsAttribute attribute5 = member.GetCustomAttribute<ApiExplorerSettingsAttribute>();
            bool flag = member.DeclaringType.IsEnum ? (member.GetCustomAttribute<EnumMemberAttribute>() > null) : (member.GetCustomAttribute<DataMemberAttribute>() > null);
            return (((((customAttribute == null) && (attribute2 == null)) && ((attribute3 == null) && (attribute4 == null))) && ((attribute5 == null) || !attribute5.IgnoreApi)) && (!hasDataContractAttribute | flag));
        }

        public Dictionary<string, ModelDescription> GeneratedModels { get; private set; }

        private IModelDocumentationProvider DocumentationProvider =>
            this._documentationProvider.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ModelDescriptionGenerator.<>c <>9 = new ModelDescriptionGenerator.<>c();
            public static Func<object, string> <>9__3_1;
            public static Func<object, string> <>9__3_2;
            public static Func<object, string> <>9__3_3;
            public static Func<object, string> <>9__3_4;
            public static Func<object, string> <>9__3_5;
            public static Func<object, string> <>9__3_6;
            public static Func<object, string> <>9__3_7;
            public static Comparison<ParameterAnnotation> <>9__14_0;

            internal string <.ctor>b__3_1(object a) => 
                "Required";

            internal string <.ctor>b__3_2(object a)
            {
                RangeAttribute attribute = (RangeAttribute) a;
                object[] args = new object[] { attribute.Minimum, attribute.Maximum };
                return string.Format(CultureInfo.CurrentCulture, "Range: inclusive between {0} and {1}", args);
            }

            internal string <.ctor>b__3_3(object a)
            {
                MaxLengthAttribute attribute = (MaxLengthAttribute) a;
                object[] args = new object[] { attribute.Length };
                return string.Format(CultureInfo.CurrentCulture, "Max length: {0}", args);
            }

            internal string <.ctor>b__3_4(object a)
            {
                MinLengthAttribute attribute = (MinLengthAttribute) a;
                object[] args = new object[] { attribute.Length };
                return string.Format(CultureInfo.CurrentCulture, "Min length: {0}", args);
            }

            internal string <.ctor>b__3_5(object a)
            {
                StringLengthAttribute attribute = (StringLengthAttribute) a;
                object[] args = new object[] { attribute.MinimumLength, attribute.MaximumLength };
                return string.Format(CultureInfo.CurrentCulture, "String length: inclusive between {0} and {1}", args);
            }

            internal string <.ctor>b__3_6(object a)
            {
                DataTypeAttribute attribute = (DataTypeAttribute) a;
                object[] args = new object[] { attribute.CustomDataType ?? attribute.get_DataType().ToString() };
                return string.Format(CultureInfo.CurrentCulture, "Data type: {0}", args);
            }

            internal string <.ctor>b__3_7(object a)
            {
                RegularExpressionAttribute attribute = (RegularExpressionAttribute) a;
                object[] args = new object[] { attribute.Pattern };
                return string.Format(CultureInfo.CurrentCulture, "Matching regular expression pattern: {0}", args);
            }

            internal int <GenerateAnnotations>b__14_0(ParameterAnnotation x, ParameterAnnotation y)
            {
                if (x.AnnotationAttribute is RequiredAttribute)
                {
                    return -1;
                }
                if (y.AnnotationAttribute is RequiredAttribute)
                {
                    return 1;
                }
                return string.Compare(x.Documentation, y.Documentation, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}

