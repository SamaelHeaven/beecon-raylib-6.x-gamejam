using Beecon.Components;

namespace Beecon.Systems;

public sealed class DamageSystem : GameSystem
{
    private readonly Sound _playerHit = Sound.Resource("Audio.player-hit.wav");
    private readonly EntitySparseSet<EntitySparseSet<DamageContact>> _targetsByDamager = new();
    private readonly Sound _virusHit = Sound.Resource("Audio.virus-hit.wav");
    private TimeSpan _playerHitLast;
    private TimeSpan _virusHitLast;

    public override void WorldSensorBegin(Shape sensor, Shape visitor)
    {
        var target = visitor.Entity;
        if (!sensor.Entity.TryGet(out Damage damage))
            return;
        if ((visitor.Filter.Category & damage.TargetMask) == 0)
            return;
        if (!target.TryGet(out Health health))
            return;
        health.Damage(damage.Amount);
        Hit(target);
        if (!_targetsByDamager.TryGetValue(sensor.Entity, out var targets))
            _targetsByDamager[sensor.Entity] = targets = new EntitySparseSet<DamageContact>();
        targets[target] = new DamageContact(damage.Amount, damage.Cooldown);
    }

    public override void WorldSensorEnd(Shape sensor, Shape visitor)
    {
        if (!_targetsByDamager.TryGetValue(sensor.Entity, out var targets))
            return;
        targets.Remove(visitor.Entity);
        if (targets.Count == 0)
            _targetsByDamager.Remove(sensor.Entity);
    }

    public override void FixedUpdate()
    {
        for (var i = _targetsByDamager.Count - 1; i >= 0; i--)
        {
            var (damager, targets) = _targetsByDamager[i];
            if (!damager.IsValid)
            {
                _targetsByDamager.Remove(damager);
                continue;
            }

            for (var j = targets.Count - 1; j >= 0; j--)
            {
                var (target, contact) = targets[j];
                if (!target.IsValid)
                {
                    targets.Remove(target);
                    continue;
                }

                contact.Elapsed += Time.FixedDelta;
                if (contact.Elapsed >= contact.Cooldown)
                {
                    contact.Elapsed = TimeSpan.Zero;
                    if (target.TryGet(out Health health))
                    {
                        health.Damage(contact.Amount);
                        Hit(target);
                    }
                }

                targets[target] = contact;
            }

            if (targets.Count == 0)
                _targetsByDamager.Remove(damager);
        }
    }

    private void Hit(Entity target)
    {
        if (target.TryGet(out Player _))
            TryPlay(_playerHit, ref _playerHitLast);
        else if (target.TryGet(out Virus _))
            TryPlay(_virusHit, ref _virusHitLast);
    }

    private static void TryPlay(Sound sound, ref TimeSpan last)
    {
        var now = Time.Elapsed;
        if (now - last < Gameplay.Audio.HitInterval)
            return;
        last = now;
        var pitch = 1f + (Random.Shared.NextSingle() * 2f - 1f) * Gameplay.Audio.HitPitchVariation;
        sound.SetPitch(pitch).Play();
    }

    private record struct DamageContact(
        float Amount,
        TimeSpan Cooldown,
        TimeSpan Elapsed = default
    );
}
