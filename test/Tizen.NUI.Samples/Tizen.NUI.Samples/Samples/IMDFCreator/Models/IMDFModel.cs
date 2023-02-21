using Newtonsoft.Json;
using ServiceProxy.IMDF;
using System;

namespace Model.IMDF
{
    public class IMDFModel
    {
        private static readonly Lazy<IMDFModel> instance = new Lazy<IMDFModel>((() => new IMDFModel()));

        public static IMDFModel Instance
        {
            get { return instance.Value; }
        }

        public IMDFIndoorMap IndoorMap;
        public bool IsDataInitialized = false;

        public IMDFModel()
        {
            var data = IMDFServiceProxy.Instance.GetDataFromSource();
            IndoorMap = JsonConvert.DeserializeObject<IMDFIndoorMap>(data);
            IsDataInitialized = true;

            Tizen.Log.Info("MYLOG", $"Read Data : {IndoorMap.building.name}");
        }
    }
}
