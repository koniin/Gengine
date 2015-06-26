﻿using Gengine.Commands;
using Gengine.Input;

namespace Gengine.Entities {
    public class InputComponentOLD : EntityComponent {
        private readonly CommandQueue _commandQueue;
        private readonly ICommandFactory _commandFactory;
        public InputComponentOLD() {
            _commandQueue = new CommandQueue();
            _commandFactory = new ComponentCommandFactory();
        }

        public override void Update(float deltaTime) {
            InputManager.Instance.HandleRealTimeInput(_commandQueue, _commandFactory);
            while (_commandQueue.HasCommands()) {
                Command(_commandQueue.GetNext());
            }
            _commandQueue.Clear();
            base.Update(deltaTime);
        }

        private void Command(ICommand command) {
            if (command.Name == "Left")
                AbstractEntity.GetComponent<MovementComponentOLD>().Direction = -1;
            if (command.Name == "Right")
                AbstractEntity.GetComponent<MovementComponentOLD>().Direction = 1;
        }

        private class ComponentCommandFactory : ICommandFactory {
            public ICommand CreateCommand(string name) {
                return new Command(name);
            }
        }
    }
}