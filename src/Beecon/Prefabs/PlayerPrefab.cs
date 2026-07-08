using Beecon.Components;
using Beecon.Physics;
using Beecon.UI;

namespace Beecon.Prefabs;

public struct PlayerPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        entity
            .SetZIndex(1000)
            .Set(new Player())
            .Set(body)
            .Set(new Circle(Color.Gold) { Scale = 50 })
            .Set(new Health(1_000))
            .Scope(scene =>
            {
                scene.Entity().SetPosition(new Vector2(0, 40)).Set(new HealthBar());
            });

        body.CreateShape(
            new ShapeDef { Filter = new ShapeFilter { Category = ShapeFilterCategory.Player } },
            CircleShape.Make(25)
        );
    }
}
