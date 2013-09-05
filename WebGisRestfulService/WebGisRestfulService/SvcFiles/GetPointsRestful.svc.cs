using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using NS_WebGisRestfulService;
using MongoDB;

namespace WebGisRestfulService
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GetPointsRestful
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "test_method", ResponseFormat = WebMessageFormat.Json)]
        public CarPoint test_method()
        {
            return new CarPoint();
        }
        // Add [WebGet] attribute to use HTTP GET
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetRouteP/{strCarID_in}/{strStartTime}/{strEndTime}", ResponseFormat = WebMessageFormat.Json)]
        public CarPoints GetSpecifiedPoints(string strCarID_in, string strStartTime, string strEndTime)
        {
            CarPoints cps = new CarPoints();
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
                    var db = mongo.GetDatabase("RouteInfo");
                    //var MyClassCollection = db.GetCollection<Document>("RoutePoint");
                    var MyClassCollection = db.GetCollection<CarPoint>("RoutePoint");
                    //  每一个查询条件初始化一个Document对象，再将这些对象放到一个总的Document对象中
                    //  作为一个查询条件使用
                    var lt = new Document().Add("StrCarID", strCarID_in);
                    Document spe = new Document().Add("$lt", strEndTime);
                    spe.Add("$gt",strStartTime);
                    //spe.Add("StrTime", -1);
                    lt.Add("StrTime", spe);
                    var all = MyClassCollection.Find(lt);
                    foreach (var doc in all.Documents)
                    {
                        CarPoint cp = new CarPoint(doc.StrCarID, doc.StrTime, doc.StrLatitude, doc.StrLongitude);
                        cps.Add(cp);
                    }
                }
                finally
                {
                    mongo.Disconnect();
                }
            }
            return cps;
        }
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "GetPos/{strCarID_in}", ResponseFormat = WebMessageFormat.Json)]
        public CarPoint GetLatestCarPosition(string strCarID_in)
        {
            CarPoint cp = null;
            //cp = new CarPoint("TestID001", "2011-5-13 14:06:00", "39.111", "113.222");
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
                        var collection = db.GetCollection<CarPoint>("CarPoint");
                        Document sort = new Document().Add("StrTime", -1);
                        var lt = new Document().Add("StrCarID", strCarID_in);
                        using (var all = collection.Find(lt))
                        {
                            var ds = all.Sort(sort);//排序做为单独的一块
                            if (ds.Documents.Count() > 0)
                            {
                                cp = ds.Documents.FirstOrDefault();
                            }
                        }
                        //lt.Add("StrTime", sort);
                        //cp = collection.FindOne(lt);
                        //cp = collection.FindAndModify(lt, new Document(), sort, true);
                        //cp = collection.FindAndModify(new Document(), new Document(), sort, true);
                    }
                    finally
                    {
                        mongo.Disconnect();
                    }
                }
            }
            return cp;
        }


    }
}
