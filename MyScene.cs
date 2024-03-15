using System.Numerics;
using System.Runtime.CompilerServices;
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

            void updateBullets()
            {
                foreach (var bullet in bullets)
                {
                    bullet.Update();
                    bullet.Draw();
                }
            }

            void updatePlayer()
            {
                playerShip.Update();
                playerShip.Draw();
            }


            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                updateBullets();
                updatePlayer();

                Raylib.EndDrawing();
            }

            playerShip.Destroy();
            foreach (var bullet in bullets)
            {
                bullet.Destroy();
            }
            Raylib.CloseWindow();
        }
    }
}
