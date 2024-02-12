using Raylib_cs;

namespace HelloWorld;

class MyScene
{
    public static void Main()
    {
        const int screenWidth = 400;
        const int screenHeight = 400;

        Raylib.InitWindow(screenWidth, screenHeight, "Screen Surge");

        Image playership = Raylib.LoadImage("resources/player_ship.png");
        Texture2D texture = Raylib.LoadTextureFromImage(playership);
        Raylib.UnloadImage(playership);

        Raylib.SetTargetFPS(60);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.RayWhite);

            Raylib.DrawTexture(texture, screenWidth / 2 - texture.Width / 2, screenHeight / 2 - texture.Height / 2, Color.White);

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(texture);

        Raylib.CloseWindow();
    }
}