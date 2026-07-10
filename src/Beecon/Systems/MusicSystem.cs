namespace Beecon.Systems;

public sealed class MusicSystem : GameSystem
{
    private readonly Music _music = Music.Resource("Audio.game-music.ogg");

    public override void Start()
    {
        _music.Play();
    }

    public override void Stop()
    {
        _music.Stop();
    }
}
