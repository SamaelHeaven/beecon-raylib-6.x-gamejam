using Beecon.Components;

namespace Beecon.UI;

public class HealthBar : UIContainer
{
    private readonly UIRectangle _bar;

    public HealthBar()
    {
        Camera = Vigilance.Core.Camera.Scene;
        Add(
            new UIRectangle(Color.Brown) { Height = 6, Width = 60 }[
                new UIRectangle(Color.Red) { Height = Unit.Full }.Tap(out _bar)
            ]
        );
    }

    protected override void OnUpdate()
    {
        var health = Entity.AncestorsAndSelf().Last().Get<Health>();
        _bar.Width = Unit.Percent(health.Percent);
    }
}
