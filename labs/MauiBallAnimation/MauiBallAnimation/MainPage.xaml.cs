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
		timer.Start();      // start the timer
    }

    private void updateBallField(object sender, ElapsedEventArgs e)
    {
		var graphicsView = this.BallGraphicsView;

		var ballView = (BallField)graphicsView.Drawable;
		ballView.Width = graphicsView.Width;
		ballView.Height = graphicsView.Height;
		ballView.Height = graphicsView.Height;
		
        graphicsView.Invalidate();	// invalidate to update
    }

    private void ContentPage_SizeChanged(object sender, EventArgs e)
    {
		var graphicsView = this.BallGraphicsView;
		var ballFieldDrawable = (BallField)graphicsView.Drawable;

		if (Width <= 0 || Height <= 0)
		{
			return;
		}
		graphicsView.WidthRequest = Width;
		graphicsView.HeightRequest = Height;
		ballFieldDrawable.Width = Width;
		ballFieldDrawable.Height = Height;
    }

    private void BallSpeed_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		var ballFieldDrawable = (BallField)this.BallGraphicsView.Drawable;		// create drawable
		int speed;																// create int to hold speed value
		if(int.TryParse(e.NewTextValue, out speed))								// if valid input
		{
            BallSpeed_Entry.Text = e.NewTextValue;								// change the text
            ballFieldDrawable.BallSpeed = speed;								// set the new speed
			return;
		}	
		BallSpeed_Entry.Text = e.NewTextValue;									// otherwise just change the text
    }


    private void BallCount_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var ballFieldDrawable = (BallField)this.BallGraphicsView.Drawable;	// create drawable
		int count;															// create int to hold count value from entry
		if( int.TryParse(e.NewTextValue,out count))							// if valid
		{
			BallCount_Entry.Text = e.NewTextValue;							// change the text
			ballFieldDrawable.BallCount = count;                            // set the new ball count
			
			return;
		}
		ballFieldDrawable.BallCount = count; return;						// otherwise just change the text
    }

    private void FrameRate_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
		var ballFieldDrawable = (BallField)this.BallGraphicsView.Drawable;
		if(double.TryParse(e.NewTextValue, out frameDuration))      // if input is valid
		{
			timer.Interval = (frameDuration > 16) ? frameDuration : 16;         // set the timer interval to the new frame duration, minimum allowable is 16ms
			FrameRate_Entry.Text = frameDuration.ToString();                    // set the text to the frame duration	
			ballFieldDrawable.FrameRate = frameDuration;						// set the frame rate of the ball field to prevent ball slowdown as frame rate drops
			return;	
        }

		FrameRate_Entry.Text = frameDuration.ToString();			// otherwise just set to the current frame duration 
    }
}

