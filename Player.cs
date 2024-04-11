using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Player : Entity
{
    private bool isMouseButtonDown = false;
    private float lastShotTime = 0.0f;
    private float rateOfFire = 0.4f;

    public Player(Vector2 direction) : base(direction, "playerShipSprite.png")
    {
        // Load player texture using ResourceManager
        texture = ResourceManager.Instance.LoadTexture(textureName);

        // Starting position
        Position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);

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

        // Update mouse button state
        if (Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            isMouseButtonDown = true;
        }
        else if (Raylib.IsMouseButtonReleased(MouseButton.Left))
        {
            isMouseButtonDown = false;
        }
    }

    // Rotate sprite to cursor
    public override float Angle
    {
        get
        {
            Vector2 dist = new Vector2(direction.X - Position.X, direction.Y - Position.Y);
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

        Position += movement;
    }
    public void Shoot()
    {
        if (isMouseButtonDown && Raylib.GetTime() - lastShotTime >= rateOfFire) // Check since when the last shot was fired
        {
            // Calculate the direction from the player to the mouse cursor
            Vector2 bulletDirection = Vector2.Normalize(new Vector2(Raylib.GetMousePosition().X - Position.X, Raylib.GetMousePosition().Y - Position.Y));

            // Spawn bullet at the player's position, with the calculated direction
            MyScene.bullets.Add(new Bullet(bulletDirection, Position));

            lastShotTime = (float)Raylib.GetTime(); // Update the time of the last shot
        }
    }
}
