using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using MongoDB;
using MongoDB.Configuration;
using NS_WebGisRestfulService;

namespace WebGisRestfulService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AddPointsRestful
    {
        [OperationContract]
        [WebInvoke(Method = "GET",UriTemplate = "AddPoint/{strCarID_in}/{strTime_in}/{strLat_in}/{strLon_in}",ResponseFormat = WebMessageFormat.Json)]
        public int AddCarPointByGet(string strTime_in, string strLat_in, string strLon_in,string strCarID_in)
        {
            CarPoint cp = new CarPoint(strCarID_in,strTime_in, strLat_in, strLon_in);
            // add data to DB
            {
#if LOCALDB
                string strSeverInfo = "Server=127.0.0.1";
#elif REMOTEDB
                string strSeverInfo = "Server=61.50.168.12";
#endif
                //using (Mongo mongo = new Mongo("Server=61.50.168.12"))
                using (Mongo mongo = new Mongo(strSeverInfo))
                {
                    mongo.Connect();
                    try
                    {
                        var db = mongo.GetDatabase("CarMonitoring");
                        // 存储类时使用该种形式，否则无法将要存储的类转化
                        var collection = db.GetCollection<CarPoint>();

                        collection.Save(cp);
                    }
                    finally
                    {
                        mongo.Disconnect();
                    }
                }
            }
            return 1;
        }
        //{"StrLatitude":"34","StrLongitude":"56","StrTime":"12"}
        //public CarPoint AddCarPointByGet(string strTime_in, string strLat_in, string strLon_in)
        //{
        //    CarPoint cp = new CarPoint(strTime_in, strLat_in, strLon_in);
        //    // add data to DB

        //    return cp;
        //}
        [OperationContract]
        [WebInvoke(Method = "POST",UriTemplate = "AddPoint",ResponseFormat = WebMessageFormat.Json,RequestFormat = WebMessageFormat.Json)]
        public int AddCarPointByPost(CarPoint cp)
        {
            // add data to DB
            {
#if LOCALDB
                string strSeverInfo = "Server=127.0.0.1";
#elif REMOTEDB
                string strSeverInfo = "Server=61.50.168.12";
#endif
                //using (Mongo mongo = new Mongo("Server=61.50.168.12"))
                using (Mongo mongo = new Mongo(strSeverInfo))
                {
                    mongo.Connect();
                    try
                    {
                        var db = mongo.GetDatabase("CarMonitoring");
                        // 存储类时使用该种形式，否则无法将要存储的类转化
                        var collection = db.GetCollection<CarPoint>();

                        collection.Save(cp);
                    }
                    finally
                    {
                        mongo.Disconnect();
                    }
                }
            }
            return 1;
        }
        // Add [WebGet] attribute to use HTTP GET
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]
    }
    //[DataContract]
    //public class CarPoint
    //{
    //    public CarPoint(string strTime_in,string strLat_in,string strLon_in)
    //    {
    //        this.StrTime = strTime_in;
    //        this.StrLatitude = strLat_in;
    //        this.StrLongitude = strLon_in;
    //    }
    //    string strTime = DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToShortTimeString();
    //    [DataMember]
    //    public string StrTime
    //    {
    //        get { return strTime; }
    //        set { strTime = value; }
    //    }
    //    string strLatitude = "0";
    //    [DataMember]
    //    public string StrLatitude
    //    {
    //        get { return strLatitude; }
    //        set { strLatitude = value; }
    //    }
    //    string strLongitude = "0";
    //    [DataMember]
    //    public string StrLongitude
    //    {
    //        get { return strLongitude; }
    //        set { strLongitude = value; }
    //    }
    //}
}
