﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gengine.Commands;
using Gengine.Rendering;
using Microsoft.Xna.Framework;

namespace Gengine.State {
    public class StateManager {
        private readonly IWorld _world;
        private readonly Stack<State> _stateStack;
        private readonly Queue<Action> _stateQueue;
        private readonly Dictionary<string, State> _availableStates = new Dictionary<string, State>();
        private Matrix? _transformationMatrix;
        private Color _color;

        public StateManager(IWorld world) {
            _world = world;
            _stateStack = new Stack<State>();
            _stateQueue = new Queue<Action>();
            _color = Color.White;
        }

        public void Add(string stateId, State state) {
            SetupState(state);
            _availableStates.Add(stateId, state);
        }

        public void PopState() {
            _stateQueue.Enqueue(() =>
            {
                var state = _stateStack.Pop();
                state.Unload();
                SetTransformationMatrix(null);
            });
        }

        public void ClearStates() {
            _stateQueue.Enqueue(() => {
                while (_stateStack.Count != 0){
                    var state = _stateStack.Pop();
                    state.Unload();
                }
                SetTransformationMatrix(null);
            });
        }

        public void PushState(string stateId) {
            if (_availableStates.ContainsKey(stateId))
                _stateQueue.Enqueue(() => PushStateNow(stateId));
        }

        public void PushAndSetupState(string stateId) {
            if (_availableStates.ContainsKey(stateId))
                _stateQueue.Enqueue(() =>{
                    _availableStates[stateId].Setup();
                    PushStateNow(stateId);
                });
        }

        private void PushStateNow(string stateId){
            if (_availableStates[stateId].Initialized){
                _availableStates[stateId].Unload();
            }
            _availableStates[stateId].Run();
            _availableStates[stateId].Initialized = true;
            _stateStack.Push(_availableStates[stateId]);
        }

        public void PushState(Transition state) {
            SetupState(state);
            _stateQueue.Enqueue(() =>{
                if (_availableStates.ContainsKey(state.NextStateId)){
                    PushStateNow(state.NextStateId);
                }
                state.Run();
                _stateStack.Push(state);
            });
        }

        public void HandleCommands(CommandQueue commands) {
            if (_stateStack.Count == 0)
                return;

            var currentState = _stateStack.Peek();
            currentState.HandleCommands(commands);
        }

        public void ChangeState() {
            while (_stateQueue.Count != 0) {
                var action = _stateQueue.Dequeue();
                action();
            }
        }

        public bool IsEmpty() {
            return _stateStack.Count == 0;
        }

        public void Update(float deltaTime) {
            foreach (var state in _stateStack) {
                if (!state.Update(deltaTime))
                    return;
            }
        }
        
        private void SetupState(State state) {
            state.StateManager = this;
            state.World = _world;
        }

        public IEnumerable<IEnumerable<IRenderable>> GetRenderLayers() {
            return _stateStack.SelectMany(state => state.GetRenderLayers());
        } 

        public IEnumerable<IRenderableText> GetRenderText() {
            return _stateStack.SelectMany(state => state.GetTextRenderTargets());
        }
        
        public Matrix? GetRenderTransformation() {
            return _transformationMatrix;
        }

        public void SetTransformationMatrix(Matrix? transformationMatrix) {
            _transformationMatrix = transformationMatrix;
        }

        public void SetColor(Color color){
            _color = color;
        }

        public Color GetRenderColor() {
            return _color;
        }
    }
}
