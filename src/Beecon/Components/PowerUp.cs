namespace Beecon.Components;

public sealed class PowerUp(PowerUpType type)
{
    public PowerUpType Type { get; } = type;
}
