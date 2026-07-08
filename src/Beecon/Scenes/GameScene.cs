using Beecon.Prefabs;

namespace Beecon.Scenes;

public sealed class GameScene : BaseScene
{
    public override void Initialize()
    {
        var mapSize = Gameplay.Map.Size;
        var halfMapSize = mapSize / 2f;
        var thickness = Gameplay.Map.WallThickness;

        // Background
        Scene.Entity().SetZIndex(-1).Set(new Grid(64, Color.Gray) { Scale = mapSize, Thick = 2 });

        // Player
        new PlayerPrefab().Build(Scene.Entity());

        // Edges
        new WallPrefab(new Vector2(mapSize.X, thickness)).Build(
            Scene.Entity().SetPosition(new Vector2(0, -halfMapSize.Y))
        );
        new WallPrefab(new Vector2(mapSize.X, thickness)).Build(
            Scene.Entity().SetPosition(new Vector2(0, halfMapSize.Y))
        );
        new WallPrefab(new Vector2(thickness, mapSize.Y)).Build(
            Scene.Entity().SetPosition(new Vector2(-halfMapSize.X, 0))
        );
        new WallPrefab(new Vector2(thickness, mapSize.Y)).Build(
            Scene.Entity().SetPosition(new Vector2(halfMapSize.X, 0))
        );
    }
}
