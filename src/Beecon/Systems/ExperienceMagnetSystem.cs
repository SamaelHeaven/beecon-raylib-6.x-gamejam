using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class ExperienceMagnetSystem : GameSystem
{
    public override void FixedUpdate()
    {
        var player = Scene.Player;
        if (player.IsNull)
            return;
        var state = player.Get<Player>();
        if (state.MagnetActive)
            state.MagnetRemaining -= Time.FixedDelta;
        var active = state.MagnetActive;
        var playerPosition = player.Position;
        var radius = Gameplay.Experience.MagnetRadius;
        foreach (var (_, _, body) in Entries<Experience, Body>())
        {
            var offset = playerPosition - body.Position;
            var distance = offset.Length();
            if (distance <= 0.01f)
                continue;
            if (!active && distance > radius)
                continue;
            float speed;
            if (active)
            {
                speed = Gameplay.Experience.MagnetMaxSpeed;
            }
            else
            {
                var closeness = 1f - distance / radius;
                speed =
                    Gameplay.Experience.MagnetMinSpeed
                    + (Gameplay.Experience.MagnetMaxSpeed - Gameplay.Experience.MagnetMinSpeed)
                        * closeness;
            }

            var step = MathF.Min(speed * Time.FixedDeltaSeconds, distance);
            body.Position += offset / distance * step;
        }
    }
}
