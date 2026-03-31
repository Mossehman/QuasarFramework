using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine;

namespace QuasarFramework.InputEvents
{

    /// <summary>
    /// Class for serializing input events in the scene input handler
    /// </summary>
    [System.Serializable]
    public struct SceneInputEvent
    {
        [SerializeField] private string inputName;

        public UnityEvent<InputAction.CallbackContext, int> onPressed;
        public UnityEvent<InputAction.CallbackContext, int> onHeld;
        public UnityEvent<InputAction.CallbackContext, int> onReleased;
        public UnityEvent<InputAction.CallbackContext, int> onPerformed;

        public string GetInputName() { return inputName; }
    }

    /// <summary>
    /// Class for serializing input events in the player object
    /// </summary>
    [System.Serializable]
    public struct PlayerInputEvent
    {
        [SerializeField] private string inputName;

        public UnityEvent<InputAction.CallbackContext> onPressed;
        public UnityEvent<InputAction.CallbackContext> onHeld;
        public UnityEvent<InputAction.CallbackContext> onReleased;
        public UnityEvent<InputAction.CallbackContext> onPerformed;

        public string GetInputName() { return inputName; }
    }
}
