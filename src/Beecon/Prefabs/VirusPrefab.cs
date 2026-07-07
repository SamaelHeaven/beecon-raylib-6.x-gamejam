using Beecon.Components;
using Beecon.Physics;

namespace Beecon.Prefabs;

public struct VirusPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        body.CreateShape(
            new ShapeDef
            {
                Filter = new ShapeFilter
                {
                    Category = ShapeFilterCategory.Virus,
                    Mask = ShapeFilterCategory.Wall | ShapeFilterCategory.Virus,
                },
            },
            CircleShape.Make(14)
        );

        entity
            .SetZIndex(1300)
            .Set(new Virus())
            .Set(body)
            .Set(new Circle(Color.Green) { Scale = 28 })
            .Set(new Health());
    }
}
