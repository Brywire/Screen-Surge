using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Player : Entity
{
    public Player(Vector2 direction) : base(direction)
    {
        // Loading the player sprite
        texture = Raylib.LoadTexture("resources/playerShipSprite.png");
        this.position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);

        // Setting speed and scaleFactor
        speed = 6.0f;
        scaleFactor = 1.5f;
    }

    public override void Update()
    {
        // Cursor position
        direction = Raylib.GetMousePosition();

        Move();
        Shoot();
    }

    // Rotate sprite to cursor
    public override float Angle
    {
        get
        {
            return MathF.Atan2(direction.Y - position.Y, direction.X - position.X) * (180.0f / MathF.PI);

        }
    }
    public void Move()
    {
        Vector2 movement = new Vector2(0, 0);

        if (Raylib.IsKeyDown(KeyboardKey.W)) movement.Y -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.S)) movement.Y += 1;
        if (Raylib.IsKeyDown(KeyboardKey.A)) movement.X -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.D)) movement.X += 1;

        if (movement != Vector2.Zero)
        {
            movement = Vector2.Normalize(movement);
        }

        movement *= speed;

        position += movement;
    }
    public void Shoot()
    {
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            // Set bullet direction
            Vector2 bulletDirection = Vector2.Normalize(new Vector2(direction.X - position.X, direction.Y - position.Y));

            // Spawn bullet, initial position, give direction
            MyScene.bullets.Add(new Bullet(position, bulletDirection, "resources/bulletSprite.png"));
        }
    }
}
