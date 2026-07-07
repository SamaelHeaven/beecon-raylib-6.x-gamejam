using Beecon.Components;

namespace Beecon.Systems;

public sealed class CameraSystem : GameSystem
{
    public override void PreRender()
    {
        Scene.Camera.Target = Scene.PlayerEntity.RenderPosition;
        Scene.Camera.Offset = Display.Size / 2;
    }
}
