using System;
using System.Collections.Generic;
using System.Text;
using fastJSON;
using System.Diagnostics;
using System.Collections;
using System.Reflection;

namespace fastJSONLmt
{
    public delegate void GenericSetter(object target, object value);

    class Program
    {
        static void Main(string[] args)
        {
            //unitTestClass ucreated = Activator.CreateInstance<unitTestClass>();
            //Type type = typeof(unitTestClass);
            //FieldInfo[] fi = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            //foreach (FieldInfo f in fi)
            //{
            //    Console.WriteLine("fieldInfo name -> {0} type -> {1}", f.Name, f.FieldType);
            //}

            unitTestClass u = new unitTestClass("c1", 2);
            string jsonString = fastJSON.JSON.Instance.ToJSON(u);
            Debug.WriteLine(jsonString);

            List<tagID> tagList = new List<tagID>();
            tagID t1 = new tagID("t1", "111", "cmd");
            tagID t2 = new tagID("t2", "111", "cmd");
            tagList.Add(t1);
            tagList.Add(t2);
            string tagJson1 = tagID.toJSONFromList(tagList);
            string tagJson = fastJSON.JSON.Instance.ToJSON(tagList);
            List<unitTestClass> ltc = new List<unitTestClass>();
            ltc.Add(new unitTestClass("l1", 1));
            ltc.Add(new unitTestClass("l2", 2));
            ltc.Add(new unitTestClass("l3", 3));
            ltc.Add(new unitTestClass("l4", 4));
            string listJsonString = fastJSON.JSON.Instance.ToJSON(ltc);
            Debug.WriteLine(listJsonString);

            string jsons = "{\"name\":\"c2\",\"count\":20}";
            object o = fastJSON.JSON.Instance.ToObject(jsons, typeof(unitTestClass));
            Dictionary<string, object> dic = (Dictionary<string, object>)o;
            foreach (KeyValuePair<string, object> kvp in dic)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }
            unitTestClass u1 = unitTestClass.createUnitTestClass(dic);


            string listJsons = "[{\"name\":\"l1\",\"count\":1},{\"name\":\"l2\",\"count\":2},{\"name\":\"l3\",\"count\":3},{\"name\":\"l4\",\"count\":4}]";
            object olist = fastJSON.JSON.Instance.ToObjectListDic(listJsons);
            ArrayList array = (ArrayList)olist;
            for (int i = 0; i < array.Count; i++)
            {
                Dictionary<string, object> dicTemp = (Dictionary<string, object>)array[i];
                foreach (KeyValuePair<string, object> kvp in dicTemp)
                {
                    Console.WriteLine("Key = {0}, Value = {1}",
                        kvp.Key, kvp.Value);
                }
            }


            //unitTestClass u2 = fastJSON.JSON.Instance.ToObject<unitTestClass>(jsons);
            // unitTestClass u2 = (unitTestClass)o;
            //Debug.WriteLine(u2.name);
            //Debug.WriteLine(u2.count);
            Console.Read();

        }
    }




}
