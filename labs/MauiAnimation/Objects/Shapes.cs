namespace AnimationObjects
{
    // All the code in this file is included in all platforms.
    public class Ball
    {
        public float X;             // X coordinate of the ball
        public float Y;             // Y coordinate of the ball
        public float R;             // Radius of the ball
        public int VelX;            // Velocity in x dimension
        public int VelY;            // Velocity in the y dimension
        public Color FillColor;     // Ball colour

        // class constructor
        public Ball(float x, float y, float r, int velX, int velY, Color fillColor)
        {
            X = x; 
            Y = y;
            R = r; 
            VelX = velX;
            VelY = velY;
            FillColor = fillColor;
        }

        // Bounce detection function
        public void Bounce()
    }
}