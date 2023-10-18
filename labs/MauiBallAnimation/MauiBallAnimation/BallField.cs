using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBallAnimation;

public class BallField :IDrawable //
{
    public Ball[] Balls;    // array of balls
    
    /// <summary>
    /// The number of balls in the animation
    /// </summary>
    public int BallCount
    {
        get { return _ballCount; }
        set { _ballCount = value;
            newBalls(_ballCount);    // create new Balls array when the value of ball count is changed
        }
    }
    private int _ballCount = 2;   // the number of balls on the screen, initialized to 2

    /// <summary>
    /// The relative ball speed of the balls in pixels per ms, chosen by the user
    /// </summary>
    public int BallSpeed
    {
        get { return _ballSpeed; }
        set { _ballSpeed = value;}
    }

    private int _ballSpeed = 10;    // the relative speed of the ball in pixels per ms

    /// <summary>
    /// The frame rate of the animation
    /// </summary>
    public double FrameRate
    {
        get { return _frameRate; }
        set { _frameRate = value;}
    }
    private double _frameRate = 30; // the frame duration in ms (i know this is technically a period and not a frequency...)

    /// <summary>
    /// Width property of the graphics drawable
    /// </summary>
    public double Width
    {
        get { return _width; }
        set { _width = value;}
    }
    private double _width;

    /// <summary>
    /// Height property of the graphics drawable
    /// </summary>
    public double Height
    {
        get { return _height; }
        set { _height = value; }
    }
    private double _height;


    public BallField()
    {
        newBalls(_ballCount);    // create an array of size ballCount
    }

    /// <summary>
    /// Creates a new array of balls with the size of ballCount
    /// </summary>
    /// <param name="ballCount">The size of the Balls array</param>
    private void newBalls(int ballCount)
    {
        Balls=new Ball[ballCount];  // creates an array of size ballCount
        Random rand = new();        // create random number generator
        for (int i = 0; i < ballCount; i++)             // create a Ball with random properties
        {
            double X = rand.NextDouble() * Width;
            double Y = rand.NextDouble() * Height;
            double Radius = rand.NextDouble() * 5 + 5;
            double XVel = (rand.NextDouble() - .5);
            double YVel = (rand.NextDouble() - .5);
            byte R = (byte)(rand.Next(50, 255));
            byte G = (byte)(rand.Next(50, 255));
            byte B = (byte)(rand.Next(50, 255));
            Balls[i] = new Ball
                (
                    x: X,
                    y: Y,
                    radius: Radius,
                    xVel : XVel,
                    yVel : YVel,
                    r : R,
                    g: G,
                    b: B
                );
        } 
    }

    /// <summary>
    /// Draws each ball in the Balls array
    /// </summary>
    /// <param name="canvas">The canvas to draw to</param>
    /// <param name="dirtyRect">The physical properties of the canvas</param>
    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        foreach (Ball ball in Balls)
        {
            ball.Advance(_ballSpeed*_frameRate, _width, _height);     // The number
            ball.Draw(canvas);
        }
    }
}

