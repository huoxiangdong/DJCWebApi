namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web;
    using System.Web.Http.Description;

    public static class ApiDescriptionExtensions
    {
        public static string GetFriendlyId(this ApiDescription description)
        {
            char[] separator = new char[] { '?' };
            string[] strArray = description.RelativePath.Split(separator);
            string str2 = strArray[0];
            string str3 = null;
            if (strArray.Length > 1)
            {
                string query = strArray[1];
                string[] allKeys = HttpUtility.ParseQueryString(query).AllKeys;
                str3 = string.Join("_", allKeys);
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("{0}-{1}", description.HttpMethod.Method, str2.Replace("/", "-").Replace("{", string.Empty).Replace("}", string.Empty));
            if (str3 > null)
            {
                builder.AppendFormat("_{0}", str3.Replace('.', '-'));
            }
            return builder.ToString();
        }
    }
}

