using Beecon.Components;
using Beecon.Physics;
using Beecon.Prefabs;

namespace Beecon.Systems;

public sealed class BeaconSystem : GameSystem
{
    public override void Initialize()
    {
        var bound = Gameplay.Map.Size / 2f - Gameplay.Beacon.Radius - Gameplay.Map.WallThickness;
        var filter = new ShapeFilter { Category = ShapeCategory.Beacon };
        var candidate = () => RandomInBounds(bound);
        for (var i = 0; i < Gameplay.Beacon.Count; i++)
            if (
                Scene.TryFindSpawnPosition(
                    candidate,
                    Gameplay.Beacon.Radius * 2f,
                    filter,
                    out var position,
                    Gameplay.Beacon.SpawnMaxAttempts
                )
            )
                new BeaconPrefab().Build(Scene.Entity().SetPosition(position));
    }

    public override void Update()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        foreach (var (entity, beacon, body) in Entries<Beacon, Body>())
        {
            if (beacon.Activated || !IsInside(body, playerPosition))
                continue;
            var completed = beacon.ChargeTimer.Update();
            entity.Get<RegularPolygon>().Fill = Color.Lerp(
                BeaconPrefab.DeactivatedColor,
                BeaconPrefab.ActivatedColor,
                beacon.Progress
            );

            if (!completed)
                continue;
            beacon.Activated = true;
            player.Get<Player>().MaxBees += Gameplay.Beacon.MaxBeesBonus;
        }
    }

    private static bool IsInside(Body body, Vector2 point)
    {
        return body.Shapes.AsValueEnumerable().Any(shape => shape.TestPoint(point));
    }

    private static Vector2 RandomInBounds(Vector2 bound)
    {
        var rnd = Random.Shared;
        return new Vector2(
            (rnd.NextSingle() * 2f - 1f) * bound.X,
            (rnd.NextSingle() * 2f - 1f) * bound.Y
        );
    }
}
