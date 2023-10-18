using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBallAnimation;

public class Ball
{
    public double X;        // x coordinate of the ball
    public double Y;        // y coordinate of the ball
    public double Radius;   // ball radius
    public double XVel;     // velocity in x direction
    public double YVel;     // velocity in y direction
    public byte R, G, B;    // ball colour values

    // class constructor
    public Ball(double x, double y, double radius, double xVel, double yVel, byte r, byte g, byte b)
    {
        X = x;
        Y = y;
        Radius = radius;
        XVel = xVel;
        YVel = yVel;
        R = r;
        G = g;
        B = b;
    }

    /// <summary>
    /// Moves the ball and performs any bounce operations
    /// </summary>
    /// <param name="BallSpeed"></param>
    /// <param name="FrameDuration"></param>
    /// <param name="Width"></param>
    /// <param name="Height"></param>
    public void Advance(double BallSpeed, double FrameDuration, double Width, double Height)
    {
        MoveBall(BallSpeed, FrameDuration);
        Bounce(Width, Height);
    }

    /// <summary>
    /// Identifies and handles when a ball should bounce
    /// </summary>
    /// <param name="Width">The width of the screen</param>
    /// <param name="Height">The height of the screen</param>
    private void Bounce(double Width, double Height)
    {
        double minX = Radius;           // minimum x value of the ball
        double minY = Radius;           // minimum y value of the ball
        double maxX = Width - Radius;   // maximum x value of the ball
        double maxY = Height - Radius;  // maximum y value of the ball

        // when ball reaches the left wall
        if(X < minX)
        {
            double over = minX - X;     // calculate position overage
            X = minX + over;            // reset ball positin
            XVel = -XVel;               // reverse XVel direction
        }
        // when ball reaches the bottom
        if(Y < minY)
        {
            double over = minY - Y;     // calculate position overage
            Y = minY + over;            // reset ball position
            YVel= -YVel;                // reverse YVel direction
        }

        // do the same for the rest of the page limits
        if (X > maxX)
        {
            double over = X - maxX;
            X = maxX - over;
            XVel = -XVel;
        }
        if (Y > maxY)
        {
            double over = Y - maxY;
            Y = maxY - over;
            YVel=-YVel;
        }
    }

    /// <summary>
    /// Moves the ball position
    /// </summary>
    /// <param name="BallSpeed">The relative ball speed selected by the user in pixels/ms</param>
    /// <param name="FrameDuration">The number of ms that have passed since the last frame</param>
    private void MoveBall(double BallSpeed, double FrameDuration)
    {
        X += XVel * BallSpeed * FrameDuration/1000.0;  // increases the x coordinate by the number of milli-pixels since the last frame, weighted by the ball's velocity
        Y += YVel * BallSpeed * FrameDuration/1000.0;  // increases the y coordinate by the number of milli-pixels since the last frame, weighted by the ball's velocity
    }

    /// <summary>
    /// Draws the ball to the canvas
    /// </summary>
    /// <param name="canvas">The canvas that the objects should be drawn on</param>
    public void Draw(ICanvas canvas)
    {
        canvas.FillColor = Color.FromRgb(R,G,B);                // creates a colour from the ball's colour
        canvas.FillCircle((float)X, (float)Y, (float)Radius);   // draws the ball
    }
}

