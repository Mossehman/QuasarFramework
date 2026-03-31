using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{
    /// <summary>
    /// Script entrypoint for handling player inputs
    /// <br>This script will register all inputs from the action map and broadcast them to the Input Event Bus
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] protected string actionMapID;
        List<PlayerInputEvent> inputEvents = new();
        PlayerInput input;

        public void Initialise(PlayerInput input)
        {
            this.input = input;
            InputActionMap actionMap = input.actions.FindActionMap(actionMapID);
            foreach (var action in actionMap.actions)
            {
                PlayerInputEvent newInputEvent = new PlayerInputEvent();
                newInputEvent.Initalise(actionMap, input.playerIndex, action.name);
                inputEvents.Add(newInputEvent);
            }
            DontDestroyOnLoad(input);
        }

        private void Update()
        {
            foreach (var input in inputEvents)
            {
                input.Update();
            }
        }

        public void SetActionMapID(string newID)
        {
            this.actionMapID = newID;
            inputEvents.Clear();
            InputActionMap actionMap = input.actions.FindActionMap(newID);
            foreach (var action in actionMap.actions)
            {
                PlayerInputEvent newInputEvent = new PlayerInputEvent();
                newInputEvent.Initalise(actionMap, input.playerIndex, action.name);
                inputEvents.Add(newInputEvent);
            }
        }



        protected class PlayerInputEvent
        {
            private string inputName;
            private bool isHeld;
            private int playerID;

            InputAction action;
            InputAction.CallbackContext callbackContext;

            public void Initalise(InputActionMap actionMap, int playerID, string inputName)
            {
                this.playerID = playerID;
                this.inputName = inputName;
                action = actionMap.FindAction(inputName);
                action.started += OnInputPressed;
                action.canceled += OnInputReleased;
                action.performed += OnInputPerformed;
            }

            private void OnInputPressed(InputAction.CallbackContext callbackContext)
            {
                this.callbackContext = callbackContext;
                isHeld = true;
                InputEventBus.RaiseInputPressed(callbackContext, inputName, playerID);
            }

            private void OnInputReleased(InputAction.CallbackContext callbackContext)
            {
                this.callbackContext = callbackContext;
                isHeld = false;
                InputEventBus.RaiseInputReleased(callbackContext, inputName, playerID);
            }

            private void OnInputPerformed(InputAction.CallbackContext callbackContext)
            {
                InputEventBus.RaiseInputPerformed(callbackContext, inputName, playerID);
            }

            public void Update()
            {
                if (!isHeld) { return; }
                InputEventBus.RaiseInputHeld(callbackContext, inputName, playerID);
            }

            public void Unsubscribe()
            {
                if (action != null)
                {
                    action.started -= OnInputPressed;
                    action.canceled -= OnInputReleased;
                    action.performed -= OnInputPerformed;
                }
            }
        }
    }
}
