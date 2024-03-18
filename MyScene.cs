using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace ScreenSurge
{

    class MyScene
    {
        public const int screenWidth = 500;
        public const int screenHeight = 500;
        static public List<Bullet> bullets = new List<Bullet>();
        static public List<Enemy> enemies = new List<Enemy>();
        public static void Main()
        {

            //Making the window
            Raylib.InitWindow(screenWidth, screenHeight, "Screen Surge");

            Player playerShip = new Player(Raylib.GetMousePosition());
            enemies.Add(new Enemy());
            double lastEnemySpawnTime = Raylib.GetTime();

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

            void updateEnemy()
            {
                foreach (var enemy in enemies)
                {
                    enemy.Update();
                    enemy.Draw();
                }
            }

            void enemySpawner()
            {
                // Check if 5 seconds have passed since the last enemy spawn
                if (Raylib.GetTime() - lastEnemySpawnTime >= 5.0)
                {
                    enemies.Add(new Enemy()); // Create a new enemy
                    lastEnemySpawnTime = Raylib.GetTime(); // Reset the timer
                }
            }

            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);

                updateBullets();
                updatePlayer();
                updateEnemy();
                //enemySpawner();
                DrawCircle(Raylib.GetMouseX(), Raylib.GetMouseY(), 5.0f, Color.White);

                Raylib.EndDrawing();
            }

            playerShip.Destroy();
            foreach (var bullet in bullets)
            {
                bullet.Destroy();
            }
            foreach (var enemy in enemies)
            {
                enemy.Destroy();
            }
            Raylib.CloseWindow();
        }
    }
}
