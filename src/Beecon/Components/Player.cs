namespace Beecon.Components;

public sealed class Player { }

public static class ScenePlayerExtensions
{
    extension(Scene scene)
    {
        public Entity PlayerEntity => scene.Entities<Player>().First();
    }
}
