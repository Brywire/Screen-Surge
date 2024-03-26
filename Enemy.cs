using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Enemy : Entity
{
    public Vector2 Position;

    public Texture2D Texture;
    public Vector2 targetPosition;
    public Enemy() : base(new Vector2(0, 0))
    {
        texture = Raylib.LoadTexture("resources/enemySprite.png");
        speed = 3.0f;
        scaleFactor = 1.0f;
        position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);

        Position = position;
        Texture = texture;

        targetPosition = Vector2.Zero;
    }

    public override void Update()
    {
        // Calculate the direction towards the target
        Vector2 directionToTarget = Vector2.Normalize(targetPosition - position);

        // Rotate sprite towards the target
         direction = directionToTarget;

        // Move the enemy towards the player
        position += directionToTarget * speed;
    }
}
