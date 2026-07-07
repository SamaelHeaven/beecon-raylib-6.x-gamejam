using Beecon.Prefabs;

namespace Beecon.Scenes;

public sealed class GameScene : BaseScene
{
    private Entity _player;

    public override void Initialize()
    {
        // Background
        Scene.Entity().SetZIndex(-1).Set(new Grid(64, Color.Gray) { Scale = 10_000, Thick = 2 });

        // Player
        _player = Scene.Entity();
        new PlayerPrefab().Build(_player);
    }

    public override void PreRender()
    {
        Scene.Camera.Target = _player.RenderPosition;
        Scene.Camera.Offset = Display.Size / 2;
    }
}
