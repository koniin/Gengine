﻿using Gengine.Entities;
using Microsoft.Xna.Framework;

namespace Gengine.Background {
    public interface IBackgroundTile : IRenderable{
        void Update(float dt, Vector2 direction);
    }
}
