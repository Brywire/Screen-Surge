using System.Numerics;
using ScreenSurge;

public enum BorderSide
    {
        None,
        Top,
        Right,
        Bottom,
        Left
    }

class Bullet : Entity
{
    public Bullet(Vector2 direction, Vector2 startPosition) : base(direction, "bulletSprite.png")
    {
        // Load bullet texture using ResourceManager
        texture = ResourceManager.Instance.LoadTexture(textureName);
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

    public BorderSide hasHitWindowBorder(int windowWidth, int windowHeight)
    {
        if (Position.X <= 0) return BorderSide.Left;
        if (Position.X >= windowWidth) return BorderSide.Right;
        if (Position.Y <= 0) return BorderSide.Top;
        if (Position.Y >= windowHeight) return BorderSide.Bottom;
        return BorderSide.None;
    }
}
