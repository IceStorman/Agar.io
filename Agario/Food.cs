using SFML.Graphics;

namespace Agario
{
    internal class Food : Entity
    {
        private CircleShape sprite;
        private float size;

        public override CircleShape Sprite
        {
            get => sprite;
            set => sprite = value;
        }

        public override float Size
        {
            get => size;
            set => size = value;
        }
    }
}