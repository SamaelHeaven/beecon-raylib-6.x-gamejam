namespace Beecon.UI;

public class UIHud : UIContainer
{
    public UIHud()
    {
        Add(
            new UIContainer { Size = Vigilance.Core.Display.Size, Justify = Justify.SpaceBetween }[
                new UIExperienceBar()
            ]
        );
    }
}
