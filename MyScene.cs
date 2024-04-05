using System.Numerics;
using Raylib_cs;

/*
    TODO:
    - Enemy behaviours
    - 
    - 

    BUGS:
    - Enemy sprite unloading
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
                // Timer between enemy spawning
                if (Raylib.GetTime() - lastEnemySpawnTime >= 0.5)
                {
                    // Randomly select an edge
                    int edge = Raylib.GetRandomValue(0, 3); // 0 = top, 1 = right, 2 = bottom, 3 = left

                    // Randomly select a position on the edge
                    Vector2 spawnPosition;
                    switch (edge)
                    {
                        case 0:
                            // Top edge
                            spawnPosition = new Vector2(Raylib.GetRandomValue(0, screenWidth), 0);
                            break;
                        case 1:
                            // Right edge
                            spawnPosition = new Vector2(screenWidth, Raylib.GetRandomValue(0, screenHeight));
                            break;
                        case 2:
                            // Bottom edge
                            spawnPosition = new Vector2(Raylib.GetRandomValue(0, screenWidth), screenHeight);
                            break;
                        case 3:
                            // Left edge
                            spawnPosition = new Vector2(0, Raylib.GetRandomValue(0, screenHeight));
                            break;
                        default:
                            spawnPosition = new Vector2(screenHeight / 2, screenWidth / 2); // For if something goes wrong
                            break;
                    }

                    // Create a new enemy at the new position
                    enemies.Add(new Enemy(spawnPosition));

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
                        if (Raylib.CheckCollisionCircles(bullet.GetCenter(), bullet.GetRadius(), enemy.GetCenter(), enemy.GetRadius()))
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
                }

            }
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
                enemySpawner();
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
