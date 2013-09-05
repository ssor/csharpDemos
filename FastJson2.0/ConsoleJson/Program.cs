using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ConsoleJson
{
    class Program
    {
        static void Main(string[] args)
        {
            string fpJson="{\"EPC\":\"FFF123451234560001000008\",\"Pname\":\"\u4ea7\u54c1A                                               \",\"productPici\":\"2222\",\"temputer\":\"22\",\"mature\":\"25\",\"startTime\":\"2012-3-9 15:28:39\",\"endTime\":\"2012-3-9 15:29:21\",\"beiZhu\":\"A                                                 \",\"productState\":\"\u5728\u5e93\",\"state\":\"ok\"}";
            object oFP = fastJSON.JSON.Instance.ToObject<FProduct>(fpJson);

            unitTestClass u = new unitTestClass("c1", 2);
            string jsonString = fastJSON.JSON.Instance.ToJSON(u);
            Debug.WriteLine(jsonString);

            string jsons = "{\"name\":\"c2\",\"count\":20}";
            object o = fastJSON.JSON.Instance.ToObject(jsons, typeof(unitTestClass));
            unitTestClass u2 = fastJSON.JSON.Instance.ToObject<unitTestClass>(jsons);
            // unitTestClass u2 = (unitTestClass)o;
            Debug.WriteLine(u2.name);
            Debug.WriteLine(u2.count);

            //List<unitTestClass> list = new List<unitTestClass>();
            listTestClass ltc = new listTestClass();

            ltc.Add(new unitTestClass("l1", 1));
            ltc.Add(new unitTestClass("l2", 2));
            ltc.Add(new unitTestClass("l3", 3));
            ltc.Add(new unitTestClass("l4", 4));
            string listJsonString = fastJSON.JSON.Instance.ToJSON(ltc);
            Debug.WriteLine(listJsonString);
            string listJsons = "[{\"name\":\"l1\",\"count\":1},{\"name\":\"l2\",\"count\":2},{\"name\":\"l3\",\"count\":3},{\"name\":\"l4\",\"count\":4}]";
            object olist = fastJSON.JSON.Instance.ToObjectList(listJsons, typeof(listTestClass), typeof(unitTestClass));
            //            object olist = fastJSON.JSON.Instance.ToObject(listJsons, typeof(listTestClass));
            foreach (unitTestClass c in (List<unitTestClass>)olist)
            {
                Debug.WriteLine(c.name + "      " + c.count.ToString());
            }
            Console.Read();
        }
    }
    public class listTestClass : List<unitTestClass>
    {
        public listTestClass() { }
    }
    public class unitTestClass
    {
        public string name = string.Empty;
        public int count = 0;
        //必须有一个无参构造函数
        public unitTestClass() { }
        public unitTestClass(string name, int count)
        {
            this.name = name;
            this.count = count;
        }
    }
}
