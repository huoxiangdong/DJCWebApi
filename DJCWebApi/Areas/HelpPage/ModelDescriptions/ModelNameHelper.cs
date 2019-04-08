namespace DJCWebApi.Areas.HelpPage.ModelDescriptions
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class ModelNameHelper
    {
        public static string GetModelName(Type type)
        {
            ModelNameAttribute customAttribute = type.GetCustomAttribute<ModelNameAttribute>();
            if ((customAttribute != null) && !string.IsNullOrEmpty(customAttribute.Name))
            {
                return customAttribute.Name;
            }
            string name = type.Name;
            if (type.IsGenericType)
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string str3 = genericTypeDefinition.Name;
                str3 = str3.Substring(0, str3.IndexOf('`'));
                string[] strArray = (from t in genericArguments select GetModelName(t)).ToArray<string>();
                object[] args = new object[] { str3, string.Join("And", strArray) };
                name = string.Format(CultureInfo.InvariantCulture, "{0}Of{1}", args);
            }
            return name;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ModelNameHelper.<>c <>9 = new ModelNameHelper.<>c();
            public static Func<Type, string> <>9__0_0;

            internal string <GetModelName>b__0_0(Type t) => 
                ModelNameHelper.GetModelName(t);
        }
    }
}

