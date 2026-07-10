namespace Beecon.Components;

public readonly struct Turret
{
    public Turret() { }

    public Timer FireTimer { get; } = new(Gameplay.Turret.FireInterval);
}
