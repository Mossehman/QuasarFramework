using System;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{
    /// <summary>
    /// Static class for handling the input lifecycles
    /// <br>External classes can bind themselves to the different events
    /// </summary>
    public static class InputEventBus
    {
        // The input events that will be invoked

        public static event Action<InputAction.CallbackContext, string, int> onInputPressed;
        public static event Action<InputAction.CallbackContext, string, int> onInputHeld;
        public static event Action<InputAction.CallbackContext, string, int> onInputReleased;
        public static event Action<InputAction.CallbackContext, string, int> onInputPerformed;



        // public methods for invoking the events
        // (ik the events themselves are already public, this should not be the case, I was just lazy)

        public static void RaiseInputPressed(InputAction.CallbackContext context, string inputName, int playerID) => onInputPressed?.Invoke(context, inputName, playerID);
        public static void RaiseInputHeld(InputAction.CallbackContext context, string inputName, int playerID) => onInputHeld?.Invoke(context, inputName, playerID);
        public static void RaiseInputReleased(InputAction.CallbackContext context, string inputName, int playerID) => onInputReleased?.Invoke(context, inputName, playerID);
        public static void RaiseInputPerformed(InputAction.CallbackContext context, string inputName, int playerID) => onInputPerformed?.Invoke(context, inputName, playerID);


        /*
        context - The input context/data, allows us to read values from it, such as the Vector2 input (context.ReadValue<Vector2>())
        inputName - The string value of the input (Move, Look, etc)
        playerID - The id of the player that called the input, useful for local multiplayer systems
         */
    }
}
