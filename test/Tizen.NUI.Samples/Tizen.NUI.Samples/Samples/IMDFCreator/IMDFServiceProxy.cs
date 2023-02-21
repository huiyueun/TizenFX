using System;
using System.IO;

namespace ServiceProxy.IMDF
{
    public class IMDFServiceProxy
    {
        private static readonly Lazy<IMDFServiceProxy> instance = new Lazy<IMDFServiceProxy>((() => new IMDFServiceProxy()));
        public static IMDFServiceProxy Instance
        {
            get { return instance.Value; }
        }

        private string dataDirectory = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/IMDFData/";
        private string resourceName = "6e20f8f0-caac-481f-898a-c84b8461bab8.json";

        private IMDFServiceProxy() { }

        public string GetDataFromSource()
        {
            var resources = File.ReadAllText(dataDirectory + resourceName);
            return resources;
        }
    }
}
