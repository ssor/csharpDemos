using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PPT = Microsoft.Office.Interop.PowerPoint;
using System.Reflection;

namespace ppt2html
{
    class Program
    {
        static void Main(string[] args)
        {

            string path;         //文件路径变量

            PPT.Application pptApp;      //Excel应用程序变量
            PPT.Presentation pptDoc;      //Excel文档变量

            PPT.Presentation pptDoctmp;



            path = @"C:\MyPPT.ppt";      //路径
            pptApp = new PPT.ApplicationClass(); //初始化

            //如果已存在，则删除
            if (File.Exists((string)path))
            {
                File.Delete((string)path);
            }

            //由于使用的是COM库，因此有许多变量需要用Nothing代替
            Object Nothing = Missing.Value;
            pptDoc = pptApp.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoFalse);
            pptDoc.Slides.Add(1, Microsoft.Office.Interop.PowerPoint.PpSlideLayout.ppLayoutText);

            string text = "示例文本";

            foreach (PPT.Slide slide in pptDoc.Slides)
            {
                foreach (PPT.Shape shape in slide.Shapes)
                {
                    shape.TextFrame.TextRange.InsertAfter(text);
                }
            }


            //WdSaveFormat为Excel文档的保存格式
            PPT.PpSaveAsFileType format = PPT.PpSaveAsFileType.ppSaveAsDefault;

            //将excelDoc文档对象的内容保存为XLSX文档 
            pptDoc.SaveAs(path, format, Microsoft.Office.Core.MsoTriState.msoFalse);

            //关闭excelDoc文档对象 
            pptDoc.Close();

            //关闭excelApp组件对象 
            pptApp.Quit();

            Console.WriteLine(path + " 创建完毕！");

            Console.ReadLine();


            string pathHtml = @"c:\MyPPT.html";

            PPT.Application pa = new PPT.ApplicationClass();

            pptDoctmp = pa.Presentations.Open(path, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
            PPT.PpSaveAsFileType formatTmp = PPT.PpSaveAsFileType.ppSaveAsHTML;
            pptDoctmp.SaveAs(pathHtml, formatTmp, Microsoft.Office.Core.MsoTriState.msoFalse);
            pptDoctmp.Close();
            pa.Quit();
            Console.WriteLine(pathHtml + " 创建完毕！");

        }
    }
}
