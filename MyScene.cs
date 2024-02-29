using System.Numerics;
using Raylib_cs;

namespace ScreenSurge
{
    class MyScene
    {
        public static void Main()
        {
            const int screenWidth = 500;
            const int screenHeight = 500;

            //Making the window
            Raylib.InitWindow(screenWidth, screenHeight, "Screen Surge");

            //Loading in the player ship sprite
            Image playership = Raylib.LoadImage("resources/player_ship.png");
            Texture2D texture = Raylib.LoadTextureFromImage(playership);
            Raylib.UnloadImage(playership);

            //Setting initial position
            Vector2 playershipPosition = new Vector2(screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2);

            float speed = 5.0f;

            float scaleFactor = 1.5f;

            //List with bullets
            List<Bullet> bullets = new List<Bullet>();

            //Loading in the texture for the bullet
            Texture2D bulletTexture = Raylib.LoadTexture("resources/bullet.png");


            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                //Cursor position
                Vector2 cursorPosition = Raylib.GetMousePosition();

                //Movement
                if (Raylib.IsKeyDown(KeyboardKey.W)) playershipPosition.Y -= speed;
                if (Raylib.IsKeyDown(KeyboardKey.S)) playershipPosition.Y += speed;
                if (Raylib.IsKeyDown(KeyboardKey.A)) playershipPosition.X -= speed;
                if (Raylib.IsKeyDown(KeyboardKey.D)) playershipPosition.X += speed;

                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    //Set direction
                    Vector2 bulletDirection = Vector2.Normalize(new Vector2(cursorPosition.X - playershipPosition.X, cursorPosition.Y - playershipPosition.Y));

                    //Spawn bullet, initial position, give direction
                    bullets.Add(new Bullet(playershipPosition, bulletDirection, "resources/bullet.png"));
                }

                //Update bullets
                foreach (var bullet in bullets)
                {
                    bullet.Update();
                    bullet.Draw();
                }



                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                //Calculate ship angle to cursor
                float angle = MathF.Atan2(cursorPosition.Y - playershipPosition.Y, cursorPosition.X - playershipPosition.X) * (180.0f / MathF.PI);

                //Turn the ship 90 degrees to properly look at the cursor
                angle += 90.0f;

                // Calculate the scaled width and height
                int scaledWidth = (int)(texture.Width * scaleFactor);
                int scaledHeight = (int)(texture.Height * scaleFactor);

                // Draw the ship at its current position rotated by the angle
                Raylib.DrawTexturePro(texture, new Rectangle(0, 0, texture.Width, texture.Height),
                    new Rectangle((int)playershipPosition.X, (int)playershipPosition.Y, scaledWidth, scaledHeight),
                    new Vector2(scaledWidth / 2, scaledHeight / 2), angle, Color.White);


                Raylib.EndDrawing();
            }

            Raylib.UnloadTexture(texture);
            Raylib.UnloadTexture(bulletTexture);
            Raylib.CloseWindow();
        }
    }
}