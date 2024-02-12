using System.Numerics;
using Raylib_cs;

namespace HelloWorld
{
    class MyScene
    {
        public static void Main()
        {
            const int screenWidth =  800;
            const int screenHeight =  800;

            Raylib.InitWindow(screenWidth, screenHeight, "Screen Surge");

            Image playership = Raylib.LoadImage("resources/player_ship.png");
            Texture2D texture = Raylib.LoadTextureFromImage(playership);
            Raylib.UnloadImage(playership);

            Vector2 playershipPosition = new Vector2(screenWidth /  2 - texture.Width /  2, screenHeight /  2 - texture.Height /  2);
            float speed =  5.0f;

            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                // Update
                if (Raylib.IsKeyDown(KeyboardKey.W)) playershipPosition.Y -= speed;
                if (Raylib.IsKeyDown(KeyboardKey.S)) playershipPosition.Y += speed;
                if (Raylib.IsKeyDown(KeyboardKey.A)) playershipPosition.X -= speed;
                if (Raylib.IsKeyDown(KeyboardKey.D)) playershipPosition.X += speed;

                // Draw
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.RayWhite);

                // Get the cursor's position
                Vector2 cursorPosition = Raylib.GetMousePosition();
                // Calculate the angle to the cursor
                float angle = MathF.Atan2(cursorPosition.Y - playershipPosition.Y, cursorPosition.X - playershipPosition.X) * (180.0f / MathF.PI);
                // Subtract  90 degrees from the angle to align the sprite correctly
                angle +=  90.0f;

                // Draw the playership at its current position rotated by the angle
                Raylib.DrawTexturePro(texture, new Rectangle(0,  0, texture.Width, texture.Height), new Rectangle((int)playershipPosition.X, (int)playershipPosition.Y, texture.Width, texture.Height), new Vector2(texture.Width /  2, texture.Height /  2), angle, Color.White);

                Raylib.EndDrawing();
            }

            Raylib.UnloadTexture(texture);
            Raylib.CloseWindow();
        }
    }
}
