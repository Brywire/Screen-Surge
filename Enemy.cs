using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Enemy : Entity
{
public Enemy() : base(new Vector2(0, 0)) // Assuming no initial direction for now
    {
        texture = Raylib.LoadTexture("resources/enemySprite.png");
        speed = 5.0f; // Example speed
        scaleFactor = 1.0f; // Example scale factor
        position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);
    }

    public override void Update()
    {
        // Example movement logic
        //position.X += speed;
    }
}
