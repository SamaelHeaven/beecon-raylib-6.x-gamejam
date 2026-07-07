using Beecon.Components;
using Beecon.Input;

namespace Beecon.Systems;

public sealed class PlayerMovementSystem : GameSystem
{
    private const float Acceleration = 100f;

    private Vector2 _movement;

    public override void Update()
    {
        _movement.X = Inputs.HorizontalAxis.Value;
        _movement.Y = Inputs.VerticalAxis.Value;
        _movement = _movement.Normalize();
    }

    public override void FixedUpdate()
    {
        var force = _movement * Acceleration;
        foreach (var (entity, player, body) in Entries<Player, Body>())
            body.ApplyForce(force);
    }
}
