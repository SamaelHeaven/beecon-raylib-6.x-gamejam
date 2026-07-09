namespace Beecon.UI;

public class UIHud : UIContainer
{
    public readonly UIStatMenu StatMenu;

    public UIHud()
    {
        Culling = false;
        Add(
            new UIContainer
            {
                Size = Vigilance.Core.Display.Size,
                Direction = Direction.TopToBottom,
                Justify = Justify.SpaceBetween,
                AlignItems = Align.Start,
            }[
                new UIContainer
                {
                    Width = Unit.Full,
                    Direction = Direction.TopToBottom,
                    AlignItems = Align.Center,
                }[new UIExperienceBar(), new UITimer()],
                new UIStatMenu().Tap(out StatMenu)
            ]
        );
    }
}
