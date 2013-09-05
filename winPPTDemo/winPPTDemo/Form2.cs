using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Graph = Microsoft.Office.Interop.Graph;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace winPPTDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowPresentation();
            GC.Collect();
        }

        private void ShowPresentation()
        {
            String strTemplate, strPic;
            strTemplate =
            //"C:\\Program Files\\Microsoft Office\\Templates\\Presentation Designs\\Blends.pot";
            @"C:\Users\ssor\Desktop\MyPPT.ppt";
            //strPic = "C:\\Windows\\Blue Lace 16.bmp";
            strPic = @"C:\Users\ssor\Desktop\460.jpg";
            bool bAssistantOn;

            PowerPoint.Application objApp;
            PowerPoint.Presentations objPresSet;
            PowerPoint._Presentation objPres;
            PowerPoint.Slides objSlides;
            PowerPoint._Slide objSlide;
            PowerPoint.TextRange objTextRng;
            PowerPoint.Shapes objShapes;
            PowerPoint.Shape objShape;
            PowerPoint.SlideShowWindows objSSWs;
            PowerPoint.SlideShowTransition objSST;
            PowerPoint.SlideShowSettings objSSS;
            PowerPoint.SlideRange objSldRng;
            Graph.Chart objChart;

            //Create a new presentation based on a template.
            objApp = new PowerPoint.Application();
            objApp.Visible = MsoTriState.msoTrue;
            objPresSet = objApp.Presentations;
            objPres = objPresSet.Open(strTemplate,
            MsoTriState.msoFalse, MsoTriState.msoTrue, MsoTriState.msoTrue);
            objSlides = objPres.Slides;

            //Build Slide #1:
            //Add text to the slide, change the font and insert/position a
            //picture on the first slide.
            objSlide = objSlides.Add(1, PowerPoint.PpSlideLayout.ppLayoutTitleOnly);
            objTextRng = objSlide.Shapes[1].TextFrame.TextRange;
            objTextRng.Text = "My Sample Presentation";
            objTextRng.Font.Name = "Comic Sans MS";
            objTextRng.Font.Size = 48;
            objSlide.Shapes.AddPicture(strPic, MsoTriState.msoFalse, MsoTriState.msoTrue,
            150, 150, 500, 350);

            //Build Slide #2:
            //Add text to the slide title, format the text. Also add a chart to the
            //slide and change the chart type to a 3D pie chart.
            objSlide = objSlides.Add(2, PowerPoint.PpSlideLayout.ppLayoutTitleOnly);
            objTextRng = objSlide.Shapes[1].TextFrame.TextRange;
            objTextRng.Text = "My Chart";
            objTextRng.Font.Name = "Comic Sans MS";
            objTextRng.Font.Size = 48;
            objChart = (Graph.Chart)objSlide.Shapes.AddOLEObject(150, 150, 480, 320,
            "MSGraph.Chart.8", "", MsoTriState.msoFalse, "", 0, "",
            MsoTriState.msoFalse).OLEFormat.Object;
            objChart.ChartType = Graph.XlChartType.xl3DPie;
            objChart.Legend.Position = Graph.XlLegendPosition.xlLegendPositionBottom;
            objChart.HasTitle = true;
            objChart.ChartTitle.Text = "Here it is...";

            //Build Slide #3:
            //Change the background color of this slide only. Add a text effect to the slide
            //and apply various color schemes and shadows to the text effect.
            objSlide = objSlides.Add(3, PowerPoint.PpSlideLayout.ppLayoutBlank);
            objSlide.FollowMasterBackground = MsoTriState.msoFalse;
            objShapes = objSlide.Shapes;
            objShape = objShapes.AddTextEffect(MsoPresetTextEffect.msoTextEffect27,
            "The End", "Impact", 96, MsoTriState.msoFalse, MsoTriState.msoFalse, 230, 200);


            // 自动播放的代码（閞始）
            //Modify the slide show transition settings for all 3 slides in
            //the presentation.
            int[] SlideIdx = new int[3];
            for (int i = 0; i < 3; i++) SlideIdx[i] = i + 1;
            objSldRng = objSlides.Range(SlideIdx);
            objSST = objSldRng.SlideShowTransition;
            objSST.AdvanceOnTime = MsoTriState.msoTrue;
            objSST.AdvanceTime = 3;
            objSST.EntryEffect = PowerPoint.PpEntryEffect.ppEffectBoxOut;

            //Prevent Office Assistant from displaying alert messages:
            bAssistantOn = objApp.Assistant.On;
            objApp.Assistant.On = false;

            //Run the Slide show from slides 1 thru 3.
            objSSS = objPres.SlideShowSettings;
            objSSS.StartingSlide = 1;
            objSSS.EndingSlide = 3;
            objSSS.Run();

            //Wait for the slide show to end.
            objSSWs = objApp.SlideShowWindows;
            while (objSSWs.Count >= 1) System.Threading.Thread.Sleep(100);

            //Reenable Office Assisant, if it was on:
            if (bAssistantOn)
            {
                objApp.Assistant.On = true;
                objApp.Assistant.Visible = false;
            }
            // 自动播放的代码（结束）

            //Close the presentation without saving changes and quit PowerPoint.
            objPres.Close();
            objApp.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path;         //文件路径变量

            PowerPoint.Application pptApp;      //Excel应用程序变量
            PowerPoint.Presentation pptDoc;      //Excel文档变量

            PowerPoint.Presentation pptDoctmp;



            path = @"C:\Users\ssor\Desktop\test.ppt";      //路径
            
            /*
            pptApp = new PowerPoint.ApplicationClass(); //初始化

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

            foreach (PowerPoint.Slide slide in pptDoc.Slides)
            {
                foreach (PowerPoint.Shape shape in slide.Shapes)
                {
                    shape.TextFrame.TextRange.InsertAfter(text);
                }
            }


            //WdSaveFormat为Excel文档的保存格式
            PowerPoint.PpSaveAsFileType format = PowerPoint.PpSaveAsFileType.ppSaveAsDefault;

            //将excelDoc文档对象的内容保存为XLSX文档 
            pptDoc.SaveAs(path, format, Microsoft.Office.Core.MsoTriState.msoFalse);

            //关闭excelDoc文档对象 
            pptDoc.Close();

            //关闭excelApp组件对象 
            pptApp.Quit();

            //Console.WriteLine(path + " 创建完毕！");

            //Console.ReadLine();

            return;
             * */

            string pathHtml = @"C:\Users\ssor\Desktop\MyPPT.html";

            PowerPoint.Application pa = new PowerPoint.ApplicationClass();

            pptDoctmp = pa.Presentations.Open(path, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);
            PowerPoint.PpSaveAsFileType formatTmp = PowerPoint.PpSaveAsFileType.ppSaveAsHTMLDual;
            pptDoctmp.SaveAs(pathHtml, formatTmp, Microsoft.Office.Core.MsoTriState.msoFalse);
            pptDoctmp.Close();
            pa.Quit();
            MessageBox.Show("创建完毕！");
            //Console.WriteLine(pathHtml + " 创建完毕！");
        }

    }
}
