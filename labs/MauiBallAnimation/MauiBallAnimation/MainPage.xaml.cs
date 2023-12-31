﻿using System.Timers;
using System.Windows.Input;
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


	/// ----- EVENT HANDLERS ----- ///
	
	/// <summary>
	/// handles the content page loading event
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void ContentPage_Loaded(object sender, EventArgs e)
    {
        timer = new Timer();			// create local instance of timer
		timer.Interval = frameDuration; // set timer interval to the frame duration
		timer.Elapsed += new ElapsedEventHandler(updateBallField);	// create event handler
		timer.Start();      // start the timer
    }

	/// <summary>
	/// updates the ball field when the timer elapses
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void updateBallField(object sender, ElapsedEventArgs e)
    {
		var graphicsView = this.BallGraphicsView;

		var ballView = (BallField)graphicsView.Drawable;
		ballView.Width = graphicsView.Width;
		ballView.Height = graphicsView.Height;
		ballView.Height = graphicsView.Height;
		
        graphicsView.Invalidate();  // invalidate to update
		return;
    }

	/// <summary>
	/// Handles changing content page size event
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
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
		return;
    }

	/// <summary>
	/// Ball speed entry text change event handler
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
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
		BallSpeed_Entry.Text = e.NewTextValue;                                  // otherwise just change the text
		return;
    }

	/// <summary>
	/// handles a ball count entry text changed event
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void BallCount_Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        var ballFieldDrawable = (BallField)this.BallGraphicsView.Drawable;	// create drawable
		int count;															// create int to hold count value from entry
		if( int.TryParse(e.NewTextValue, out count))							// if valid
		{
			BallCount_Entry.Text = e.NewTextValue;							// change the text
			ballFieldDrawable.BallCount = count;                            // set the new ball count
			
			return;
		}
		ballFieldDrawable.BallCount = count; return;						// otherwise just change the text
    }

	/// <summary>
	/// Handles the a new frame rate request from the user.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void FrameRate_Entry_Completed(object sender, EventArgs e)
    {
		var ballFieldDrawable = (BallField)this.BallGraphicsView.Drawable;
		double UserRequest;												// variable to hold user-requested frame rate
		if(double.TryParse(((Entry)sender).Text, out UserRequest))		// if the user input is valid
		{
			frameDuration = (UserRequest > 16) ? UserRequest : 16;              // minimum duration is 16ms
			timer.Interval = frameDuration;										// set timer interval to the frame duration         
			FrameRate_Entry.Text = frameDuration.ToString();                    // set the text to the frame duration	
			ballFieldDrawable.FrameRate = frameDuration;						// set the frame rate of the ball field to prevent ball slowdown as frame rate drops
			return;	
        }

		FrameRate_Entry.Text = frameDuration.ToString();            // otherwise just set to the current frame duration 
		return;
    }
}

