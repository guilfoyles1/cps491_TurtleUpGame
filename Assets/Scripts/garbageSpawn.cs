using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform player;
    public float spawnDistance = 5f;
    public float spawnRate = 3f;
    public int numObjectsPerSpawn = 3;

    public Vector2 spawnAreaSize = new Vector2(5f, 5f);

    void Start()
    {
        InvokeRepeating(nameof(SpawnObjects), spawnRate, spawnRate);
    }

    void SpawnObjects()
    {
        if (objectToSpawn == null || player == null) return;

        for (int i = 0; i < numObjectsPerSpawn; i++)
        {
            Vector2 randomOffset = new Vector2(
                Random.Range(-spawnAreaSize.x, spawnAreaSize.x),
                Random.Range(-spawnAreaSize.y, spawnAreaSize.y)
            ).normalized * spawnDistance;

            Vector3 spawnPosition = player.position + new Vector3(randomOffset.x, randomOffset.y, 0);

            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
