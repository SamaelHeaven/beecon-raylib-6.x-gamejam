using Beecon.Input;

namespace Beecon.Scenes;

public abstract class BaseScene : GameSystem
{
    private PhysicsSystem PhysicsSystem => field ??= Scene.System<PhysicsSystem>();

    public bool IsDebugEnabled
    {
        get;
        set
        {
            field = value;
            PhysicsSystem.IsDebugDrawEnabled = field;
        }
    } = false;

    public override void Update()
    {
#if DEBUG
        if (Inputs.DebugButton.IsPressed)
            IsDebugEnabled = !IsDebugEnabled;
        if (Inputs.RestartButton.IsPressed)
            Game.Scene = new Scene(Scene.SystemsFunc);
#endif
    }

    public override void PostRender()
    {
        if (IsDebugEnabled)
            Renderer.Graphics.FillText($"FPS: {Time.AverageFps}", 0, 0);
    }
}
