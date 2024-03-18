using System.Numerics;
using Raylib_cs;
using ScreenSurge;

class Enemy : Entity
{
public Enemy() : base(new Vector2(0, 0))
    {
        texture = Raylib.LoadTexture("resources/enemySprite.png");
        speed = 5.0f;
        scaleFactor = 1.0f;
        position = new Vector2(MyScene.screenWidth / 2 - texture.Width / 2, MyScene.screenHeight / 2 - texture.Height / 2);
    }

    public override void Update()
    {
        //position.X += speed;
    }
}
