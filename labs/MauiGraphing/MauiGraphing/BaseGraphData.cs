using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiGraphing
{
    public class BaseGraphData
    {
        public int Yaxis { get; set; } = 0;
        public int Xaxis { get; set; } = 0;

        public int[] PointArray { get; set; }

        public Color LineColor { get; set; }

        public int LineSize { get; set; }

        public bool NewGraph { get; set; } = true;

        //default constructor
        public BaseGraphData() { }

        // constructor overload 1
        public BaseGraphData(int yaxis, int xaxis, Color lineColor, int lineSize, bool newGraph)
        {
            Yaxis = yaxis;
            Xaxis = xaxis;
            PointArray = new int[1000];
            this.LineColor = lineColor;
            this.LineSize = lineSize;
            this.NewGraph = newGraph;
        }
    }
}
