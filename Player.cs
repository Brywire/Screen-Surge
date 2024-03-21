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

    public Vector2 Position
    {
        get { return position; }
    }

    // Rotate sprite to cursor
    public override float Angle
    {
        get
        {
            Vector2 dist = new Vector2(direction.X - position.X, direction.Y - position.Y);
            float result = MathF.Atan2(dist.Y, dist.X) * (180.0f / MathF.PI);
            return result;
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
            // Calculate the direction from the player to the mouse cursor
            Vector2 bulletDirection = Vector2.Normalize(new Vector2(Raylib.GetMousePosition().X - position.X, Raylib.GetMousePosition().Y - position.Y));

            // Spawn bullet at the player's position, with the calculated direction
            MyScene.bullets.Add(new Bullet(bulletDirection, position));
        }
    }
}
