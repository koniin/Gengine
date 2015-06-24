﻿using Gengine.Commands;
using Gengine.Input;
using Gengine.Resources;
using Gengine.State;
using Gengine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gengine {
    public class HGengine {
        private readonly Game _gameRef;
        private readonly GraphicsDeviceManager _graphics;
        private RenderingSystem _renderingSystem;
        private readonly IResourceManager _resourceManager;
        private readonly ICommandFactory _commandFactory;
        private IWorld _world;
        private StateManager _stateManager;
        private readonly CommandQueue _commandQueue;

        public HGengine(Game gameRef, GraphicsDeviceManager graphics){
            _gameRef = gameRef;
            _graphics = graphics;
            _resourceManager = new ResourceManager();
            _commandQueue = new CommandQueue();
            _commandFactory = new SimpleCommandFactory();
        }

        public void Start(int gameWidth, int gameHeight, int windowWidth, int windowHeight) {
            _world = new TwoDWorld(gameWidth, gameHeight, windowWidth, windowHeight);
            _renderingSystem = new RenderingSystem(_graphics, _resourceManager, _world);
            _graphics.PreferredBackBufferWidth = windowWidth;
            _graphics.PreferredBackBufferHeight = windowHeight;
            _graphics.ApplyChanges();

            InitSystems();
        }

        public void StartFullScreen(int gameWidth, int gameHeight) {
            int windowWidth = _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.DisplayMode.Width;
            int windowHeight = _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.DisplayMode.Height;
            _graphics.IsFullScreen = true;
            _world = new TwoDWorld(gameWidth, gameHeight, windowWidth, windowHeight);
            _renderingSystem = new RenderingSystem(_graphics, _resourceManager, _world);
            _graphics.ApplyChanges();

            InitSystems();
        }
        
        public void Update(float dt){
            _stateManager.ChangeState();
            if (_stateManager.IsEmpty())
                _gameRef.Exit();

            InputManager.Instance.HandleInput(_commandQueue, _commandFactory);
            _stateManager.HandleCommands(_commandQueue);
            _stateManager.Update(dt);
        }

        public void Draw(){
            _renderingSystem.DrawWithRenderTarget(_stateManager.GetRenderTargets(), 
                _stateManager.GetRenderText(), 
                _stateManager.GetRenderTransformation(), 
                _stateManager.GetRenderColor());
        }

        public void Unload() {
            
        }

        public void AddState(string name, State.State state) {
            _stateManager.Add(name, state);
            if(_stateManager.IsEmpty())
                _stateManager.PushState(name);
        }

        private void InitSystems() {
            _stateManager = new StateManager(_world);
        }

        public void AddFont(string text, string fontPath){
            _resourceManager.AddFont("text", _gameRef.Content.Load<SpriteFont>(fontPath));
        }

        public void AddTexture(string tiles32Png, string spritePath) {
            _resourceManager.AddTexture("tiles32.png", _gameRef.Content.Load<Texture2D>(spritePath));
        }
    }
}