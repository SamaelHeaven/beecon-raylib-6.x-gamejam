namespace Beecon.Components;

public struct Player { }

public static class ScenePlayerExtensions
{
    extension(Scene scene)
    {
        public Entity Player => scene.Entities<Player>().AsValueEnumerable().First();
    }
}
