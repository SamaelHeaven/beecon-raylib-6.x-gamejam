namespace Beecon.Components;

public sealed class Player
{
    public int MaxBees { get; set; } = Gameplay.Player.InitialMaxBees;
}

public static class ScenePlayerExtensions
{
    extension(Scene scene)
    {
        public Entity Player => scene.Entities<Player>().AsValueEnumerable().FirstOrDefault();
    }
}
