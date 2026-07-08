namespace Beecon.Physics;

public static class SpawnExtensions
{
    private static bool _blocked = false;

    extension(Scene scene)
    {
        public bool TryFindSpawnPosition(
            Func<Vector2> candidate,
            float clearanceRadius,
            in ShapeFilter filter,
            out Vector2 position,
            int maxAttempts = 16
        )
        {
            var world = scene.World;
            for (var attempt = 0; attempt < maxAttempts; attempt++)
            {
                position = candidate.Invoke();
                _blocked = false;
                world.Overlap(
                    CircleShape.Make(position, clearanceRadius),
                    static _ =>
                    {
                        _blocked = true;
                        return false;
                    },
                    filter
                );
                if (!_blocked)
                    return true;
            }

            position = default;
            return false;
        }
    }
}
