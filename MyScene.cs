using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

namespace ScreenSurge
{

    class MyScene
    {
        public const int screenWidth = 500;
        public const int screenHeight = 500;
        static public List<Bullet> bullets = new List<Bullet>();
        public static void Main()
        {

            //Making the window
            Raylib.InitWindow(screenWidth, screenHeight, "Screen Surge");

            Player playerShip = new Player(Raylib.GetMousePosition());

            //Loading in the texture for the bullet
            Texture2D bulletTexture = Raylib.LoadTexture("resources/bulletSprite.png");


            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                //Update bullets
                foreach (var bullet in bullets)
                {
                    bullet.Update();
                    bullet.Draw();
                }

                playerShip.Update();
                playerShip.Draw();

                Raylib.EndDrawing();
            }

            Raylib.UnloadTexture(bulletTexture);
            playerShip.Destroy();
            Raylib.CloseWindow();
        }
    }
}