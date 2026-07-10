using Beecon.Components;

namespace Beecon.Systems;

public sealed class MatrixRainSystem : GameSystem
{
    private static readonly string[] HexStrings = "0123456789ABCDEF"
        .Select(c => c.ToString())
        .ToArray();

    private readonly Timer _stepTimer = new(Visuals.Matrix.StepInterval);
    private byte[,] _cells = null!;
    private int _cols;
    private int[] _heads = null!;
    private RenderTexture? _matrixBaked;
    private RenderTexture? _matrixBakedPrevious;
    private UIContainer _matrixElement = null!;
    private UIPolygon _matrixPolygon = null!;
    private MatrixRain _rain = null!;
    private RenderTexture _rainTexture = null!;
    private int _rows;

    public override void Initialize()
    {
        var resolution = Visuals.Matrix.Resolution;
        var cell = Visuals.Matrix.CellSize;
        _cols = (int)(resolution / cell);
        _rows = (int)(resolution / cell);
        _rainTexture = new RenderTexture(resolution, resolution, 1f, false);
        _cells = new byte[_cols, _rows];
        _heads = new int[_cols];
        for (var x = 0; x < _cols; x++)
        {
            _heads[x] = Random.Shared.Next(-_rows, 0);
            for (var y = 0; y < _rows; y++)
                _cells[x, y] = RandomHex();
        }

        _matrixPolygon = new UIPolygon(6, Color.White)
        {
            Size = Visuals.Beacon.Size,
            Components =
            [
                new UIDropShadow(Visuals.Beacon.ShadowBlur, Visuals.Matrix.BackgroundColor),
            ],
        };
        _matrixElement = BuildBakeElement(_matrixPolygon);

        _rain = new MatrixRain();
        RenderRain();
        _matrixPolygon.ShapeTexture = _rainTexture.Texture;
        _matrixBaked = _matrixElement.ToTexture();
        _rain.Matrix = _matrixBaked.Texture;
        Scene.Entity().Set(_rain);
    }

    public override void PreRender()
    {
        if (_stepTimer.Update())
            Step();
        RenderRain();
        _matrixPolygon.ShapeTexture = _rainTexture.Texture;
        _matrixBakedPrevious?.Dispose();
        _matrixBakedPrevious = _matrixBaked;
        _matrixBaked = _matrixElement.ToTexture();
        _rain.Matrix = _matrixBaked.Texture;
    }

    private static UIContainer BuildBakeElement(UIPolygon polygon)
    {
        var container = new UIContainer { Padding = Visuals.Beacon.ShadowPadding };
        container.Add(polygon);
        return container;
    }

    private void Step()
    {
        for (var x = 0; x < _cols; x++)
        {
            var head = _heads[x] + 1;
            if (head - Visuals.Matrix.Trail > _rows)
                head = -Random.Shared.Next(0, _rows);
            _heads[x] = head;
            if (head >= 0 && head < _rows)
                _cells[x, head] = RandomHex();
        }
    }

    private void RenderRain()
    {
        var graphics = _rainTexture.Graphics;
        var cell = Visuals.Matrix.CellSize;
        var trail = Visuals.Matrix.Trail;
        graphics.ClearBackground(Visuals.Matrix.BackgroundColor);
        for (var x = 0; x < _cols; x++)
        {
            var head = _heads[x];
            var start = Math.Max(0, head - trail + 1);
            var end = Math.Min(_rows - 1, head);
            for (var y = start; y <= end; y++)
            {
                var distance = head - y;
                var brightness = 1f - distance / (float)trail;
                var color =
                    distance == 0
                        ? Visuals.Matrix.Head
                        : Color.Lerp(
                            Visuals.Matrix.BackgroundColor,
                            Visuals.Matrix.Text,
                            brightness
                        );
                graphics.FillText(
                    HexStrings[_cells[x, y]],
                    x * cell,
                    y * cell,
                    color,
                    fontSize: cell
                );
            }
        }
    }

    private static byte RandomHex()
    {
        return (byte)Random.Shared.Next(HexStrings.Length);
    }
}
