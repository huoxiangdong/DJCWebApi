namespace DJCWebApi.Areas.HelpPage
{
    using DJCWebApi.Areas.HelpPage.ModelDescriptions;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Web.Http.Controllers;
    using System.Web.Http.Description;
    using System.Xml.XPath;

    public class XmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        private XPathNavigator _documentNavigator;
        private const string TypeExpression = "/doc/members/member[@name='T:{0}']";
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";
        private const string PropertyExpression = "/doc/members/member[@name='P:{0}']";
        private const string FieldExpression = "/doc/members/member[@name='F:{0}']";
        private const string ParameterExpression = "param[@name='{0}']";

        public XmlDocumentationProvider(string documentPath)
        {
            if (documentPath == null)
            {
                throw new ArgumentNullException("documentPath");
            }
            this._documentNavigator = new XPathDocument(documentPath).CreateNavigator();
        }

        public string GetDocumentation(MemberInfo member)
        {
            object[] args = new object[] { GetTypeName(member.DeclaringType), member.Name };
            string str = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", args);
            string format = (member.MemberType == MemberTypes.Field) ? "/doc/members/member[@name='F:{0}']" : "/doc/members/member[@name='P:{0}']";
            object[] objArray2 = new object[] { str };
            string xpath = string.Format(CultureInfo.InvariantCulture, format, objArray2);
            return GetTagValue(this._documentNavigator.SelectSingleNode(xpath), "summary");
        }

        public string GetDocumentation(Type type) => 
            GetTagValue(this.GetTypeNode(type), "summary");

        public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor) => 
            GetTagValue(this.GetMethodNode(actionDescriptor), "summary");

        public string GetDocumentation(HttpControllerDescriptor controllerDescriptor) => 
            GetTagValue(this.GetTypeNode(controllerDescriptor.ControllerType), "summary");

        public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            ReflectedHttpParameterDescriptor descriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;
            if (descriptor > null)
            {
                XPathNavigator methodNode = this.GetMethodNode(descriptor.ActionDescriptor);
                if (methodNode > null)
                {
                    string name = descriptor.ParameterInfo.Name;
                    object[] args = new object[] { name };
                    XPathNavigator navigator2 = methodNode.SelectSingleNode(string.Format(CultureInfo.InvariantCulture, "param[@name='{0}']", args));
                    if (navigator2 > null)
                    {
                        return navigator2.Value.Trim();
                    }
                }
            }
            return null;
        }

        private static string GetMemberName(MethodInfo method)
        {
            object[] args = new object[] { GetTypeName(method.DeclaringType), method.Name };
            string str = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", args);
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length != 0)
            {
                string[] strArray = (from param in parameters select GetTypeName(param.ParameterType)).ToArray<string>();
                object[] objArray2 = new object[] { string.Join(",", strArray) };
                str = str + string.Format(CultureInfo.InvariantCulture, "({0})", objArray2);
            }
            return str;
        }

        private XPathNavigator GetMethodNode(HttpActionDescriptor actionDescriptor)
        {
            ReflectedHttpActionDescriptor descriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            if (descriptor > null)
            {
                object[] args = new object[] { GetMemberName(descriptor.MethodInfo) };
                string xpath = string.Format(CultureInfo.InvariantCulture, "/doc/members/member[@name='M:{0}']", args);
                return this._documentNavigator.SelectSingleNode(xpath);
            }
            return null;
        }

        public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor) => 
            GetTagValue(this.GetMethodNode(actionDescriptor), "returns");

        private static string GetTagValue(XPathNavigator parentNode, string tagName)
        {
            if (parentNode > null)
            {
                XPathNavigator navigator = parentNode.SelectSingleNode(tagName);
                if (navigator > null)
                {
                    return navigator.Value.Trim();
                }
            }
            return null;
        }

        private static string GetTypeName(Type type)
        {
            string fullName = type.FullName;
            if (type.IsGenericType)
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                Type[] genericArguments = type.GetGenericArguments();
                string str2 = genericTypeDefinition.FullName;
                str2 = str2.Substring(0, str2.IndexOf('`'));
                string[] strArray = (from t in genericArguments select GetTypeName(t)).ToArray<string>();
                object[] args = new object[] { str2, string.Join(",", strArray) };
                fullName = string.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", args);
            }
            if (type.IsNested)
            {
                fullName = fullName.Replace("+", ".");
            }
            return fullName;
        }

        private XPathNavigator GetTypeNode(Type type)
        {
            string typeName = GetTypeName(type);
            object[] args = new object[] { typeName };
            string xpath = string.Format(CultureInfo.InvariantCulture, "/doc/members/member[@name='T:{0}']", args);
            return this._documentNavigator.SelectSingleNode(xpath);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly XmlDocumentationProvider.<>c <>9 = new XmlDocumentationProvider.<>c();
            public static Func<ParameterInfo, string> <>9__14_0;
            public static Func<Type, string> <>9__17_0;

            internal string <GetMemberName>b__14_0(ParameterInfo param) => 
                XmlDocumentationProvider.GetTypeName(param.ParameterType);

            internal string <GetTypeName>b__17_0(Type t) => 
                XmlDocumentationProvider.GetTypeName(t);
        }
    }
}

