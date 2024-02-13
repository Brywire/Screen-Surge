using System.Numerics;
using Raylib_cs;

class Bullet
{
    public Vector2 Position;
    public Vector2 Direction;
    public Texture2D Texture;
    public float Speed =  15.0f;
    private float scaleFactor =  0.7f; // Scale factor for bullet size

    public Bullet(Vector2 position, Vector2 direction, string texturePath)
    {
        Position = position;
        Direction = direction;
        Texture = Raylib.LoadTexture(texturePath);
    }

    public float Angle
    {
        get
        {
            return MathF.Atan2(Direction.Y, Direction.X) * (180.0f / MathF.PI) + 90.0f;
        }
    }

    public void Update()
    {
        Position += Direction * Speed;
        // Add any logic to remove the bullet if it goes off-screen or hits something
    }

    public void Draw()
    {
        //Scaled texture dimensions
        int scaledWidth = (int)(Texture.Width * scaleFactor);
        int scaledHeight = (int)(Texture.Height * scaleFactor);
        
        //Calculate the rectangle for the destination area considering the scaled dimensions
        Rectangle destRec = new Rectangle(
            (int)Position.X - scaledWidth /  2,
            (int)Position.Y - scaledHeight /  2,
            scaledWidth,
            scaledHeight
        );
        
        //Draw the bullet texture with the scaling applied
        Raylib.DrawTexturePro(
            Texture,
            new Rectangle(0,  0, Texture.Width, Texture.Height),
            destRec,
            new Vector2(scaledWidth /  2, scaledHeight /  2), // Origin is now the center of the scaled image
            Angle,
            Color.White
        );
    }
}
