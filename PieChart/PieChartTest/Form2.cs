using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Nexus.Windows.Forms;

namespace PieChartTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            PieChart1.Items.Add(new PieChartItem(10, Color.BurlyWood, "Tan", "Tan tool tip", 0));
            PieChart1.Items.Add(new PieChartItem(10, Color.Gold, "Gold", "Gold tool tip", 0));
            PieChart1.Items.Add(new PieChartItem(20, Color.Chocolate, "Brown", "Brown tool tip", 30));
            PieChart1.Items.Add(new PieChartItem(10, Color.DarkRed, "Red", "Red tool tip", 0));


            PieChart1.ItemStyle.SurfaceAlphaTransparency = 0.75F;
            PieChart1.FocusedItemStyle.SurfaceAlphaTransparency = 0.75F;
            PieChart1.FocusedItemStyle.SurfaceBrightnessFactor = 0.3F;
            PieChart1.Inclination = (float)(50 * Math.PI / 180);
            PieChart1.AutoSizePie = true;

        }
    }
}
