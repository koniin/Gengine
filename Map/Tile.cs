﻿using Gengine.Entities;
using Microsoft.Xna.Framework;

namespace Gengine.Map {
    public class Tile : IRenderable, ICollidable {
        public string Identifier {
            get { return "ground"; }
        }

        public Vector2 RenderPosition {
            get { return new Vector2(Position.X * 32, Position.Y * 32); }
        }

        public string TextureName { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle SourceRectangle { get; private set; }
        public Rectangle BoundingBox { get; private set; }
        public bool IsSolid { get; set; }

        public Tile(string textureName, Vector2 position, Rectangle sourceRectangle, bool solid = true) {
            TextureName = textureName;
            Position = position;
            SourceRectangle = sourceRectangle;
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, sourceRectangle.Width, sourceRectangle.Height);
            IsSolid = solid;
        }

        public void Collide(ICollidable target) {}
    }
}
