using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace linqDemo
{
    public class Program
    {
        static void Main(string[] args)
        {

            List<string> strList = new List<string>();

            var newList = from s in strList select s;

            //Get_des_folder_existed_file("10001", @"C:\\davs");

            //GetDistinctName();

            Console.ReadLine();
        }
        public static List<string> GetDistinctName(List<string> list)
        {
            var groups = list.Select(_s => Get_group_id(_s)).Distinct();
            var orderedList = list.OrderByDescending(_s => _s);

            var names = groups.Select(_groupID =>
            {
                return orderedList.First(_s => Get_group_id(_s) == _groupID);
            }).ToList<string>();

            return names;
        }
        private static string Get_group_id(string file_name)
        {
            if (file_name.IndexOf("-") >= 0)
            {
                return file_name.Substring(0, file_name.IndexOf("-"));
            }
            else
                return string.Empty;
        }
        static void Get_des_folder_existed_file(string _file_name, string _file_path)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(_file_path);
            FileInfo[] all_files = TheFolder.GetFiles();

            var all_file_names = all_files.Select(_file_info => _file_info.Name).ToList<string>();
            //var list_all_file_names = new List<string>(all_file_names);
            List<FileInfo> all_files_list = new List<FileInfo>(all_files);
            List<FileInfo> all_files_list_temp = new List<FileInfo>(all_files_list);

            all_file_names = all_files_list_temp.Select(_file_info =>
                 {
                     //linq执行是单线程的
                     Console.WriteLine(_file_info.Name + "  " + Thread.CurrentThread.ManagedThreadId.ToString());
                     Thread.Sleep(3000);
                     all_files_list.RemoveAt(0);
                     return _file_info.Name;
                 }).ToList<string>();



        }
    }
}
