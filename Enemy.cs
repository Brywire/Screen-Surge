using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Enemy : Entity
{
    public Vector2 targetPosition;
    public Enemy() : base(new Vector2(0, 0))
    {
        texture = Raylib.LoadTexture("resources/enemySprite.png");
        speed = 3.0f; // Set the speed for the enemy
        scaleFactor = 1.0f;
        position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);

        targetPosition = Vector2.Zero;
    }

    public override void Update()
    {
        // Calculate the direction towards the target
        Vector2 directionToTarget = Vector2.Normalize(targetPosition - position);

        // Move the enemy towards the player
        position += directionToTarget * speed;
    }
}
