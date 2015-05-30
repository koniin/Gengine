﻿using Gengine.Commands;
using Gengine.Entities;
using System.Collections.Generic;

namespace Gengine.State {
    public abstract class State {
        public StateManager StateManager { get; set; }
        protected IWorld World { get; set; }

        protected State(IWorld world) {
            World = world;
        }

        // the bool return value marks if its fall through or not
        public abstract bool Update(float deltaTime);
        public abstract void HandleCommands(CommandQueue commandQueue);
        public abstract void Init();
        public abstract IEnumerable<IRenderable> GetRenderTargets();
    }
}