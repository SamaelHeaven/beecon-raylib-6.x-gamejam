namespace Beecon.Components;

public sealed class Beacon
{
    public Timer ChargeTimer { get; } = new(Gameplay.Beacon.ChargeDuration, cycleCount: 1);
    public bool Activated { get; set; }
    public float Progress => ChargeTimer.Progress;
}
