using System.Timers;

namespace MauiGraphing;

public partial class MainPage : ContentPage
{
	public int Yaxis = 0;
	public double degrees = 0;
	public int count = 0;
	public int graphHeight = 500;

	public MainPage()
	{
		InitializeComponent();
        Loaded += MainPage_Loaded;
	}

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        var timer = new System.Timers.Timer(16);
		timer.Elapsed += new ElapsedEventHandler(DrawNewPointOnGraph);
		timer.Start();
    }

    private void DrawNewPointOnGraph(object sender, ElapsedEventArgs e)
    {
		var graphicsView = this.LineGraphView;
		var lineGraphDrawable = (LineDrawable)graphicsView.Drawable;
		double angle = Math.PI * degrees++ / 180;
		lineGraphDrawable.lineGraphs[0].Yaxis=(int)(graphHeight/2*Math.Sin(angle))+graphHeight/2;
		lineGraphDrawable.lineGraphs[1].Yaxis = (int)(0.002 * Math.Pow(500 - count, 2) + graphHeight);
		lineGraphDrawable.lineGraphs[2].Yaxis = count--;
		if(count < 0)
		{
			count = graphHeight;
		}

		graphicsView.Invalidate();
    }
}

