﻿using Microsoft.Xna.Framework;

namespace Gengine.Rendering {
    public interface IRenderable {
        string TextureName { get; }
        Vector2 RenderPosition { get; }
        Rectangle SourceRectangle { get; }
        bool DebugDraw { get; set; }
    }
}
