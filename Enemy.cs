using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Enemy : Entity
{
    public Vector2 targetPosition;
    public Enemy(Vector2 position) : base(position, "enemySprite.png")
    {
        // Load enemy texture using ResourceManager
        texture = ResourceManager.Instance.LoadTexture(textureName);

        speed = 3.0f;
        scaleFactor = 1.0f;

        targetPosition = Vector2.Zero;
    }

    public override void Update()
    {
        // Calculate the direction towards the target
        Vector2 directionToTarget = Vector2.Normalize(targetPosition - Position);

        // Rotate sprite towards the target
        direction = directionToTarget;

        // Move the enemy towards the player
        Position += directionToTarget * speed;

    }
}
