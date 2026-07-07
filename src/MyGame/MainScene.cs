namespace MyGame;

public sealed class MainScene : GameSystem
{
    public override void Update()
    {
        Renderer.Graphics.ClearBackground(Color.Blue);
        Renderer.Graphics.FillText("Hello, World!", 4, 4, Color.Black);
    }
}
