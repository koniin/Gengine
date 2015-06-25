﻿using Gengine.Entities;
using Microsoft.Xna.Framework;

namespace Gengine.Examples {
    public class PlayerAbstractEntity : AbstractEntity, IRenderable, ICollidable {
        public string TextureName { get { return null; } }
        public Vector2 Position { get { return GetComponent<MovementComponentOLD>().Position; } }
        public Rectangle SourceRectangle { get { return new Rectangle(0, 0, 0, 0); } }
        public Rectangle BoundingBox { get; private set; }

        public string Identifier {
            get { return "player"; }
        }

        public Vector2 RenderPosition {
            get { return Position; }
        }

        public PlayerAbstractEntity(InputComponentOLD input, MovementComponentOLD movement) {
            // Order matters
            AddComponent(input);
            AddComponent(movement);
        }

        public void Collide(ICollidable target) {
        }
    }
}
