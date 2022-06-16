using SFML.System;
using SFML.Graphics;

namespace Agario
{
    public abstract class MovingObject : Entity
    {
        private CircleShape sprite;
        private float size;
        public Vector2f Direction { get; set; }
        public float Speed { get; set; }

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

        public MovingObject(CircleShape sprite, float size, Vector2f direction, float speed)
        {
            Sprite = sprite;
            Size = size;
            Direction = direction;
            Speed = speed;
        }
    }
}