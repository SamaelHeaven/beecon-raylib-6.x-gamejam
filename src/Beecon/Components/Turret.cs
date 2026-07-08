namespace Beecon.Components;

public sealed class Turret
{
    public Timer FireTimer { get; } = new(Gameplay.Turret.FireInterval);
}
