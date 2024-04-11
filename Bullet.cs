using System.Numerics;
using Raylib_cs;

class Bullet : Entity
{
    public Bullet(Vector2 direction, Vector2 startPosition) : base(direction)
    {
        texture = Raylib.LoadTexture("resources/bulletSprite.png");
        speed = 15.0f;
        scaleFactor = 1.5f;
        Position = startPosition;
    }

    public override float Angle
    {
        get
        {
            return MathF.Atan2(direction.Y, direction.X) * (180.0f / MathF.PI);
        }
    }

    public override void Update()
    {
        Position += direction * speed;
    }

    public bool hasHitWindowBorder(int windowWidth, int windowHeight)
    {
        // Check if the bullet's position is outside the window
        if (Position.X < 0 || 
        Position.X > windowWidth || 
        Position.Y < 0 || 
        Position.Y > windowHeight)
        {
            return true;
        }
        return false;
    }
}
