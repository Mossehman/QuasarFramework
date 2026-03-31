using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{
    /// <summary>
    /// Singleton script for managing all player input objects in the world
    /// <br>This should be included in your starting scene, or whatever scene you want the players to join
    /// </summary>
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager instance { get; private set; }
        public event System.Action<int> onNewPlayerJoin;

        // Singleton behaviour
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private readonly Dictionary<int, PlayerInput> players = new();

        /// <summary>
        /// Function for handling player device/input join behaviour
        /// <br>Helps to initialise the player input handler 
        /// </summary>
        /// <param name="newPlayer">The new player object that initialised into the scene</param>
        public void OnNewPlayerJoin(PlayerInput newPlayer)
        {
            if (newPlayer.TryGetComponent(out PlayerInputHandler inputHandler))
            {
                inputHandler.Initialise(newPlayer);
                players.TryAdd(newPlayer.playerIndex, newPlayer);
                onNewPlayerJoin?.Invoke(newPlayer.playerIndex);
            }
        }

        public Dictionary<int, PlayerInput> GetPlayers() { return players; }
    }
}