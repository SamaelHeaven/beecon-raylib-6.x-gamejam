using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Systems;

public sealed class PlayerMovementSystem : GameSystem
{
    private Vector2 _movement;

    public override void Update()
    {
        _movement.X = Inputs.HorizontalAxis.Value;
        _movement.Y = Inputs.VerticalAxis.Value;
        _movement = _movement.Normalize();
    }

    public override void FixedUpdate()
    {
        foreach (var (_, player, body) in Entries<Player, Body>())
            body.Seek(_movement * player.Stats.PlayerSpeed, Gameplay.Player.Acceleration);
    }
}
