using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBallAnimation;

public class BallField :IDrawable //
{
    public Ball[] Balls;    // array of balls

    public int ballCount { get; set; } = 2;
    public int ballSpeed { get; set; } = 100;
    public double width { get; set; } = 1000;
    public double height { get; set; } = 1000;

    public BallField()
    {
        newBalls(ballCount);    // create an array of size ballCount
    }

    private void newBalls(int ballCount)
    {
        Balls=new Ball[ballCount];  // creates an array of size ballCount
        Random rand = new();        // create random number generator
        for (int i = 0; i < ballCount; i++)             // create a Ball with random properties
        {
            double X = rand.NextDouble() * width;
            double Y = rand.NextDouble() * height;
            double Radius = rand.NextDouble() * 5 + 5;
            double XVel = rand.NextDouble() - .5;
            double YVel = rand.NextDouble() - .5;
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

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        foreach (Ball ball in Balls)
        {
            ball.Advance(ballSpeed, width, height);
            ball.Draw(canvas);
        }
        
    }
}

