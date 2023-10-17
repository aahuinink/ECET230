using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBallAnimation;

public class BallField :IDrawable //
{
    Ball ball = new Ball(100, 100, 20, 10, 11, 128, 128, 128);
    public BallField()
    {
        
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        ball.Advance(25, 1000, 1000);
        ball.Draw(canvas);
    }
}

