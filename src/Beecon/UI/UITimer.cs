using Beecon.Scenes;

namespace Beecon.UI;

public class UITimer : UIContainer
{
    private readonly UIText _text;

    public UITimer()
    {
        Direction = Direction.TopToBottom;
        AlignItems = Align.Center;
        Add(new UIText("00:00") { FontSize = 24f }.Tap(out _text));
    }

    protected override void OnUpdate()
    {
        var swarm = Entity.Scene.Swarm;
        if (swarm is null)
            return;
        var elapsed = swarm.Elapsed;
        _text.Value = $"{(int)elapsed.TotalMinutes:00}:{elapsed.Seconds:00}";
        _text.Fill = swarm.IsActive ? "#FCA5A5" : Color.White;
    }
}
