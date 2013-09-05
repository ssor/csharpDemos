using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linqDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> strList = new List<string>();

            var newList = from s in strList select s;

            Get_des_folder_existed_file("10001", @"C:\\davs");
        }
        static void Get_des_folder_existed_file(string _file_name, string _file_path)
        {
            DirectoryInfo TheFolder = new DirectoryInfo(_file_path);
            FileInfo[] all_files = TheFolder.GetFiles();

            var all_file_names = all_files.Select(_file_info => _file_info.Name).ToList<string>();
            //var list_all_file_names = new List<string>(all_file_names);

        }
    }
}
