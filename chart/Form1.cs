using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace chart
{
    public partial class Form1 : Form
    {
        List<double> xVals = new List<double>();
        List<double> yVals = new List<double>();

        public Random rnd = new Random();
        double tick = 0.0;
        public Form1()
        {
            InitializeComponent();
            LineGraph(0,10,0,0,15,5,3,0);
            configStripline();
        }

        public void LineGraph(int xMin,int xMax,int xInterval,int yMin,int yMax,int yInterval,int borderWidth,int minorYgrid)
        {
            chart.ChartAreas[0].AxisX.Minimum=xMin;
            chart.ChartAreas[0].AxisY.Minimum=yMin;
            chart.ChartAreas[0].AxisX.Maximum=xMax;
            chart.ChartAreas[0].AxisY.Maximum=yMax;
            
            chart.ChartAreas[0].AxisY.Interval = yInterval;
            chart.ChartAreas[0].AxisY.MinorGrid.Enabled = true;
            chart.ChartAreas[0].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
            chart.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.LightBlue;
            chart.ChartAreas[0].AxisY.MinorGrid.Interval = minorYgrid;
            chart.Series[0].BorderWidth = borderWidth;
            chart.Legends.Clear();
            chart.Series[0].ChartType = SeriesChartType.Line;
        }

        private void chcColumn_CheckedChanged(object sender, EventArgs e)
        {
            if(chcColumn.Checked)
                chart.Series[0].ChartType = SeriesChartType.Column;
            else
                chart.Series[0].ChartType = SeriesChartType.Line;
        }

        private void chc3D_CheckedChanged(object sender, EventArgs e)
        {
            if(chc3D.Checked)
                chart.ChartAreas[0].Area3DStyle.Enable3D = true;
            else
                chart.ChartAreas[0].Area3DStyle.Enable3D = false;
        }

        private void chcFast_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFast.Checked)
                timerChart.Interval = 250;
            else
                timerChart.Interval = 1000;
        }

        private void timerChart_Tick(object sender, EventArgs e)
        {
            xVals.Add(tick);
            yVals.Add(10.0 * rnd.NextDouble());

            if (tick > 20.0)
            {
                yVals.RemoveAt(0);
                xVals.RemoveAt(0);
            }
            chart.ChartAreas[0].AxisX.Minimum = xVals[0];
            chart.ChartAreas[0].AxisX.Maximum = tick;

            chart.Series[0].Points.DataBindXY(xVals, yVals);
            tick = tick + 0.5;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timerChart.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timerChart.Stop();
        }

        private void trackRotation_Scroll(object sender, EventArgs e)
        {
            //chart.ChartAreas[0].Area3DStyle.Inclination = trackRotation.Value;
            chart.ChartAreas[0].Area3DStyle.Rotation = trackRotation.Value*10;
        }

        private void chcMarkers_CheckedChanged(object sender, EventArgs e)
        {
            chart.Series[0].MarkerSize = 10;
            chart.Series[0].MarkerColor = Color.Tomato;
            if (chcMarkers.Checked)
                chart.Series[0].MarkerStyle = MarkerStyle.Square;
            else
                chart.Series[0].MarkerStyle = MarkerStyle.None;
        }

        private void trackWidth_Scroll(object sender, EventArgs e)
        {
            chart.Series[0].BorderWidth = trackWidth.Value;
        }

        private void trackDepth_Scroll(object sender, EventArgs e)
        {
            chart.ChartAreas[0].Area3DStyle.PointDepth = trackDepth.Value*10;
        }

        public void configStripline()
        {
            StripLine stripLine = new StripLine();
            stripLine.StripWidth = 7;
            stripLine.Interval = 0;
            stripLine.IntervalOffset = 3;
            stripLine.BackColor = Color.FromArgb(120, Color.Red);

            chart.ChartAreas[0].AxisY.StripLines.Add(stripLine);
        }
    }
}
