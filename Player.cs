using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Player : Entity
{
    public Player(Vector2 direction) : base(direction)
    {
        //Loading the player sprite
        texture = Raylib.LoadTexture("resources/playerShipSprite.png");
        this.position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);

        speed = 5.0f;

        scaleFactor = 1.5f;
    }
        public override float Angle
    {
        get
        {
            return MathF.Atan2(direction.Y - position.Y, direction.X - position.X) * (180.0f / MathF.PI);

        }
    }

    public override void Update()
    {
        //Movement
        if (Raylib.IsKeyDown(KeyboardKey.W)) position.Y -= speed;
        if (Raylib.IsKeyDown(KeyboardKey.S)) position.Y += speed;
        if (Raylib.IsKeyDown(KeyboardKey.A)) position.X -= speed;
        if (Raylib.IsKeyDown(KeyboardKey.D)) position.X += speed;

        //Cursor position
        direction = Raylib.GetMousePosition();

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            //Set direction
            Vector2 bulletDirection = Vector2.Normalize(new Vector2(direction.X - position.X, direction.Y - position.Y));

            //Spawn bullet, initial position, give direction
            MyScene.bullets.Add(new Bullet(position, bulletDirection, "resources/bulletSprite.png"));
        }


    }


}