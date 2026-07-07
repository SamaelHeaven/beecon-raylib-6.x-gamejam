using Beecon.Components;
using Beecon.Input;

namespace Beecon.Systems;

public sealed class PlayerMovementSystem : GameSystem
{
    private const float MaxSpeed = 200f;
    private const float Acceleration = 5f;

    private Vector2 _movement;

    public override void Update()
    {
        _movement.X = Inputs.HorizontalAxis.Value;
        _movement.Y = Inputs.VerticalAxis.Value;
        _movement = _movement.Normalize();
    }

    public override void FixedUpdate()
    {
        foreach (var (entity, player, body) in Entries<Player, Body>())
        {
            var desiredVelocity = _movement * MaxSpeed;
            var currentVelocity = body.LinearVelocity;
            var velocityChange = desiredVelocity - currentVelocity;
            var force = velocityChange * Acceleration;
            body.ApplyForce(force);
        }
    }
}
