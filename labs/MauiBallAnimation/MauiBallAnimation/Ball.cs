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

    public void Advance(double ballSpeed, double width, double height)
    {
        MoveBall(ballSpeed);
        Bounce(width, height);
    }

    private void Bounce(double width, double height)
    {
        double minX = Radius;           // minimum x value of the ball
        double minY = Radius;           // minimum y value of the ball
        double maxX = width - Radius;   // maximum x value of the ball
        double maxY = height - Radius;  // maximum y value of the ball

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

    private void MoveBall(double ballSpeed)
    {
        X += XVel * ballSpeed;
        Y += YVel * ballSpeed;
    }

    public void Draw(ICanvas canvas)
    {
        canvas.FillColor = Color.FromRgb(R,G,B);
        canvas.FillCircle((float)X, (float)Y, (float)Radius);
    }
}

