using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{

    public class PlayerMovementExample : MonoBehaviour
    {
        [SerializeField] UnityEvent<Vector3> movementEvent;
        [SerializeField] private float speed = 1.0f;

        public void HandleInput(InputAction.CallbackContext ctx, int playerID)
        {
            Vector2 motionVector = ctx.ReadValue<Vector2>();
            movementEvent.Invoke(new Vector3(motionVector.x, 0, motionVector.y) * Time.deltaTime * speed);
        }
    }
}
