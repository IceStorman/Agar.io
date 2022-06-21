using SFML.System;
using SFML.Graphics;

namespace Agario
{
    public abstract class MoveableObject : Entity
    {
        public virtual Vector2f Direction { get; set; }
        public virtual float Speed { get; set; }

        public MoveableObject(CircleShape sprite, float size, Vector2f direction, float speed)
        {
            Sprite = sprite;
            Size = size;
            Direction = direction;
            Speed = speed;
        }

        public void Move()
        {
            Sprite.Position += Speed * Direction;
        }
    }
}