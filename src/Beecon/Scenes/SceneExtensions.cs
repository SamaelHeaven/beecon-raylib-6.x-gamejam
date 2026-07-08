using System.Diagnostics.CodeAnalysis;

namespace Beecon.Scenes;

public static class SceneExtensions
{
    extension(Scene scene)
    {
        public bool TryGetSingleton<T>([MaybeNullWhen(false)] out T singleton)
        {
            if (scene.Count<T>() == 0)
            {
                singleton = default!;
                return false;
            }

            singleton = scene.Components<T>().AsValueEnumerable().First();
            return true;
        }
    }
}
