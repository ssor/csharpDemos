using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Runtime.Serialization;
using System.Collections.Generic;


namespace NS_WebGisRestfulService
{

    [DataContract]
    public class CarPoint
    {
        string strCarID = string.Empty;
        [DataMember]
        public string StrCarID
        {
            get { return strCarID; }
            set { strCarID = value; }
        }
        // no arguments constructor is necessary
        public CarPoint()
        {
            this.strCarID = "0";
            this.strLatitude = "0";
            this.strLongitude = "0";
            this.strTime = "0";

        }
        public CarPoint(string strCarID_in,string strTime_in, string strLat_in, string strLon_in)
        {
            this.strCarID = strCarID_in;
            this.StrTime = strTime_in;
            this.StrLatitude = strLat_in;
            this.StrLongitude = strLon_in;
        }
        string strTime = DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToShortTimeString();
        [DataMember]
        public string StrTime
        {
            get { return strTime; }
            set { strTime = value; }
        }
        string strLatitude = "0";
        [DataMember]
        public string StrLatitude
        {
            get { return strLatitude; }
            set { strLatitude = value; }
        }
        string strLongitude = "0";
        [DataMember]
        public string StrLongitude
        {
            get { return strLongitude; }
            set { strLongitude = value; }
        }
    }
    [CollectionDataContract(Name = "CarPoints", Namespace = "")]
    public class CarPoints : List<CarPoint>
    {
        //List<CarPoint> carPointList;
        //public CarPoints()
        //{
        //    carPointList = new List<CarPoint>();
        //}
        //[DataMember]
        //public List<CarPoint> CarPointList
        //{
        //    get { return carPointList; }
        //    set { carPointList = value; }
        //}
    }
}
