using Beecon.Input;

namespace Beecon.Scenes;

public abstract class BaseScene : GameSystem
{
    private PhysicsSystem PhysicsSystem => field ??= Scene.System<PhysicsSystem>();

    public bool IsDebugEnabled
    {
        get;
        set => PhysicsSystem.IsDebugDrawEnabled = value;
    } = false;

    public override void Update()
    {
#if DEBUG
        if (Inputs.DebugButton.IsPressed)
            IsDebugEnabled = !IsDebugEnabled;
#endif
    }
}
