namespace Beecon.Physics;

public static class BodyMovementExtensions
{
    extension(Body body)
    {
        public void Seek(Vector2 desiredVelocity, float acceleration)
        {
            var velocityChange = desiredVelocity - body.LinearVelocity;
            body.ApplyForce(velocityChange * acceleration);
        }

        public void Arrive(Vector2 target, float maxSpeed, float acceleration, float arrivalRadius)
        {
            var offset = target - body.Position;
            var distance = offset.Length();
            var speedScale = MathF.Min(distance / arrivalRadius, 1f);
            var desiredVelocity =
                distance > 0.01f ? offset / distance * maxSpeed * speedScale : Vector2.Zero;
            body.Seek(desiredVelocity, acceleration);
        }
    }
}
