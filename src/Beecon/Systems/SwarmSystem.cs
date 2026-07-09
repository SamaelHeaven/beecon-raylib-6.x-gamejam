using Beecon.Components;

namespace Beecon.Systems;

public sealed class SwarmSystem : GameSystem
{
    private Swarm _swarm = null!;

    public override void Initialize()
    {
        _swarm = new Swarm();
        Scene.Entity().Set(_swarm);
    }

    public override void Update()
    {
        _swarm.Elapsed += Time.Delta;
        var elapsed = _swarm.Elapsed.TotalSeconds;
        var interval = Gameplay.Swarm.Interval.TotalSeconds;
        var duration = Gameplay.Swarm.Duration.TotalSeconds;
        _swarm.IsActive = elapsed >= interval && elapsed % interval < duration;
    }
}
