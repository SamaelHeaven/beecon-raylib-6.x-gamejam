using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class VirusSpawnSystem : GameSystem
{
    private readonly Timer _spawnTimer = new(Gameplay.Virus.SpawnInterval);

    public override void Update()
    {
        if (!_spawnTimer.Update())
            return;
        var camera = Scene.Camera;
        var half = Display.Size / 2f / camera.Zoom;
        var extentX = half.X + Gameplay.Virus.SpawnMargin;
        var extentY = half.Y + Gameplay.Virus.SpawnMargin;
        var filter = new ShapeFilter { Category = ShapeCategory.Virus };
        var candidate = () => camera.Target + EdgePoint(extentX, extentY);
        for (var spawned = 0; spawned < Gameplay.Virus.SpawnCount; spawned++)
        {
            if (Scene.Count<Virus>() >= Gameplay.Virus.MaxCount)
                return;
            if (
                Scene.TryFindSpawnPosition(
                    candidate,
                    Gameplay.Virus.SpawnClearanceRadius,
                    filter,
                    out var position
                )
            )
                new VirusPrefab().Build(Scene.Entity().SetPosition(position));
        }
    }

    private static Vector2 EdgePoint(float extentX, float extentY)
    {
        var rnd = Random.Shared;
        var x = (rnd.NextSingle() * 2f - 1f) * extentX;
        var y = (rnd.NextSingle() * 2f - 1f) * extentY;
        return rnd.Next(4) switch
        {
            0 => new Vector2(x, -extentY),
            1 => new Vector2(x, extentY),
            2 => new Vector2(-extentX, y),
            _ => new Vector2(extentX, y),
        };
    }
}
