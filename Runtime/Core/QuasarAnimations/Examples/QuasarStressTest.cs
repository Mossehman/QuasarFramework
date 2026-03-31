using UnityEngine;

public class QuasarStressTest : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Vector2 count;
    [SerializeField] Vector2 spacing;

    private void Awake()
    {
        if (prefab == null)
        {
            Debug.LogWarning("GridSpawner: Prefab not assigned.");
            return;
        }

        // Calculate total size of grid
        float totalWidth = (count.x - 1) * spacing.x;
        float totalHeight = (count.y - 1) * spacing.y;

        // Offset to ensure grid is centered on (0, 0)
        Vector2 originOffset = new Vector2(totalWidth / 2f, totalHeight / 2f);

        for (int y = 0; y < count.y; y++)
        {
            for (int x = 0; x < count.x; x++)
            {
                // Calculate position relative to center
                float posX = x * spacing.x - originOffset.x;
                float posY = y * spacing.y - originOffset.y;

                Vector3 spawnPos = new Vector3(posX, 0, posY) + transform.position;
                GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }
}
