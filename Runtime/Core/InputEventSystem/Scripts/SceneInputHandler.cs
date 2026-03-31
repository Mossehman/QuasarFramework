using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{
    public class SceneInputHandler : MonoBehaviour
    {
        [SerializeField] SceneInputEvent[] sceneInputEvents;
        private Dictionary<string, SceneInputEvent> sceneInputEventsDictionary = new();

        private void Awake()
        {
            if (sceneInputEvents == null || sceneInputEvents.Length == 0) { return; }
            foreach (var globalInputEvent in sceneInputEvents)
            {
                sceneInputEventsDictionary.TryAdd(globalInputEvent.GetInputName(), globalInputEvent);
            }

            InputEventBus.onInputPressed += HandleInputPress;
            InputEventBus.onInputHeld += HandleInputHeld;
            InputEventBus.onInputReleased += HandleInputReleased;
            InputEventBus.onInputPerformed += HandleInputPerformed;
        }

        private void HandleInputPress(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (sceneInputEventsDictionary.TryGetValue(inputName, out var globalInputEvent))
            {
                globalInputEvent.onPressed?.Invoke(context, playerID);
            }
        }

        private void HandleInputReleased(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (sceneInputEventsDictionary.TryGetValue(inputName, out var globalInputEvent))
            {
                globalInputEvent.onReleased?.Invoke(context, playerID);
            }
        }

        private void HandleInputHeld(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (sceneInputEventsDictionary.TryGetValue(inputName, out var globalInputEvent))
            {
                globalInputEvent.onHeld?.Invoke(context, playerID);
            }
        }

        private void HandleInputPerformed(InputAction.CallbackContext context, string inputName, int playerID)
        {
            if (sceneInputEventsDictionary.TryGetValue(inputName, out var globalInputEvent))
            {
                globalInputEvent.onPerformed?.Invoke(context, playerID);
            }
        }
    }
}
