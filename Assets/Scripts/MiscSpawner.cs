using UnityEngine;

public class ObjectSpawner2 : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform player;
    public float spawnDistance = 5f;
    public float maxSpawnDistance = 5f;
    public float spawnRate = 3f;
    public int numObjectsPerSpawn = 3;
    public bool isBubble;
    public Sprite[] possibleSprites;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObjects), spawnRate, spawnRate);
    }

    void SpawnObjects()
    {
        if (objectToSpawn == null || player == null) return;

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
            randomDirection.y = Random.Range(0.05f, 0.1f);
            if (randomDirection.x > 0)
                spriteRenderer.flipX = true;
            float randomForce = Random.Range(0.5f, 1.5f);
            rb.velocity = randomDirection * randomForce;
        }
    }
}
