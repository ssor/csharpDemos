using System;
using System.Collections.Generic;
using System.Text;
using Db4objects.Db4o;

namespace SGSclient
{
    public class SavedData
    {
        public string name = string.Empty;
        public string message = string.Empty;
        public SavedData(string name, string msg)
        {
            this.name = name;
            this.message = msg;
        }
        public void copy(SavedData source)
        {
            this.name = source.name;
            this.message = source.message;
        }
        public static List<SavedData> getAllSavedData()
        {
            List<SavedData> listR = new List<SavedData>();
            IObjectContainer db = Db4oFactory.OpenFile(staticClass.configFilePath);
            try
            {
                IList<SavedData> list = db.Query<SavedData>(delegate(SavedData cf)
                                                 {
                                                     return cf.name != string.Empty;
                                                 }

                                           );

                foreach (SavedData sd in list)
                {
                    listR.Add(sd);
                }
            }
            finally
            {
                db.Close();
            }
            return listR;
        }
        public static SavedData getSaveData(SavedData data)
        {
            SavedData configR = null;
            IObjectContainer db = Db4oFactory.OpenFile(staticClass.configFilePath);
            try
            {
                IList<SavedData> list = db.Query<SavedData>(delegate(SavedData cf)
                {
                    return data.name == cf.name;
                }
                                                          );
                if (list.Count > 0)
                {
                    configR = list[0];
                }
            }
            finally
            {
                db.Close();
            }
            return configR;
        }
        public static void saveData(SavedData data)
        {
            IObjectContainer db = Db4oFactory.OpenFile(staticClass.configFilePath);
            try
            {
                IList<SavedData> list = db.Query<SavedData>(delegate(SavedData cf)
                {
                    return data.name == cf.name;
                }
                                                          );
                if (list.Count <= 0)
                {
                    db.Store(data);
                }
                else
                {
                    list[0].copy(data);
                    db.Store(list[0]);
                }

            }
            finally
            {
                db.Close();
            }
        }
        //public string getConfigName()
        //{
        //    return this.name;
        //}
    }
}
