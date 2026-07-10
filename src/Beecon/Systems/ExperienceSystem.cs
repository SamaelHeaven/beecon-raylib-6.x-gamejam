using Beecon.Components;

namespace Beecon.Systems;

public sealed class ExperienceSystem : GameSystem
{
    private readonly Sound _experience = Sound
        .Resource("Audio.experience.wav")
        .SetVolume(Gameplay.Audio.ExperienceVolume);

    private readonly Sound _levelUp = Sound.Resource("Audio.level-up.wav");

    public override void WorldSensorBegin(Shape sensor, Shape visitor)
    {
        if (!sensor.Entity.TryGet(out Experience experience))
            return;
        if (!visitor.Entity.TryGet(out Player player))
            return;
        var level = player.Level;
        player.AddExperience(experience.Amount);
        sensor.Entity.Destroy();

        var pitch =
            1f + (Random.Shared.NextSingle() * 2f - 1f) * Gameplay.Audio.ExperiencePitchVariation;
        _experience.SetPitch(pitch).Play();
        if (player.Level > level)
            _levelUp.Play();
    }
}
