namespace MauiBallAnimation;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}
	private void ContentPage_Loaded(object sender, EventArgs e)
    {
		DrawBall();
    }

	private void DrawBall()
	{
		var graphicsView = this.BallGraphicsView;
		//var ballFieldDrawable = (BallField)graphicsView.Drawable;
		//ballFieldDrawable;
		graphicsView.Invalidate();
	}
}

