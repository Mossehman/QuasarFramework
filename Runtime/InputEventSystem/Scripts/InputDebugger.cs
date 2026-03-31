using UnityEngine;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{

    /// <summary>
    /// Helper class for debugging the input event system, should be excluded from production build
    /// <br>TODO: Use Unity macros to exclude this file from builds
    /// </summary>
    public class InputDebugger : MonoBehaviour
    {
        public void DebugInputPress(InputAction.CallbackContext context, int playerID)
        {
            Debug.Log("Player " + playerID.ToString() + " pressed: " + context.ToString());
        }

        public void DebugInputPress(InputAction.CallbackContext context)
        {
            Debug.Log(gameObject.name + " pressed: " + context.ToString());
        }

        public void DebugInputHold(InputAction.CallbackContext context, int playerID)
        {
            Debug.Log("Player " + playerID.ToString() + " held: " + context.ToString());
        }

        public void DebugInputHold(InputAction.CallbackContext context)
        {
            Debug.Log(gameObject.name + " held: " + context.ToString());
        }

        public void DebugInputRelease(InputAction.CallbackContext context, int playerID)
        {
            Debug.Log("Player " + playerID.ToString() + " released: " + context.ToString());
        }

        public void DebugInputRelease(InputAction.CallbackContext context)
        {
            Debug.Log(gameObject.name + " released: " + context.ToString());
        }

        public void DebugInputPerformed(InputAction.CallbackContext context, int playerID)
        {
            Debug.Log("Player " + playerID.ToString() + " performed: " + context.ToString());
        }

        public void DebugInputPerformed(InputAction.CallbackContext context)
        {
            Debug.Log(gameObject.name + " performed: " + context.ToString());
        }
    }
}
