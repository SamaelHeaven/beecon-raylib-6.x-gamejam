using Beecon.Components;
using Beecon.Scenes;

namespace Beecon.UI;

public class UIBeacon : UIContainer
{
    private readonly UISprite _base;
    private readonly UIPolygon _charge;

    public UIBeacon()
    {
        Camera = Vigilance.Core.Camera.Scene;
        Size = Visuals.Beacon.BakedSize;
        Add(
            new UISprite { Size = Visuals.Beacon.BakedSize, Position = PositionType.Absolute }.Tap(
                out _base
            ),
            new UIPolygon(6, Color.Transparent)
            {
                Size = 0,
                Fill = Visuals.Background.BackgroundColor,
                Stroke = Visuals.Background.LineColor,
                StrokeWidth = Visuals.Background.LineThickness,
                Position = PositionType.Absolute,
            }.Tap(out _charge)
        );
    }

    protected override void OnUpdate()
    {
        if (!Entity.TryGet(out Beacon beacon))
            return;
        var rain = Entity.Scene.MatrixRain;
        if (rain is null)
            return;
        var size = Vector2.Lerp(0, Visuals.Beacon.Size, Ease.OutSine(beacon.Progress));
        _base.Texture = rain.Matrix;
        _charge.Size = size;
        _charge.Translate = (Visuals.Beacon.BakedSize - size) / 2;
    }
}
