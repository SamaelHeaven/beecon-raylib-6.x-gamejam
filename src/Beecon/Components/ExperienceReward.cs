namespace Beecon.Components;

public readonly struct ExperienceReward(float amount, ExperienceType type)
{
    public float Amount { get; } = amount;
    public ExperienceType Type { get; } = type;
}
