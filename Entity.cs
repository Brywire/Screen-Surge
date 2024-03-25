using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

abstract public class Entity
{
    protected Vector2 position;
    protected Texture2D texture;
    protected Vector2 direction;
    protected float speed;
    protected float scaleFactor;

    public Entity(Vector2 direction)
    {
        this.direction = direction;
    }

    // Rotation angle to cursor
    public virtual float Angle
    {
        get
        {
            return MathF.Atan2(direction.Y, direction.X) * (180.0f / MathF.PI);
        }
    }

    // Create collision area
    public Rectangle getCollisionBox()
    {
        return new Rectangle(
            (int)position.X,
            (int)position.Y,
            texture.Width,
            texture.Height
            
        );
    }

    public abstract void Update();

    public void Draw()
    {
        // Scaled texture
        int scaledWidth = (int)(texture.Width * scaleFactor);
        int scaledHeight = (int)(texture.Height * scaleFactor);

        // Calculate the rectangle for the destination area considering the scaled dimensions
        Rectangle destRec = new Rectangle(
            (int)position.X,
            (int)position.Y,
            scaledWidth,
            scaledHeight
        );
        
        // Origin is now the center of the scaled image
        Vector2 pivot = new Vector2(scaledWidth / 2, scaledHeight / 2);

        // Draw the texture with the scaling applied
        Raylib.DrawTexturePro(
            texture,
            new Rectangle(0, 0, texture.Width, texture.Height),
            destRec,
            pivot, Angle,
            Color.White
        );
    }

    public void Destroy()
    {
        Raylib.UnloadTexture(texture);
    }

    // public void drawrex()
    // {
    //     DrawRectangle(position.X, this.position.Y, this.scaledWidth)
    // }
}
