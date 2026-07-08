namespace Beecon.Components;

public sealed class Virus
{
    private static readonly VirusType Last = Enum.GetValues<VirusType>()[^1];

    public VirusType Type { get; set; }

    public int MergeCount { get; set; }

    public bool CanMerge => Type != Last;
}
