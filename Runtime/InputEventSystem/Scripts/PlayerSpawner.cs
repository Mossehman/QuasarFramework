using UnityEngine;
using UnityEngine.InputSystem;

namespace QuasarFramework.InputEvents
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] GameObject playerPrefab;
        [SerializeField] Transform[] spawnPoints;

        private int spawnIndex = 0;

        private void Start()
        {
            PlayerManager playerManager = PlayerManager.instance;
            if (!playerManager) { return; }
            playerManager.onNewPlayerJoin += SpawnNewPlayer;
            SpawnExistingPlayers();
        }

        public void SpawnExistingPlayers()
        {
            PlayerManager playerManager = PlayerManager.instance;
            if (!playerManager) { return; }
            foreach (var playerData in playerManager.GetPlayers())
            {
                int playerID = playerData.Key;
                GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
                if (playerInstance.TryGetComponent(out PlayerObject playerObject))
                {
                    playerObject.OnSpawned(playerID);
                    spawnIndex++;
                    if (spawnIndex >= spawnPoints.Length)
                    {
                        spawnIndex = 0;
                    }
                }
                else
                {
                    DestroyImmediate(playerInstance);
                }
            }
        }

        public void SpawnNewPlayer(int playerID)
        {
            GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            if (playerInstance.TryGetComponent(out PlayerObject playerObject))
            {
                playerObject.OnSpawned(playerID);
                spawnIndex++;
                if (spawnIndex >= spawnPoints.Length)
                {
                    spawnIndex = 0;
                }
            }
            else
            {
                DestroyImmediate(playerInstance);
            }
        }
    }
}