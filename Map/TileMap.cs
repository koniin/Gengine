﻿using System.Collections.Generic;
using Gengine.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gengine.Map {
    public class TileMap {
        private List<Tile> tiles;

        public TileMap(string environmentTextureName) {
            this.tiles = new List<Tile>();

            Vector2 groundPos = new Vector2(0, 480);
            for (int i = 1; i < 26; i++) {
                tiles.Add(new Tile(environmentTextureName, groundPos, new Rectangle(0, 0, 32, 32)));
                groundPos.X += 32;
            }
        }

        public TileMap(int width, int height) {
            Width = width;
            Height = height;
            Layers = new List<Layer>();
        }

        public IEnumerable<IRenderable> Tiles { get { return tiles; } }
        public IEnumerable<ICollidable> CollisionMap { get { return tiles; } }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public IList<Layer> Layers { get; private set; }
    }
}
