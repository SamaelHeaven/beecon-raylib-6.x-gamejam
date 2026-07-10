namespace Beecon.Components;

public readonly struct PowerUp(PowerUpType type)
{
    public PowerUpType Type { get; } = type;
}
