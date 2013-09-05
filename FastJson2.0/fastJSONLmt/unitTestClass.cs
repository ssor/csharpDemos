using System;
using System.Collections.Generic;
using System.Text;

namespace fastJSON
{
    // [{"tag":"t1","startTime":"111","cmd":"cmd","state":""},{"tag":"t2","startTime":"111","cmd":"cmd","state":""}]
    // [{"tag":"t1","startTime":"111","cmd":"cmd","state":""},{"tag":"t2","startTime":"111","cmd":"cmd","state":""}]
    public class tagID
    {
        public string tag = string.Empty;
        public string startTime = string.Empty;
        public string cmd = string.Empty;
        public string state;
        public tagID() { }
        //public tagID(string _tag, string _time)
        public tagID(string _tag, string _time, string _cmd)
        {
            this.tag = _tag;
            this.startTime = _time;
            this.cmd = _cmd;
            this.state = string.Empty;
        }
        public tagID(string _tag, string _time, string _cmd, string _state)
        {
            this.tag = _tag;
            this.startTime = _time;
            this.cmd = _cmd;
            this.state = _state;
        }
        public string toJSON()
        {
            string json = string.Empty;
            json = "{\"tag\":\"" + this.tag + "\",\"startTime\":\"" + this.startTime + "\",\"cmd\":\"" + this.cmd + "\",\"state\":\"\"}";
            //json = string.Format("{\"tag\":\"{0}\",\"startTime\":\"{1}\",\"cmd\":\"{2}\",\"state\":\"\"}", this.tag, this.startTime, this.cmd);
            return json;
        }
        public static tagID createUnitTestClass(Dictionary<string, object> dic)
        {
            string tag = string.Empty;
            string startTime = string.Empty;
            string cmd = string.Empty;
            string state = string.Empty;
            object tmpTag = null;
            object tmpTime = null;
            object tmpCmd = null;
            object tmpState = null;
            if (dic.TryGetValue("tag", out tmpTag) == true)
            {
                tag = (string)tmpTag;
            }
            if (dic.TryGetValue("startTime", out tmpTime) == true)
            {

                startTime = (string)tmpTime;

            }
            if (dic.TryGetValue("cmd", out tmpCmd) == true)
            {
                cmd = (string)tmpCmd;
            }
            if (dic.TryGetValue("state", out tmpState) == true)
            {
                state = (string)tmpState;
            }
            tagID u = new tagID(tag, startTime, cmd, state);
            return u;
        }
        public static string toJSONFromList(List<tagID> list)
        {
            if (list == null || list.Count <= 0)
            {
                return "[]";
            }
            string strR = "[";
            for (int i = 0; i < list.Count; i++)
            {
                tagID t = list[i];
                if (i > 0)
                {
                    strR += "," + t.toJSON();
                }
                else
                {
                    strR += t.toJSON();
                }
            }
            strR += "]";
            return strR;
        }
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
        public static unitTestClass createUnitTestClass(Dictionary<string, object> dic)
        {
            string name = string.Empty;
            int ncount = 0;
            object tmpName = null;
            object tmpCount = null;

            if (dic.TryGetValue("name", out tmpName) == true)
            {
                name = (string)tmpName;
            }
            if (dic.TryGetValue("count", out tmpCount) == true)
            {
                try
                {
                    ncount = int.Parse((string)tmpCount);
                }
                catch (System.Exception ex)
                {

                }
            }
            unitTestClass u = new unitTestClass(name, ncount);
            return u;
        }

    }
}
