using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Enemy : Entity
{
    Vector2 targetPosition;
public Enemy() : base(new Vector2(0, 0))
    {
        texture = Raylib.LoadTexture("resources/enemySprite.png");
        speed = 2.0f; // Set the speed for the enemy
        scaleFactor = 1.0f;
        position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);

        targetPosition = Vector2.Zero;
    }

    public override void Update()
    {
        // Get the player's current position
       // Vector2 playerPosition = MyScene.playerShip.Position;

        // Calculate the direction towards the player
        Vector2 directionToPlayer = Vector2.Normalize(targetPosition - position);

        // Move the enemy towards the player
        position += directionToPlayer * speed;
    }
}
