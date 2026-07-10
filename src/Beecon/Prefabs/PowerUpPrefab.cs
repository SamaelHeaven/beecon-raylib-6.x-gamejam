using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct PowerUpPrefab(PowerUpType type) : IPrefab
{
    public PowerUpType Type { get; set; } = type;

    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Static });

        entity
            .SetZIndex(Visuals.PowerUp.ZIndex)
            .Set(new PowerUp(Type))
            .Set(body)
            .Set(new Circle(ColorOf(Type)) { Scale = Visuals.PowerUp.Size });

        body.CreateShape(
            new ShapeDef
            {
                IsSensor = true,
                Filter = new ShapeFilter
                {
                    Category = ShapeCategory.PowerUp,
                    Mask = ShapeCategory.Player,
                },
            },
            CircleShape.Make(Gameplay.PowerUp.Radius)
        );
    }

    private static Color ColorOf(PowerUpType type)
    {
        return type switch
        {
            PowerUpType.Health => Visuals.PowerUp.HealthColor,
            PowerUpType.Magnet => Visuals.PowerUp.MagnetColor,
            _ => Visuals.PowerUp.NukeColor,
        };
    }
}
