using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;
using static Raylib_cs.Raylib;

/*
    TODO:
    - Make the enemy spawn from the edge of the screen
    - Make the enemy spawn from different edges
    - Make the enemy spawn from random edges, max 2 in a row
    - Adjust window size in realtime

    BUGS:
    - All textures get unloaded when collision is detected
    - Collision area is not 100% accurate
*/
namespace ScreenSurge
{

    class MyScene
    {
        public const int screenWidth = 800;
        public const int screenHeight = 800;
        static public List<Bullet> bullets = new List<Bullet>();
        static public List<Enemy> enemies = new List<Enemy>();
        public static void Main()
        {

            //Making the window
            Raylib.InitWindow(screenWidth, screenHeight, "Screen Surge");

            Player playerShip = new Player(Raylib.GetMousePosition());
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
                // Check if 3 seconds have passed since the last enemy spawn
                if (Raylib.GetTime() - lastEnemySpawnTime >= 2.0)
                {
                    enemies.Add(new Enemy()); // Create a new enemy
                    lastEnemySpawnTime = Raylib.GetTime(); // Reset the timer
                }
            }

            void checkCollisions()
            {
                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    Bullet bullet = bullets[i];
                    for (int j = enemies.Count - 1; j >= 0; j--)
                    {
                        Enemy enemy = enemies[j];
                        if (Raylib.CheckCollisionRecs(bullet.getCollisionBox(), enemy.getCollisionBox()))
                        {
                            // Collision detected, destroy both the bullet and the enemy
                            bullet.Destroy();
                            bullets.RemoveAt(i);
                            enemy.Destroy();
                            enemies.RemoveAt(j);
                            break; // Break the inner loop as the bullet has been destroyed
                        }
                    }
                }
            }

            void enemiesLookForPlayer()
            {
                foreach (var enemy in enemies)
                {
                    enemy.targetPosition = playerShip.Position;
                    DrawRectangle((int)enemy.Position.X, (int)enemy.Position.Y, (int)enemy.Texture.Width, (int)enemy.Texture.Height, Color.Red);
                }

            }
            enemies.Add(new Enemy());
            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);
                
                // Methods that need to be updated
                updateBullets();
                updatePlayer();
                updateEnemy();
                checkCollisions();
                //enemySpawner();
                enemiesLookForPlayer();

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
