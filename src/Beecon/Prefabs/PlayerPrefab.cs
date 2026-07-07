using Beecon.Components;

namespace Beecon.Prefabs;

public struct PlayerPrefab : IPrefab
{
    public void Build(Entity entity)
    {
        var body = entity.Scene.World.CreateBody(new BodyDef { Type = BodyType.Dynamic });

        body.CreateShape(new ShapeDef(), CircleShape.Make(25));

        entity.Set(new Player()).Set(body).Set(new Circle(Color.Red) { Scale = 50 });
    }
}
