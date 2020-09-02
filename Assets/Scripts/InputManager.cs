using Andromeda.Ship;
using System.Collections.Generic;
using UnityEngine;

namespace Andromeda
{
    public abstract class ACommand
    {
        public KeyState State { get; set; }
        public bool IsPhysics { get; private set; }

        public ACommand(bool isPhysics)
        {
            IsPhysics = IsPhysics;
        }

        public abstract void Execute(ShipController ship, Actor actor);
    }


    public enum KeyState
    {
        NONE,
        PRESSED,
        HELD
    }

    public sealed class InputManager : MonoSingleton<InputManager>
    {
        public Dictionary<KeyCode, ACommand> InputMap { get; private set; } = new Dictionary<KeyCode, ACommand>();

        public Queue<ACommand> GetInput()
        {
            Queue<ACommand> inputQueue = new Queue<ACommand>();

            foreach (var input in InputMap)
            {
                // Down
                if (Input.GetKeyDown(input.Key))
                {
                    input.Value.State = KeyState.PRESSED;
                    inputQueue.Enqueue(input.Value);
                }

                // Pressed
                else if (Input.GetKey(input.Key))
                {
                    input.Value.State = KeyState.HELD;
                    inputQueue.Enqueue(input.Value);
                }
            }

            return inputQueue;
        }
    }
}
