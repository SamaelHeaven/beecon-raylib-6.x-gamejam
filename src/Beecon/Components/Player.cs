namespace Beecon.Components;

public struct Player { }

public static class ScenePlayerExtensions
{
    extension(Scene scene)
    {
        public Entity PlayerEntity => scene.Entities<Player>().First();
    }
}
