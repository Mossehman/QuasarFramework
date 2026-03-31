using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{
    public class PlayerObject : MonoBehaviour
    {
        [SerializeField] PlayerInputEvent[] playerInputEvents;
        Dictionary<string, PlayerInputEvent> playerInputEventsDictionary = new();

        [SerializeField] private int playerID;

        public void OnSpawned(int playerID)
        {
            this.playerID = playerID;
            foreach (var inputEvent in playerInputEvents)
            {
                playerInputEventsDictionary.TryAdd(inputEvent.GetInputName(), inputEvent);
            }

            InputEventBus.onInputPressed += HandleInputPress;
            InputEventBus.onInputHeld += HandleInputHeld;
            InputEventBus.onInputReleased += HandleInputReleased;
            InputEventBus.onInputPerformed += HandleInputPerformed;
        }

        private void HandleInputPress(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (this.playerID != playerID) return;
            if (playerInputEventsDictionary.TryGetValue(inputName, out var playerInputEvent))
            {
                playerInputEvent.onPressed?.Invoke(context);
            }
        }

        private void HandleInputReleased(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (this.playerID != playerID) return;
            if (playerInputEventsDictionary.TryGetValue(inputName, out var playerInputEvent))
            {
                playerInputEvent.onReleased?.Invoke(context);
            }
        }

        private void HandleInputHeld(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (this.playerID != playerID) return;
            if (playerInputEventsDictionary.TryGetValue(inputName, out var playerInputEvent))
            {
                playerInputEvent.onHeld?.Invoke(context);
            }
        }

        private void HandleInputPerformed(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (this.playerID != playerID) return;
            if (playerInputEventsDictionary.TryGetValue(inputName, out var playerInputEvent))
            {
                playerInputEvent.onPerformed?.Invoke(context);
            }
        }
    }
}
