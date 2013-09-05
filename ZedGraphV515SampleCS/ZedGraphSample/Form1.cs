using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace ZedGraphSample
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load( object sender, EventArgs e )
		{
            //CreateGraph_One(this.zedGraphControl1);//一条曲线

            CreateGraph_Two(this.zedGraphControl1);  //两条曲线

            //CreateGraph_Date(this.zedGraphControl1);  //日期

            //CreateGraph_Chart(this.zedGraphControl1);  //图表


            //CreateGraph_jingzhi(this.zedGraphControl1);
            this.Init_Timer();
            this.Timer1.Start();

            //SetSize();
		}


        private void Form1_Resize(object sender, EventArgs e)
        {
            //SetSize();
        }

        private void SetSize()
        {
            //zg1.Location = new Point(10, 10);
            //// Leave a small margin around the outside of the control
            //zg1.Size = new Size(this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20);
        }

        #region 样例
        private void CreateGraph_One( ZedGraphControl zgc )
		{
			GraphPane myPane = zgc.GraphPane;

			// Set the titles and axis labels
			myPane.Title.Text = "曲线示例";
			myPane.XAxis.Title.Text = "Xxx轴";
            myPane.YAxis.Title.Text = "Yyy轴";

			// Make up some data points from the Sine function
			PointPairList list = new PointPairList();
			for ( double x = 0; x < 36; x++ )
			{
				double y = Math.Sin( x * Math.PI / 15.0 );

				list.Add( x, y );
			}

            // Generate a blue curve with circle symbols, and "My Curve 2" in the legend
            LineItem myCurve = myPane.AddCurve("My Curve", list, Color.Blue, SymbolType.Circle);
            // Fill the area under the curve with a white-red gradient at 45 degrees
            myCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);
            // Make the symbols opaque by filling them with white
            myCurve.Symbol.Fill = new Fill(Color.White);

            // Fill the axis background with a color gradient
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45F);

            // Fill the pane background with a color gradient
            myPane.Fill = new Fill(Color.White, Color.FromArgb(220, 220, 255), 45F);

            // Calculate the Axis Scale Ranges
            zgc.AxisChange();
        }

        private void CreateGraph_Two(ZedGraphControl zgc)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

         
            // Make up some data arrays based on the Sine function
            double x, y1, y2;
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            for (int i = 0; i < 36; i++)
            {
                x = (double)i + 5;
                y1 = 1.5 + Math.Sin((double)i * 0.2);
                y2 = 3.0 * (1.5 + Math.Sin((double)i * 0.2));
                list1.Add(x, y1);
                list2.Add(x, y2);
            }

            // Generate a red curve with diamond
            // symbols, and "Porsche" in the legend
            LineItem myCurve = myPane.AddCurve("曲线1", list1, Color.Red, SymbolType.Diamond);

            // Generate a blue curve with circle
            // symbols, and "Piper" in the legend
            LineItem myCurve2 = myPane.AddCurve("曲线2", list2, Color.Blue, SymbolType.Circle);

            // Tell ZedGraph to refigure the
            // axes since the data have changed

            try
            {
                // Change the color of the title
                myPane.Title.FontSpec.FontColor = Color.Green;

                // Add gridlines to the plot, and make them gray
                myPane.XAxis.MajorGrid.IsVisible = true;
                myPane.YAxis.MajorGrid.IsVisible = true;
                myPane.XAxis.MajorGrid.Color = Color.LightGray;
                myPane.YAxis.MajorGrid.Color = Color.LightGray;

                // Move the legend location
                myPane.Legend.Position = ZedGraph.LegendPos.Bottom;

                // Make both curves thicker
                myCurve.Line.Width = 2.0F;
                myCurve2.Line.Width = 2.0F;

                // Fill the area under the curves
                myCurve.Line.Fill = new Fill(Color.White, Color.Red, 45F);
                myCurve2.Line.Fill = new Fill(Color.White, Color.Blue, 45F);

                // Increase the symbol sizes, and fill them with solid white
                myCurve.Symbol.Size = 8.0F;
                myCurve2.Symbol.Size = 8.0F;
                myCurve.Symbol.Fill = new Fill(Color.White);
                myCurve2.Symbol.Fill = new Fill(Color.White);

                // Add a background gradient fill to the axis frame
                myPane.Chart.Fill = new Fill(Color.White,
                    Color.FromArgb(255, 255, 210), -45F);

                // Add a caption and an arrow
                TextObj myText = new TextObj("Interesting\nPoint", 230F, 70F);
                myText.FontSpec.FontColor = Color.Red;
                myText.Location.AlignH = AlignH.Center;
                myText.Location.AlignV = AlignV.Top;
                myPane.GraphObjList.Add(myText);
                ArrowObj myArrow = new ArrowObj(Color.Red, 12F, 230F, 70F, 280F, 55F);
                myPane.GraphObjList.Add(myArrow);//The final "dressed-up" graph will look like this:

            }
            catch { }

            zgc.AxisChange();
        }

        protected double CalcStepSize(double range, double targetSteps)
        {
            // Calculate an initial guess at step size
            double tempStep = range / targetSteps;

            // Get the magnitude of the step size
            double mag = Math.Floor(Math.Log10(tempStep));
            double magPow = Math.Pow((double)10.0, mag);

            // Calculate most significant digit of the new step size
            double magMsd = ((int)(tempStep / magPow + .5));

            // promote the MSD to either 1, 2, or 5
            if (magMsd > 5.0)
                magMsd = 10.0;
            else if (magMsd > 2.0)
                magMsd = 5.0;
            else if (magMsd > 1.0)
                magMsd = 2.0;

            return magMsd * magPow;
        }


        private void CreateGraph_Date(ZedGraphControl zg1)
        {
            // Get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;

      

            // Make up some random data points
            double x, y;
            PointPairList list = new PointPairList();
            for (int i = 0; i < 36; i++)
            {
                x = (double)new XDate(1995, 5, i + 11);
                y = Math.Sin((double)i * Math.PI / 15.0);
                list.Add(x, y);
            }

            // Generate a red curve with diamond
            // symbols, and "My Curve" in the legend
            CurveItem myCurve = myPane.AddCurve("曲线1", list, Color.Red, SymbolType.Diamond);

            // Set the XAxis to date type
            myPane.XAxis.Type = AxisType.Date;

            // Tell ZedGraph to refigure the axes since the data 
            // have changed
            zg1.AxisChange();
        }


        // Build the Chart
        private void CreateGraph_Chart(ZedGraphControl zg1)
        {
            // get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;


            // Make up some random data points
            string[] labels = { "Panther", "Lion", "Cheetah", "Cougar", "Tiger", "Leopard" };
            double[] y = { 100, 115, 75, 22, 98, 40 };
            double[] y2 = { 90, 100, 95, 35, 80, 35 };
            double[] y3 = { 80, 110, 65, 15, 54, 67 };
            double[] y4 = { 120, 125, 100, 40, 105, 75 };

            // Generate a red bar with "Curve 1" in the legend
            BarItem myBar = myPane.AddBar("Curve 1", null, y, Color.Red);
            myBar.Bar.Fill = new Fill(Color.Red, Color.White, Color.Red);

            // Generate a blue bar with "Curve 2" in the legend
            myBar = myPane.AddBar("Curve 2", null, y2, Color.Blue);
            myBar.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue);

            // Generate a green bar with "Curve 3" in the legend
            myBar = myPane.AddBar("Curve 3", null, y3, Color.Green);
            myBar.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green);

            // Generate a black line with "Curve 4" in the legend
            LineItem myCurve = myPane.AddCurve("Curve 4", null, y4, Color.Black, SymbolType.Circle);
            myCurve.Line.Fill = new Fill(Color.White, Color.LightSkyBlue, -45F);

            // Fix up the curve attributes a little
            myCurve.Symbol.Size = 8.0F;
            myCurve.Symbol.Fill = new Fill(Color.White);
            myCurve.Line.Width = 2.0F;

            // Draw the X tics between the labels instead of 
            // at the labels
            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zg1.AxisChange();
        }

        #endregion


        private Random ran = new Random();
        private PointPairList list = new PointPairList();
        private void CreateGraph_jingzhi(ZedGraphControl zg1)
        {
            #region 现实特征设置
            // get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;

            // Change the color of the title
            myPane.Title.FontSpec.FontColor = Color.Green;

            // Add gridlines to the plot, and make them gray
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.LightGray;
            myPane.YAxis.MajorGrid.Color = Color.LightGray;

            myPane.XAxis.Scale.Format = "MM-dd  HH:mm:ss";   //DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") 
             
            myPane.XAxis.Scale.FontSpec.Angle = 90;
            //myPane.XAxis.Scale.FontSpec.Size = 14;  //字号 ,最好不要设置
            //myPane.XAxis.Scale.FontSpec.IsBold = true;
            myPane.XAxis.Scale.FontSpec.Border.Style = System.Drawing.Drawing2D.DashStyle.Solid;

            // Move the legend location
            myPane.Legend.Position = ZedGraph.LegendPos.Bottom;

            // Make both curves thicker
            //myCurve.Line.Width = 2.0F;
            //myCurve2.Line.Width = 2.0F;

            #endregion

            LineItem myCurve;

            zg1.GraphPane.Title.Text = "动态折线图";
            zg1.GraphPane.XAxis.Title.Text = "时间";
            zg1.GraphPane.YAxis.Title.Text = "数量";
            zg1.GraphPane.XAxis.Type = ZedGraph.AxisType.DateAsOrdinal; ;

            
          
            //zg1.GraphPane.XAxis.MajorTic.PenWidth = 8.0F;
           

            for (int i = 0; i <= 100; i++)
            {
                double x = (double)new XDate(DateTime.Now.AddSeconds(-(100 - i)));
                double y = ran.NextDouble();
                list.Add(x, y);
            }
            DateTime dt = DateTime.Now;

            myCurve = zg1.GraphPane.AddCurve("My Curve", list, Color.DarkGreen, SymbolType.None);

            zg1.AxisChange();
            zg1.Refresh();
           
        }

        private Timer Timer1 = new Timer();
        private void Init_Timer()
        {
            Timer1.Interval = 1000;
            Timer1 .Tick +=new EventHandler(Timer1_Tick);

        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.zedGraphControl1.GraphPane.XAxis.Scale.MaxAuto = true;
            double x = (double)new XDate(DateTime.Now);
            double y = ran.NextDouble();
            list.Add(x, y);
            this.zedGraphControl1.AxisChange();
            this.zedGraphControl1.Refresh();


            if (list.Count >= 100)
            {
                list.RemoveAt(0);
            }

        }
    }
}