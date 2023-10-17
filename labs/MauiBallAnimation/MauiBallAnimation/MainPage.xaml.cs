using System.Timers;
using Timer = System.Timers.Timer;

namespace MauiBallAnimation;

public partial class MainPage : ContentPage
{
	private double frameDuration = 30;      // the frame duration in milliseconds
	public Timer timer;						// the timer

	public MainPage()
	{
		InitializeComponent();
	}
	private void ContentPage_Loaded(object sender, EventArgs e)
    {
		timer = new Timer();			// create local instance of timer
		timer.Interval = frameDuration; // set timer interval to the frame duration
		timer.Elapsed += new ElapsedEventHandler(updateBallField);	// create event handler
		timer.Start();		// start the timer
    }

    private void updateBallField(object sender, ElapsedEventArgs e)
    {
        var graphicsView = this.BallGraphicsView;		// create a graphicsView instance fromt the xaml
        //var ballFieldDrawable = (BallField)graphicsView.Drawable;
        //ballFieldDrawable;
        graphicsView.Invalidate();	// invalidate to update
    }

}

