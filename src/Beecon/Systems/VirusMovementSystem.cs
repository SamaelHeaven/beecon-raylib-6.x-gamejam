using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Systems;

public sealed class VirusMovementSystem : GameSystem
{
    private static float Acceleration => 5f;
    private static float MaxSpeed => 75f;

    public override void FixedUpdate()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var playerPosition = player.Position;
        foreach (var (_, _, body) in Entries<Virus, Body>())
        {
            var direction = (playerPosition - body.Position).Normalize();
            body.Seek(direction * MaxSpeed, Acceleration);
        }
    }
}
