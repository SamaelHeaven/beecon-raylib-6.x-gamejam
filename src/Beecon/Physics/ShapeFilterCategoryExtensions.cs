namespace Beecon.Physics;

public static class ShapeFilterCategoryExtensions
{
    extension(ShapeFilterCategory)
    {
        public static ShapeFilterCategory Player => (ShapeFilterCategory)(1L << 1);
        public static ShapeFilterCategory Bee => (ShapeFilterCategory)(1L << 2);
    }
}
