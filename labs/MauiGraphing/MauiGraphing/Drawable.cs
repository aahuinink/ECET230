using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiGraphing
{
    public class LineDrawable : BaseGraphData, IDrawable
    {
        private const int _graphCount = 3;
        private string[] _colorName = new string[_graphCount] {"FF0000", "00FF00", "0000FF" };
        private int[] _lineWidth = new int[_graphCount] { 1, 2, 3 };
        public BaseGraphData[] lineGraphs = new BaseGraphData[_graphCount];

        // default constructor
        public LineDrawable() : base()
        {
            for (int i = 0; i < _graphCount; i++)
            {
                lineGraphs[i] = new BaseGraphData
                    (
                        yaxis: 0,
                        xaxis: 0,
                        lineColor: Color.FromArgb(_colorName[i]),
                        lineSize: _lineWidth[i],
                        newGraph: true
                    );
            }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            for (int graphIndex = 0; graphIndex < lineGraphs.Length; graphIndex++)
            {
                Rect lineGraphRect = new(dirtyRect.X, dirtyRect.Y, dirtyRect.Width, dirtyRect.Height);
                DrawLineGraph(canvas, lineGraphRect, lineGraphs[graphIndex]);
            }
        }

        private void DrawLineGraph(ICanvas canvas, Rect lineGraphRect, BaseGraphData baseGraphData)
        {
            if(baseGraphData.Xaxis < 2)
            {
                baseGraphData.PointArray[baseGraphData.Xaxis] = baseGraphData.Yaxis;
                baseGraphData.Xaxis++;
                return;
            }
            else if (baseGraphData.Xaxis < 1000)
            {
                baseGraphData.PointArray[baseGraphData.Xaxis] = baseGraphData.Yaxis;
                baseGraphData.Xaxis++;
                return;
            }
            else
            {
                baseGraphData.PointArray[999] = baseGraphData.Yaxis;
                for(int i = 0; i < baseGraphData.Xaxis -1; i++)
                {
                    canvas.StrokeColor = baseGraphData.LineColor;
                    canvas.StrokeSize = baseGraphData.LineSize;
                    canvas.DrawLine(i, baseGraphData.PointArray[i], i + 1, baseGraphData.PointArray[i+ 1]);
                }
            }
        }
    }
}
