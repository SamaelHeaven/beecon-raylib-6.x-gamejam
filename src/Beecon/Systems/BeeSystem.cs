using Beecon.Components;

namespace Beecon.Systems;

public sealed class BeeSystem : GameSystem
{
    private static float Acceleration => 5f;
    private static float MaxSpeed => 300f;

    public override void FixedUpdate()
    {
        var mouseWorldPosition = Mouse.WorldPosition;
        foreach (var (_, body) in Components<Bee, Body>())
        {
            var direction = (mouseWorldPosition - body.Position).Normalize();
            var desiredVelocity = direction * MaxSpeed;
            var currentVelocity = body.LinearVelocity;
            var velocityChange = desiredVelocity - currentVelocity;
            var force = velocityChange * Acceleration;
            body.ApplyForce(force);
        }
    }
}
