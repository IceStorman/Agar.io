using SFML.Graphics;

namespace Agario
{
    public abstract class Entity
    {
        public virtual CircleShape Sprite { get; set; }
        public virtual float Size { get; set; }
    }
}