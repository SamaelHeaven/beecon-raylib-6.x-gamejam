using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.Systems;

public sealed class PowerUpSystem : GameSystem
{
    private readonly Sound _health = Sound.Resource("Audio.health.wav");
    private readonly Sound _magnet = Sound.Resource("Audio.magnet.wav");
    private readonly Sound _nuke = Sound.Resource("Audio.nuke.wav");

    public override void WorldSensorBegin(Shape sensor, Shape visitor)
    {
        if (!sensor.Entity.TryGet(out PowerUp powerUp))
            return;
        if (!visitor.Entity.TryGet(out Player player))
            return;
        Apply(powerUp.Type, visitor.Entity, player);
        sensor.Entity.Destroy();
    }

    private void Apply(PowerUpType type, Entity playerEntity, Player player)
    {
        switch (type)
        {
            case PowerUpType.Health:
                if (playerEntity.TryGet(out Health health))
                    health.Restore();
                _health.Play();
                break;
            case PowerUpType.Magnet:
                player.ActivateMagnet();
                _magnet.Play();
                break;
            case PowerUpType.Nuke:
                foreach (var (_, _, virusHealth) in Entries<Virus, Health>())
                    virusHealth.Damage(Gameplay.PowerUp.NukeDamage);
                Scene.Announce("BOOM!");
                _nuke.Play();
                break;
        }
    }
}
