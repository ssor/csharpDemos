using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace invoke_by_php_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length>0)
            {
                Console.Write("hello,abc!--"+args[0]);
            }
            else
            Console.Write("hello,abc!");
        }
    }
}
