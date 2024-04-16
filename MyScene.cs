using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

/*
    TODO:
    - Expand Window when Bullet collides
    - Move Window when Window expands
    - Boids
    - 

    BUGS:
    - Enemy sprite unloading
    - Enemies overlapping (Boids)
    - 
*/
namespace ScreenSurge
{
    class MyScene
    {
        public const int screenWidth = 800; // Initial window width
        public const int screenHeight = 800; // Initial window height
        public const int minWindowWidth = 400; // Minimum window width
        public const int minWindowHeight = 400; // Minimum window height
        static public List<Bullet> bullets = new List<Bullet>();
        static public List<Enemy> enemies = new List<Enemy>();
        private static double lastResizeTime = 0.0;
        private static double resizeInterval = 0.01;

        private static IntPtr GetWindowHandle() // Retrieves handle to the main window of application
        {
            return Process.GetCurrentProcess().MainWindowHandle;
        }


        public static void Main()
        {

            //Making the window
            Raylib.InitWindow(screenWidth, screenHeight, "Screen Surge");

            IntPtr windowHandle = GetWindowHandle();

            // Initialize the ResourceManager
            ResourceManager.Initialize("resources");

            int shrinkAmount = 1;


            Player playerShip = new Player(Raylib.GetMousePosition());
            double lastEnemySpawnTime = Raylib.GetTime();

            void updateBullets()
            {
                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    Bullet bullet = bullets[i];
                    bullet.Update();
                    bullet.Draw();

                    // Check if the bullet has hit the window border
                    if (bullet.hasHitWindowBorder(screenWidth, screenHeight))
                    {
                        // Remove the bullet
                        bullets.RemoveAt(i);
                        bullet.Destroy(); // Destroy bullet texture
                    }
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
                if (Raylib.GetTime() - lastEnemySpawnTime >= 1)
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

            void bulletToEnemyCollision()
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
                            Console.WriteLine("Deleted Bullet");
                            enemy.Destroy();
                            enemies.RemoveAt(j);
                            Console.WriteLine("Deleted Enemy");
                            break; // Break the inner loop as the bullet has been destroyed
                        }
                    }
                }

            }

            void checkCollisions()
            {
                bulletToEnemyCollision();
            }

            void enemiesLookForPlayer()
            {
                foreach (var enemy in enemies)
                {
                    enemy.targetPosition = playerShip.Position;
                }

            }

            void updateWindowShrinking()
            {
                double currentTime = Raylib.GetTime();
                if (currentTime - lastResizeTime >= resizeInterval)
                {
                    // Get the current window size
                    WindowResizer.GetWindowRect(windowHandle, out WindowResizer.RECT rect);
                    int currentWidth = rect.Right - rect.Left;
                    int currentHeight = rect.Bottom - rect.Top;

                    // Check if the window is already at or below the minimum size
                    if (currentWidth > minWindowWidth && currentHeight > minWindowHeight)
                    {
                        WindowResizer.ShrinkWindowBy(windowHandle, shrinkAmount);
                        lastResizeTime = currentTime;
                    }
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
                //updateWindowShrinking();



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
            ResourceManager.Instance.UnloadAllTextures();
            Raylib.CloseWindow();
        }
    }
}
