using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform player;
    public float spawnDistance = 5f;
    public float maxSpawnDistance = 5f;
    public float spawnRate = 3f;
    public int numObjectsPerSpawn = 3;
    public Sprite[] possibleSprites;
    public int numSpawned = 0;
    public int maxSpawn = 20;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObjects), spawnRate, spawnRate);
    }

    void SpawnObjects()
    {
        if (objectToSpawn == null || player == null) return;

        if (numSpawned < maxSpawn)
        {
            for (int i = 0; i < numObjectsPerSpawn; i++)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float randomDistance = Random.Range(spawnDistance, maxSpawnDistance);
                Vector3 spawnPosition = player.position + (Vector3)(randomDirection * randomDistance);

                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);


                SpriteRenderer spriteRenderer = spawnedObject.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];

                Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
                randomDirection = Random.insideUnitCircle.normalized;
                float randomForce = Random.Range(0.1f, 0.2f);
                rb.velocity = randomDirection * randomForce;

                numSpawned++;
            }
        }
    }
}
