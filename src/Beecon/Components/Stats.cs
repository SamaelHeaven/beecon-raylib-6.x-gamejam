namespace Beecon.Components;

public sealed class Stats
{
    private readonly int[] _levels = new int[StatType.All.Length];

    public int AvailablePoints { get; private set; }

    public float PlayerSpeed => Gameplay.Stats.PlayerSpeed(LevelOf(StatType.Speed));
    public float BeeSpeed => Gameplay.Stats.BeeSpeed(LevelOf(StatType.BeeSpeed));
    public float BeeDamage => Gameplay.Stats.BeeDamage(LevelOf(StatType.BeeDamage));
    public TimeSpan BeeReload => Gameplay.Stats.BeeReload(LevelOf(StatType.BeeReload));
    public float MaxHealth => Gameplay.Stats.PlayerHealth(LevelOf(StatType.Health));
    public float HealthRegen => Gameplay.Stats.HealthRegen(LevelOf(StatType.HealthRegen));

    public int LevelOf(StatType type)
    {
        return _levels[(int)type];
    }

    public bool IsMaxed(StatType type)
    {
        return LevelOf(type) >= Gameplay.Stats.MaxLevel;
    }

    public bool CanAllocate(StatType type)
    {
        return AvailablePoints > 0 && !IsMaxed(type);
    }

    public void GrantPoint()
    {
        AvailablePoints++;
    }

    public bool Allocate(StatType type)
    {
        if (!CanAllocate(type))
            return false;
        _levels[(int)type]++;
        AvailablePoints--;
        return true;
    }
}
