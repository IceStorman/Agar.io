using SFML.Graphics;

namespace Agar.io
{
    public class Food
    {
        public CircleShape foodSprite = new CircleShape();
        public Color foodColor;
        public static int countOfFood = 0;
        public static int maxCountOfFood = 18;
    }
}
