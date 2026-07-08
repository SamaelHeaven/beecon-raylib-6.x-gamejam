using Beecon.Components;

namespace Beecon.UI;

public class UIExperienceBar : UIContainer
{
    private readonly UIRectangle _fill;
    private readonly UIText _level;

    public UIExperienceBar()
    {
        Width = Unit.Full;
        Add(
            new UIContainer { Width = Unit.Full, Padding = 4 }[
                new UIRectangle(Color.DarkGray)
                {
                    Width = Unit.Full,
                    Padding = 6,
                    Radius = 4,
                    Stroke = Color.Gray,
                    StrokeWidth = 2,
                }[
                    new UIContainer
                    {
                        Width = Unit.Full,
                        Direction = Direction.LeftToRight,
                        Justify = Justify.SpaceBetween,
                    }[
                        new UIRectangle(Color.SkyBlue)
                        {
                            Position = PositionType.Absolute,
                            Height = Unit.Full,
                        }.Tap(out _fill),
                        new UIContainer(),
                        new UIText(Color.White) { FontSize = 20f }.Tap(out _level)
                    ]
                ]
            ]
        );
    }

    protected override void OnUpdate()
    {
        var player = Entity.Scene.Player;
        if (player.IsNull)
            return;
        var state = player.Get<Player>();
        _fill.Width = Unit.Percent(state.ExperiencePercent);
        _level.Value = $"Lv {state.Level}";
    }
}
