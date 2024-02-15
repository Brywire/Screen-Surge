using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

public class Entity
{
    public Vector2 Position;
    public Texture2D Texture;

    public Entity(Vector2 position, string texturePath)
    {
        Position = position;
        Texture = Raylib.LoadTexture(texturePath);
    }

    public virtual void Update(float deltaTime)
    {

    }
}
