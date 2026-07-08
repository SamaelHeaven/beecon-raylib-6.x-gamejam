using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class BeeSpawnSystem : GameSystem
{
    private readonly Timer _spawnTimer = new(Gameplay.Bee.SpawnInterval);

    public override void Update()
    {
        if (!_spawnTimer.Update())
            return;
        var player = Scene.Player;
        if (player.IsNull)
            return;
        if (Scene.Count<Bee>() >= player.Get<Player>().MaxBees)
            return;
        var playerPosition = player.Position;
        var filter = new ShapeFilter { Category = ShapeCategory.Bee };
        if (
            Scene.TryFindSpawnPosition(
                () => playerPosition + RandomInCircle(Gameplay.Bee.SpawnRadius),
                Gameplay.Bee.SpawnClearanceRadius,
                filter,
                out var position
            )
        )
            new BeePrefab().Build(Scene.Entity().SetPosition(position));
    }

    private static Vector2 RandomInCircle(float radius)
    {
        var rnd = Random.Shared;
        var angle = rnd.NextSingle() * MathF.Tau;
        var distance = MathF.Sqrt(rnd.NextSingle()) * radius;
        return new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * distance;
    }
}
